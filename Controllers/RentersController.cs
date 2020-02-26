using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CompactDiscProject.Data;
using CompactDiscProject.Models;

namespace CompactDiscProject.Controllers
{
    public class RentersController : Controller
    {
        private readonly CompactDiscProjectContext _context;

        public RentersController(CompactDiscProjectContext context)
        {
            _context = context;
        }

        // GET: Renters
        public async Task<IActionResult> Index()
        {
            var compactDiscProjectContext = _context.Renter.Include(r => r.CompactDisc);
            return View(await compactDiscProjectContext.ToListAsync());
        }

        // GET: Renters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var renter = await _context.Renter
                .Include(r => r.CompactDisc)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (renter == null)
            {
                return NotFound();
            }

            return View(renter);
        }

        // GET: Renters/Create
        public IActionResult Create()
        {
            ViewData["CompactDiscId"] = new SelectList(_context.CompactDisc.Where(cd => cd.Renter == null), "Id", "Name");
            return View();
        }

        // POST: Renters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PhoneNumber,Email,CompactDiscId,RentalDate")] Renter renter)
        {
            if (ModelState.IsValid)
            {
                renter.RentalDate = DateTime.Now;
                _context.Add(renter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompactDiscId"] = new SelectList(_context.CompactDisc.Where(cd => cd.Renter == null), "Id", "Name", renter.CompactDiscId);
            return View(renter);
        }

        // GET: Renters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var renter = await _context.Renter.FindAsync(id);
            if (renter == null)
            {
                return NotFound();
            }
            ViewData["CompactDiscId"] = new SelectList(_context.CompactDisc.Where(cd => cd.Renter == null), "Id", "Name", renter.CompactDiscId);
            return View(renter);
        }

        // POST: Renters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PhoneNumber,Email,CompactDiscId,RentalDate")] Renter renter)
        {
            if (id != renter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(renter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RenterExists(renter.Id))
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
            ViewData["CompactDiscId"] = new SelectList(_context.CompactDisc.Where(cd => cd.Renter == null), "Id", "Name", renter.CompactDiscId);
            return View(renter);
        }

        // GET: Renters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var renter = await _context.Renter
                .Include(r => r.CompactDisc)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (renter == null)
            {
                return NotFound();
            }

            return View(renter);
        }

        // POST: Renters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var renter = await _context.Renter.FindAsync(id);
            _context.Renter.Remove(renter);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RenterExists(int id)
        {
            return _context.Renter.Any(e => e.Id == id);
        }
    }
}
