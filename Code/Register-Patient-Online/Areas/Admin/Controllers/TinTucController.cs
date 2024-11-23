using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Register_Patient_Online.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Register_Patient_Online.Controllers
{
    [Area("Admin")]
    public class TinTucController : Controller
    {
        private readonly RegisterPatientOnlineContext _context;

        public TinTucController(RegisterPatientOnlineContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách tin tức
        public async Task<IActionResult> Index()
        {
            var tinTucs = await _context.TinTucs.ToListAsync();
            return View(tinTucs);
        }

        // GET: Xóa tin tức (hiển thị thông tin trước khi xóa)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var tinTuc = await _context.TinTucs.FindAsync(id);
            if (tinTuc == null)
                return NotFound();

            return View(tinTuc);
        }

        // POST: Xóa tin tức
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tinTuc = await _context.TinTucs.FindAsync(id);
            if (tinTuc == null)
                return NotFound();

            _context.TinTucs.Remove(tinTuc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Chỉnh sửa tin tức (hiển thị form chỉnh sửa)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var tinTuc = await _context.TinTucs.FindAsync(id);
            if (tinTuc == null)
                return NotFound();

            return View(tinTuc);
        }

        // POST: Chỉnh sửa tin tức
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaTinTuc,NoiDung,NgayDang")] TinTuc tinTuc)
        {
            if (id != tinTuc.MaTinTuc)
                return NotFound();

            if (ModelState.IsValid)
            {
                var existingTinTuc = await _context.TinTucs.FindAsync(tinTuc.MaTinTuc);
                if (existingTinTuc == null)
                    return NotFound();

                existingTinTuc.NoiDung = tinTuc.NoiDung;
                existingTinTuc.NgayDang = tinTuc.NgayDang;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tinTuc);
        }

        // GET: Hiển thị form tạo mới tin tức
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tạo mới tin tức
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaTinTuc,NoiDung,NgayDang")] TinTuc tinTuc)
        {
            if (ModelState.IsValid)
            {
                _context.TinTucs.Add(tinTuc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tinTuc);
        }
    }
}