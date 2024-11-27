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
            var registerPatientOnlineContext = _context.DangKyKhams.Include(d => d.MaBnNavigation).Include(d => d.MaKhoaNavigation);
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
                .Include(d => d.MaKhoaNavigation)
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
            ViewBag.TenKhoa = new SelectList(_context.KhoaKhamBenhs, "MaKhoa", "TenKhoa");
            return View();
        }

        // POST: DangKyKhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NgayDenKham,MaKhoa,TrangThai")] DangKyKham dangKyKham)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dangKyKham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.TenKhoa = new SelectList(_context.KhoaKhamBenhs, "MaKhoa", "TenKhoa", dangKyKham.MaKhoa);
            return View(dangKyKham);
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
            ViewData["MaKhoa"] = new SelectList(_context.KhoaKhamBenhs, "MaKhoa", "MaKhoa", dangKyKham.MaKhoa);
            return View(dangKyKham);
        }

        // POST: DangKyKhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaDk,MaBn,NgayDangKi,NgayDenKham,MaKhoa,TrangThai")] DangKyKham dangKyKham)
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
            ViewData["MaKhoa"] = new SelectList(_context.KhoaKhamBenhs, "MaKhoa", "MaKhoa", dangKyKham.MaKhoa);
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
                .Include(d => d.MaKhoaNavigation)
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
