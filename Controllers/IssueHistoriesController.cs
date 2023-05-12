using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CourseWork;
using CourseWork.Models;
using Microsoft.AspNetCore.Authorization;

namespace CourseWork.Controllers
{
    [Authorize(Roles = "Admin")]
    public class IssueHistoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IssueHistoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: IssueHistories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.IssueHistories.Include(i => i.Book).Include(i => i.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: IssueHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.IssueHistories == null)
            {
                return NotFound();
            }

            var issueHistory = await _context.IssueHistories
                .Include(i => i.Book)
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issueHistory == null)
            {
                return NotFound();
            }

            return View(issueHistory);
        }

        // GET: IssueHistories/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: IssueHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookId,UserId,IssueDate,EstimatedReturnDate,FactReturnDate")] IssueHistory issueHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(issueHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", issueHistory.BookId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", issueHistory.UserId);
            return View(issueHistory);
        }

        // GET: IssueHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.IssueHistories == null)
            {
                return NotFound();
            }

            var issueHistory = await _context.IssueHistories.FindAsync(id);
            if (issueHistory == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", issueHistory.BookId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", issueHistory.UserId);
            return View(issueHistory);
        }

        // POST: IssueHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookId,UserId,IssueDate,EstimatedReturnDate,FactReturnDate")] IssueHistory issueHistory)
        {
            if (id != issueHistory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issueHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueHistoryExists(issueHistory.Id))
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
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", issueHistory.BookId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", issueHistory.UserId);
            return View(issueHistory);
        }

        // GET: IssueHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.IssueHistories == null)
            {
                return NotFound();
            }

            var issueHistory = await _context.IssueHistories
                .Include(i => i.Book)
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issueHistory == null)
            {
                return NotFound();
            }

            return View(issueHistory);
        }

        // POST: IssueHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.IssueHistories == null)
            {
                return Problem("Entity set 'ApplicationDbContext.IssueHistories'  is null.");
            }
            var issueHistory = await _context.IssueHistories.FindAsync(id);
            if (issueHistory != null)
            {
                _context.IssueHistories.Remove(issueHistory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssueHistoryExists(int id)
        {
          return (_context.IssueHistories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
