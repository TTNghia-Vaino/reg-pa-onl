using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Register_Patient_Online.Models;

namespace Register_Patient_Online.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BenhNhansController : Controller
    {
        private readonly RegisterPatientOnlineContext _context;

        public BenhNhansController(RegisterPatientOnlineContext context)
        {
            _context = context;
        }

        // GET: Admin/BenhNhans
        public async Task<IActionResult> Index()
        {
            var registerPatientOnlineContext = _context.BenhNhans.Include(b => b.MaBhytNavigation).Include(b => b.MaTkNavigation);
            return View(await registerPatientOnlineContext.ToListAsync());
        }

        // GET: Admin/BenhNhans/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.BenhNhans == null)
            {
                return NotFound();
            }

            var benhNhan = await _context.BenhNhans
                .Include(b => b.MaBhytNavigation)
                .Include(b => b.MaTkNavigation)
                .FirstOrDefaultAsync(m => m.MaBn == id);
            if (benhNhan == null)
            {
                return NotFound();
            }

            return View(benhNhan);
        }

        // GET: Admin/BenhNhans/Create
        public IActionResult Create()
        {
            ViewData["MaBhyt"] = new SelectList(_context.BaoHiemYTes, "MaBhyt", "MaBhyt");
            ViewData["MaTk"] = new SelectList(_context.TaiKhoans, "MaTk", "MaTk");
            return View();
        }

        // POST: Admin/BenhNhans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaBn,MaTk,Hoten,Sdt,Email,DiaChi,Cccd,MaBhyt")] BenhNhan benhNhan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(benhNhan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaBhyt"] = new SelectList(_context.BaoHiemYTes, "MaBhyt", "MaBhyt", benhNhan.MaBhyt);
            ViewData["MaTk"] = new SelectList(_context.TaiKhoans, "MaTk", "MaTk", benhNhan.MaTk);
            return View(benhNhan);
        }

        // GET: Admin/BenhNhans/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.BenhNhans == null)
            {
                return NotFound();
            }

            var benhNhan = await _context.BenhNhans.FindAsync(id);
            if (benhNhan == null)
            {
                return NotFound();
            }
            ViewData["MaBhyt"] = new SelectList(_context.BaoHiemYTes, "MaBhyt", "MaBhyt", benhNhan.MaBhyt);
            ViewData["MaTk"] = new SelectList(_context.TaiKhoans, "MaTk", "MaTk", benhNhan.MaTk);
            return View(benhNhan);
        }

        // POST: Admin/BenhNhans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaBn,MaTk,Hoten,Sdt,Email,DiaChi,Cccd,MaBhyt")] BenhNhan benhNhan)
        {
            if (id != benhNhan.MaBn)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(benhNhan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BenhNhanExists(benhNhan.MaBn))
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
            ViewData["MaBhyt"] = new SelectList(_context.BaoHiemYTes, "MaBhyt", "MaBhyt", benhNhan.MaBhyt);
            ViewData["MaTk"] = new SelectList(_context.TaiKhoans, "MaTk", "MaTk", benhNhan.MaTk);
            return View(benhNhan);
        }

        // GET: Admin/BenhNhans/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.BenhNhans == null)
            {
                return NotFound();
            }

            var benhNhan = await _context.BenhNhans
                .Include(b => b.MaBhytNavigation)
                .Include(b => b.MaTkNavigation)
                .FirstOrDefaultAsync(m => m.MaBn == id);
            if (benhNhan == null)
            {
                return NotFound();
            }

            return View(benhNhan);
        }

        // POST: Admin/BenhNhans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.BenhNhans == null)
            {
                return Problem("Entity set 'RegisterPatientOnlineContext.BenhNhans'  is null.");
            }
            var benhNhan = await _context.BenhNhans.FindAsync(id);
            if (benhNhan != null)
            {
                _context.BenhNhans.Remove(benhNhan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BenhNhanExists(string id)
        {
          return (_context.BenhNhans?.Any(e => e.MaBn == id)).GetValueOrDefault();
        }
    }
}
