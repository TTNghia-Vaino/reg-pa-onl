using Microsoft.AspNetCore.Mvc;
using Register_Patient_Online.Models;
using System.Linq;

namespace Register_Patient_Online.Controllers
{
    public class TinTucController : Controller
    {
        private readonly RegisterPatientOnlineContext _context;

        public TinTucController(RegisterPatientOnlineContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách tin tức
        public IActionResult Index()
        {
            var tinTucs = _context.TinTucs.ToList();
            return View(tinTucs);
        }

        // GET: Xóa tin tức (hiển thị thông tin trước khi xóa)
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var tinTuc = _context.TinTucs.FirstOrDefault(t => t.MaTinTuc == id);
            if (tinTuc == null) return NotFound();

            return View(tinTuc);
        }

        // POST: Xóa tin tức
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var tinTuc = _context.TinTucs.Find(id);
            if (tinTuc == null) return NotFound();

            _context.TinTucs.Remove(tinTuc);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Chỉnh sửa tin tức (hiển thị form chỉnh sửa)
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var tinTuc = _context.TinTucs.FirstOrDefault(t => t.MaTinTuc == id);
            if (tinTuc == null) return NotFound();

            return View(tinTuc);
        }

        // POST: Chỉnh sửa tin tức
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("MaTinTuc,NoiDung,NgayDang")] TinTuc tinTuc)
        {
            if (id != tinTuc.MaTinTuc) return NotFound();

            if (ModelState.IsValid)
            {
                var existingTinTuc = _context.TinTucs.Find(tinTuc.MaTinTuc);
                if (existingTinTuc == null) return NotFound();

                existingTinTuc.NoiDung = tinTuc.NoiDung;
                existingTinTuc.NgayDang = tinTuc.NgayDang;

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(tinTuc);
        }
    }
}
