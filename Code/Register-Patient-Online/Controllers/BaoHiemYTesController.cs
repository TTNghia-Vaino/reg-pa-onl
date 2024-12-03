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
    public class BaoHiemYTesController : Controller
    {
        private readonly RegisterPatientOnlineContext _context;

        public BaoHiemYTesController(RegisterPatientOnlineContext context)
        {
            _context = context;
        }


        // GET: BaoHiemYTes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.BaoHiemYTes == null)
            {
                return NotFound();
            }

            var baoHiemYTe = await _context.BaoHiemYTes
                .FirstOrDefaultAsync(m => m.MaBhyt == id);
            if (baoHiemYTe == null)
            {
                return NotFound();
            }

            return View(baoHiemYTe);
        }

        // GET: BaoHiemYTes/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaBhyt,Ten,GioiTinh,NgayBd,NgayHh,NgaySinh")] BaoHiemYTe baoHiemYTe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(baoHiemYTe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(baoHiemYTe);
        }

        // GET: BaoHiemYTes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.BaoHiemYTes == null)
            {
                return NotFound();
            }

            var baoHiemYTe = await _context.BaoHiemYTes.FindAsync(id);
            if (baoHiemYTe == null)
            {
                return NotFound();
            }
            return View(baoHiemYTe);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaBhyt,Ten,GioiTinh,NgayBd,NgayHh,NgaySinh")] BaoHiemYTe baoHiemYTe)
        {
            if (id != baoHiemYTe.MaBhyt)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(baoHiemYTe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaoHiemYTeExists(baoHiemYTe.MaBhyt))
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
            return View(baoHiemYTe);
        }


        private bool BaoHiemYTeExists(string id)
        {
          return (_context.BaoHiemYTes?.Any(e => e.MaBhyt == id)).GetValueOrDefault();
        }
    }
}
