using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Register_Patient_Online.Models;

namespace Register_Patient_Online.Controllers
{
    public class DangKyKhamsController : Controller
    {
        private readonly RegisterPatientOnlineContext _context;

        public DangKyKhamsController(RegisterPatientOnlineContext context)
        {
            _context = context;
        }

        // GET: DangKyKhams
        public async Task<IActionResult> Index()
        {
            // Lấy thông tin người dùng đang đăng nhập
            var user = HttpContext.Session.GetString("UserName");

            // Lọc danh sách DangKyKhams cho tài khoản hiện tại
            var registerPatientOnlineContext = _context.DangKyKhams
                .Where(d => d.MaBnNavigation.MaTkNavigation.TenDangNhap == user)
                .Include(d => d.MaBnNavigation)
                .Include(d => d.MaBsNavigation)
                .Include(d => d.MaBsNavigation.MaKhoaNavigation);

            return View(await registerPatientOnlineContext.ToListAsync());
        }


        // GET: DangKyKhams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DangKyKhams == null)
            {
                return NotFound();
            }

            var dangKyKham = await _context.DangKyKhams
                .Include(d => d.MaBnNavigation)
                .Include(d => d.MaBsNavigation)
                .FirstOrDefaultAsync(m => m.MaDk == id);
            if (dangKyKham == null)
            {
                return NotFound();
            }

            return View(dangKyKham);
        }

        // GET: DangKyKhams/Create
        public IActionResult Create()
        {
            var listKhoa = _context.KhoaKhamBenhs.ToList();  // Đảm bảo đây không phải là null
            ViewBag.ListKhoa = listKhoa;

            return View();
        }
        // Lấy danh sách bác sĩ theo Khoa, Ngày đến khám, và Ca đăng ký
        [HttpGet]
        public JsonResult GetBacSi(string maKhoa, string ngayDenKham, string caDangKi)
        {
            var bacSiList = _context.BacSis
                                    .Where(b => b.MaKhoa == maKhoa)
                                    .Join(_context.LichLamViecs,
                                          b => b.MaBs,
                                          l => l.MaBs,
                                          (b, l) => new { b.MaBs, b.Ten, l.Ngay, l.CaLamViec })
                                    .Where(b => b.Ngay == DateTime.Parse(ngayDenKham) && b.CaLamViec == caDangKi)
                                    .Select(b => new { b.MaBs, b.Ten })
                                    .ToList();
            var json_bslist = Json(bacSiList);
            return Json(bacSiList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaBs,NgayDenKham,CaDangKi")] DangKyKham dangKyKham)
        {
            // Lấy thông tin bệnh nhân từ Session
            var user = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToAction("Login", "Account"); // Chuyển hướng nếu chưa đăng nhập
            }

            // Lấy MaBn từ database dựa trên UserName
            var benhNhan = _context.BenhNhans.FirstOrDefault(bn => bn.MaTkNavigation.TenDangNhap == user);
            if (benhNhan == null)
            {
                return NotFound("Không tìm thấy thông tin bệnh nhân.");
            }

            // Gán giá trị mặc định
            dangKyKham.MaBn = benhNhan.MaBn;
            dangKyKham.NgayDangKi = DateTime.Now;
            dangKyKham.TrangThai = "CHỜ XÁC NHẬN";

            _context.Add(dangKyKham);
            await _context.SaveChangesAsync();

            ViewBag.ListKhoa = _context.KhoaKhamBenhs.ToList();
            return RedirectToAction("Index", "DangKyKhams");
        }

        // GET: DangKyKhams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DangKyKhams == null)
            {
                return NotFound();
            }

            var dangKyKham = await _context.DangKyKhams.FindAsync(id);
            if (dangKyKham == null)
            {
                return NotFound();
            }
            ViewData["MaBn"] = new SelectList(_context.BenhNhans, "MaBn", "MaBn", dangKyKham.MaBn);
            ViewData["MaBs"] = new SelectList(_context.BacSis, "MaBs", "MaBs", dangKyKham.MaBs);
            return View(dangKyKham);
        }

        // POST: DangKyKhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaDk,MaBn,MaBs,NgayDangKi,NgayDenKham,TrangThai")] DangKyKham dangKyKham)
        {
            if (id != dangKyKham.MaDk)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dangKyKham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DangKyKhamExists(dangKyKham.MaDk))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaBn"] = new SelectList(_context.BenhNhans, "MaBn", "MaBn", dangKyKham.MaBn);
            ViewData["MaBs"] = new SelectList(_context.BacSis, "MaBs", "MaBs", dangKyKham.MaBs);
            return View(dangKyKham);
        }

        // GET: DangKyKhams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DangKyKhams == null)
            {
                return NotFound();
            }

            var dangKyKham = await _context.DangKyKhams
                .Include(d => d.MaBnNavigation)
                .Include(d => d.MaBsNavigation)
                .FirstOrDefaultAsync(m => m.MaDk == id);
            if (dangKyKham == null)
            {
                return NotFound();
            }

            return View(dangKyKham);
        }

        // POST: DangKyKhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DangKyKhams == null)
            {
                return Problem("Entity set 'RegisterPatientOnlineContext.DangKyKhams'  is null.");
            }
            var dangKyKham = await _context.DangKyKhams.FindAsync(id);
            if (dangKyKham != null)
            {
                _context.DangKyKhams.Remove(dangKyKham);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DangKyKhamExists(int id)
        {
          return (_context.DangKyKhams?.Any(e => e.MaDk == id)).GetValueOrDefault();
        }
    }
}
