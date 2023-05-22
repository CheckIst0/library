using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            var applicationDbContext = _context.IssueHistories.Include(i => i.Book).Include(i => i.User).OrderBy(e => e.IssueDate).ThenBy(e => e.Book.Name);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(DateTime factReturnDate, int? id)
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

            try
            {
                issueHistory.FactReturnDate = factReturnDate;
                issueHistory.Book.Quantity++;
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

            return View(issueHistory);
        }

        // GET: IssueHistories/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: IssueHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IssueHistory issueHistory)
        {
            issueHistory.IssueDate = DateTime.Today;
            issueHistory.EstimatedReturnDate = issueHistory.IssueDate.AddDays(14);
            var book = _context.Books.Find(issueHistory.BookId);
            var usersIssuedBooks = _context.IssueHistories
                .Where(e => e.UserId == issueHistory.UserId)
                .Count(e => e.FactReturnDate == null);
            if (issueHistory.BookId != 0 && issueHistory.UserId != 0
            && book?.Quantity > 0 && usersIssuedBooks == 0)
            {
                _context.Add(issueHistory);
                issueHistory.Book.Quantity--;
                _context.Update(issueHistory.Book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            else if (book?.Quantity <= 0)
            {
                ModelState.AddModelError("BookId", "В библиотека нет экзепляров этой книги");
            }
            else if (usersIssuedBooks > 0)
            {
                ModelState.AddModelError("UserId", "Данный пользователь еще не вернул книгу в библиотеку");
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Name", issueHistory.BookId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", issueHistory.UserId);
            return View(issueHistory);
        }

        // // GET: IssueHistories/Edit/5
        // public async Task<IActionResult> Edit(int? id)
        // {
        //     if (id == null || _context.IssueHistories == null)
        //     {
        //         return NotFound();
        //     }

        //     var issueHistory = await _context.IssueHistories.FindAsync(id);
        //     if (issueHistory == null)
        //     {
        //         return NotFound();
        //     }
        //     ViewData["BookId"] = new SelectList(_context.Books, "Id", "Name", issueHistory.BookId);
        //     ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", issueHistory.UserId);
        //     return View(issueHistory);
        // }

        // // POST: IssueHistories/Edit/5
        // // To protect from overposting attacks, enable the specific properties you want to bind to.
        // // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(int id, IssueHistory issueHistory)
        // {
        //     if (id != issueHistory.Id)
        //     {
        //         return NotFound();
        //     }

        //     if (issueHistory.BookId != 0 && issueHistory.UserId != 0 &&
        //     issueHistory.IssueDate < issueHistory.EstimatedReturnDate && (issueHistory.IssueDate < issueHistory.FactReturnDate || issueHistory.FactReturnDate == null))
        //     {
        //         try
        //         {
        //             _context.Update(issueHistory);
        //             await _context.SaveChangesAsync();
        //         }
        //         catch (DbUpdateConcurrencyException)
        //         {
        //             if (!IssueHistoryExists(issueHistory.Id))
        //             {
        //                 return NotFound();
        //             }
        //             else
        //             {
        //                 throw;
        //             }
        //         }
        //         return RedirectToAction(nameof(Index));
        //     }
        //     ViewData["BookId"] = new SelectList(_context.Books, "Id", "Name", issueHistory.BookId);
        //     ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", issueHistory.UserId);
        //     return View(issueHistory);
        // }

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
