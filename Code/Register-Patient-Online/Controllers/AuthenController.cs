using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Register_Patient_Online.ViewModels;
using Register_Patient_Online.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Register_Patient_Online.Controllers
    {
        public class AuthenController : Controller
        {
            private readonly RegisterPatientOnlineContext _context;
            private readonly IEmailService _emailService;

           public AuthenController(RegisterPatientOnlineContext context, IEmailService emailService)
            {
                _context = context;
                _emailService = emailService;
            }

            public IActionResult Index()
            {
                return View(new AuthenViewModel());
            }

            // POST: Authen/Login
            [HttpPost]
            public IActionResult Login(string TenDangNhap, string MatKhau)
            {
                var user = _context.TaiKhoans
                    .FirstOrDefault(u => u.TenDangNhap == TenDangNhap && u.MatKhau == MatKhau);

                if (user != null)
                {
                    // Lưu thông tin người dùng vào Session
                    HttpContext.Session.SetString("UserName", user.TenDangNhap);
                    HttpContext.Session.SetString("Role", user.Role.ToString()); 

                    // Thông báo đăng nhập thành công
                    TempData["LoginSuccess"] = $"Đăng nhập thành công. Chào mừng, {user.TenDangNhap}!";
                    return RedirectToAction("Index", "Home");
                }

                else
                {
                        TempData["LoginError"] = "Tài khoản hoặc mật khẩu không đúng.";
                        return RedirectToAction("Index");
                    }
                }

        // POST: Authen/Register
        // POST: Authen/Register
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(AuthenViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["RegisterError"] = "Đăng ký thất bại, vui lòng thử lại.";
                return RedirectToAction("Index");
            }

            // Kiểm tra tên đăng nhập đã tồn tại chưa
            var existingUser = _context.TaiKhoans.FirstOrDefault(u => u.TenDangNhap == model.TenDangNhap);
            if (existingUser != null)
            {
                TempData["RegisterError"] = "Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác.";
                return RedirectToAction("Index");
            }

            // Tạo tài khoản mới
            var newUser = new TaiKhoan
            {
                TenDangNhap = model.TenDangNhap,
                MatKhau = model.MatKhau,
            };

            // Tạo mã bệnh nhân và đảm bảo không bị trùng lặp
            string patientCode;
            bool isUniquePatientCode = false;

            do
            {
                patientCode = "BN" + new Random().Next(10000, 99999).ToString();
                isUniquePatientCode = !_context.BenhNhans.Any(b => b.MaBn == patientCode);
            } while (!isUniquePatientCode);

            var newBenhNhan = new BenhNhan
            {
                MaBn = patientCode,
                Hoten = model.Hoten,
                Email = model.Email,
                Sdt = model.Sdt,
                DiaChi = model.DiaChi,
                Cccd = model.Cccd,

            };

            try
            {
                _context.TaiKhoans.Add(newUser);
                await _context.SaveChangesAsync();
                var newMatk = _context.TaiKhoans
                      .Where(t => t.TenDangNhap == newUser.TenDangNhap)
                      .Select(t => t.MaTk)
                      .FirstOrDefault();
                newBenhNhan.MaTk = newMatk;
                _context.BenhNhans.Add(newBenhNhan);
                await _context.SaveChangesAsync();

                // Gửi email thông báo
                string subject = "Chào mừng bạn đến với hệ thống đăng ký khám bệnh";
                string body = $"Xin chào {model.Hoten},<br><br>"
                            + "Tài khoản của bạn đã được tạo thành công.<br>"
                            + "Hãy đăng nhập để bắt đầu sử dụng dịch vụ.<br><br>"
                            + "Trân trọng,<br>Bệnh viện SIU";

                bool emailSent = await _emailService.SendEmailAsync(model.Email, subject, body);

                if (!emailSent)
                {
                    TempData["EmailError"] = "Đăng ký thành công nhưng gửi email thất bại.";
                }

                TempData["RegisterSuccess"] = "Đăng ký thành công! Bạn có thể đăng nhập.";
            }
            catch (DbUpdateException ex)
            {
                // Xử lý lỗi trùng lặp hoặc lỗi cơ sở dữ liệu khác
                if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2627) // 2627: UNIQUE constraint error
                {
                    TempData["RegisterError"] = "Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác.";
                }
                else
                {
                    TempData["RegisterError"] = "Đăng ký thất bại. Vui lòng thử lại.";
                }
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }


        // POST: Authen/ForgotPassword
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                TempData["ForgotPasswordError"] = "Email không được để trống.";
                return RedirectToAction("Index");
            }

            // Tìm người dùng trong cơ sở dữ liệu theo email
            var user = _context.TaiKhoans.FirstOrDefault(u => u.BenhNhan.Email == Email);
            if (user == null)
            {
                TempData["ForgotPasswordError"] = "Không tồn tại email.";
                return RedirectToAction("Index");
            }

            // Lấy mật khẩu hiện tại của người dùng
            string currentPassword = user.MatKhau;  // Giả sử bạn lưu mật khẩu dưới dạng plain text

            // Gửi email với mật khẩu hiện tại
            string subject = "Thông tin mật khẩu của bạn";
            string body = $"Xin chào {user.TenDangNhap},<br><br>"
                        + "Mật khẩu hiện tại của bạn là: <strong>" + currentPassword + "</strong><br><br>"
                        + "Trân trọng,<br>Bệnh viện SIU";

            bool emailSent = await _emailService.SendEmailAsync(Email, subject, body);

            if (!emailSent)
            {
                // Xử lý khi gửi email thất bại
                TempData["EmailError"] = "Đã xảy ra lỗi khi gửi email.";
            }
            else
            {
                TempData["ForgotPasswordSuccess"] = $"Mật khẩu đã được gửi đến email {Email}.";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
            public IActionResult LogOut()
            {
                // Xóa tất cả session
                HttpContext.Session.Clear();

                // Thông báo đăng xuất thành công (nếu cần)
                TempData["LogoutSuccess"] = "Bạn đã đăng xuất thành công.";

                // Chuyển hướng về trang chủ hoặc trang đăng nhập
                return RedirectToAction("Index", "Home");
            }
        }
 }

