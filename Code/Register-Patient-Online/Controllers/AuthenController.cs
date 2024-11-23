using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Register_Patient_Online.ViewModels;


namespace Register_Patient_Online.Controllers
    {
        public class AuthenController : Controller
        {
            private readonly RegisterPatientOnlineContext _context;

            public AuthenController(RegisterPatientOnlineContext context)
            {
                _context = context;
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
            [HttpPost]
            public IActionResult Register(AuthenViewModel model)
            {
                if (!ModelState.IsValid)
                {
                    TempData["RegisterError"] = "Đăng ký thất bại, vui lòng thử lại.";
                    return RedirectToAction("Index");
                }

                // Tạo tài khoản mới
                var newUser = new TaiKhoan
                {
                    TenDangNhap = model.TenDangNhap,
                    MatKhau = model.MatKhau,
                };
                var newBenhNhan = new BenhNhan
                {
                    Hoten = model.Hoten,
                    Email = model.Email,
                    Sdt = model.Sdt,
                    DiaChi = model.DiaChi,
                    Cccd = model.Cccd
                };
                _context.TaiKhoans.Add(newUser);
                _context.BenhNhans.Add(newBenhNhan);
                _context.SaveChanges();

                TempData["RegisterSuccess"] = "Đăng kí thành công! Bạn có thể đăng nhập.";
                return RedirectToAction("Index");
            }

            // POST: Authen/ForgotPassword
            [HttpPost]
            public IActionResult ForgotPassword(string Email)
            {
                if (string.IsNullOrEmpty(Email))
                {
                    TempData["ForgotPasswordError"] = "Email không được để trống.";
                    return RedirectToAction("Index");
                }

                var user = _context.BenhNhans.FirstOrDefault(u => u.Email == Email);
                if (user == null)
                {
                    TempData["ForgotPasswordError"] = "Không tồn tại email.";
                    return RedirectToAction("Index");
                }

                // Giả lập gửi email
                TempData["ForgotPasswordSuccess"] = $"Email thay đổi mật khẩu đã được gửi đến {Email}.";
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

