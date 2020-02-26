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
    public class CompactDiscsController : Controller
    {
        private readonly CompactDiscProjectContext _context;

        public CompactDiscsController(CompactDiscProjectContext context)
        {
            _context = context;
        }

        // GET: CompactDiscs
        public async Task<IActionResult> Index(string artistName, string searchString)
        {
            IQueryable<string> artistQuery = from a in _context.Artist
                                            orderby a.Name
                                            select a.Name;

            var compactDiscs = from c in _context.CompactDisc
                         select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                compactDiscs = compactDiscs.Where(s => s.Name.Contains(searchString) || s.Artist.Name.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(artistName))
            {
                compactDiscs = compactDiscs.Where(x => x.Artist.Name == artistName);
            }

            var compactDiscArtistVM = new CompactDiscArtistViewModel
            {
                Artists = new SelectList(await artistQuery.Distinct().ToListAsync()),
                CompactDiscs = await compactDiscs.Include(c => c.Artist).ToListAsync()
            };

            return View(compactDiscArtistVM);
        }

        // GET: CompactDiscs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compactDisc = await _context.CompactDisc
                .Include(c => c.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (compactDisc == null)
            {
                return NotFound();
            }

            return View(compactDisc);
        }

        // GET: CompactDiscs/Create
        public IActionResult Create()
        {
            ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "Name");
            return View();
        }

        // POST: CompactDiscs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ReleaseDate,ArtistId")] CompactDisc compactDisc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compactDisc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "Name", compactDisc.ArtistId);
            return View(compactDisc);
        }

        // GET: CompactDiscs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compactDisc = await _context.CompactDisc.FindAsync(id);
            if (compactDisc == null)
            {
                return NotFound();
            }
            ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "Name", compactDisc.ArtistId);
            return View(compactDisc);
        }

        // POST: CompactDiscs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ReleaseDate,ArtistId")] CompactDisc compactDisc)
        {
            if (id != compactDisc.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compactDisc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompactDiscExists(compactDisc.Id))
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
            ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "Name", compactDisc.ArtistId);
            return View(compactDisc);
        }

        // GET: CompactDiscs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compactDisc = await _context.CompactDisc
                .Include(c => c.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (compactDisc == null)
            {
                return NotFound();
            }

            return View(compactDisc);
        }

        // POST: CompactDiscs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var compactDisc = await _context.CompactDisc.FindAsync(id);
            _context.CompactDisc.Remove(compactDisc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompactDiscExists(int id)
        {
            return _context.CompactDisc.Any(e => e.Id == id);
        }
    }
}
