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
    public class BenhNhansController : Controller
    {
        private readonly RegisterPatientOnlineContext _context;

        public BenhNhansController(RegisterPatientOnlineContext context)
        {
            _context = context;
        }

        // GET: BenhNhans
        public async Task<IActionResult> Index()
        {
            var registerPatientOnlineContext = _context.BenhNhans.Include(b => b.MaBhytNavigation).Include(b => b.MaTkNavigation);
            return View(await registerPatientOnlineContext.ToListAsync());
        }

        // GET: BenhNhans/Details/5
        // GET: BenhNhans/Details/username
        public async Task<IActionResult> Details(string username)
        {
            if (string.IsNullOrEmpty(username) || _context.BenhNhans == null)
            {
                return NotFound();
            }

            // Find the account associated with the username
            var userAccount = await _context.TaiKhoans
                .FirstOrDefaultAsync(t => t.TenDangNhap == username);

            if (userAccount == null)
            {
                return NotFound(); // Username does not exist
            }

            // Find the BenhNhan associated with the account
            var benhNhan = await _context.BenhNhans
                .Include(b => b.MaBhytNavigation)
                .Include(b => b.MaTkNavigation)
                .FirstOrDefaultAsync(b => b.MaTk == userAccount.MaTk); // Link by account ID

            if (benhNhan == null)
            {
                return NotFound(); // No patient record linked to this account
            }

            return View(benhNhan);
        }


        // GET: BenhNhans/Create
        public IActionResult Create()
        {
            ViewData["MaBhyt"] = new SelectList(_context.BaoHiemYTes, "MaBhyt", "MaBhyt");
            ViewData["MaTk"] = new SelectList(_context.TaiKhoans, "MaTk", "MaTk");
            return View();
        }

        // POST: BenhNhans/Create
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

        // GET: BenhNhans/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.BenhNhans == null)
            {
                return NotFound();
            }

            var benhNhan = await _context.BenhNhans
                    .Include(b => b.MaTkNavigation) // Load TaiKhoan details
                    .Include(b => b.MaBhytNavigation) // Load BaoHiemYTes details
                    .FirstOrDefaultAsync(m => m.MaBn == id);
            if (benhNhan == null)
            {
                return NotFound();
            }

            // No need to pass SelectList for MaBhyt since it's read-only
            return View(benhNhan);
        }

        // POST: BenhNhans/Edit/5
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
                var username = HttpContext.Session.GetString("UserName");
                return RedirectToAction("Details", "BenhNhans", new { username });
            }

            return View(benhNhan);
        }


        // GET: BenhNhans/Delete/5
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

        // POST: BenhNhans/Delete/5
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
