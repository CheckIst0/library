using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CourseWork;
using CourseWork.Models;

namespace CourseWork.Controllers
{
    public class AwardsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AwardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Awards/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Authors.OrderBy(e => e.Name), "Id", "Name");
            return View();
        }

        // POST: Awards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Award award)
        {
            if (award.ReceiptDate < DateTime.Today && award.AuthorId != 0)
            {
                _context.Add(award);
                await _context.SaveChangesAsync();
                return Redirect("~/Authors/Details/"+ award.AuthorId);
            }
            if (award.ReceiptDate < DateTime.Today)
            {
                ModelState.AddModelError("ReceiptDate", "Некорректная дата");
            }
            return View(award);
        }

        // GET: Awards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Awards == null)
            {
                return NotFound();
            }

            var award = await _context.Awards.FindAsync(id);
            if (award == null)
            {
                return NotFound();
            }
            return View(award);
        }

        // POST: Awards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Award award)
        {
            if (id != award.Id)
            {
                return NotFound();
            }

            if (award.ReceiptDate < DateTime.Today && award.AuthorId != 0)
            {
                try
                {
                    _context.Update(award);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AwardExists(award.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("~/Authors/Details/"+ award.AuthorId);
            }
            return View(award);
        }

        // GET: Awards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Awards == null)
            {
                return NotFound();
            }

            var award = await _context.Awards
                .Include(a => a.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (award == null)
            {
                return NotFound();
            }

            return View(award);
        }

        // POST: Awards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Awards == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Awards'  is null.");
            }
            var award = await _context.Awards.FindAsync(id);
            if (award != null)
            {
                _context.Awards.Remove(award);
            }

            await _context.SaveChangesAsync();
            return Redirect("~/Authors/Details/"+ award?.AuthorId);
        }

        private bool AwardExists(int id)
        {
            return (_context.Awards?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
