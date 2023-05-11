using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CourseWork.Models;
using Microsoft.AspNetCore.Authorization;

namespace CourseWork.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Books
        public IActionResult Index(int Form, int Genus, int Content, int Style, int page = 1)
        {
            var books = _context.Books.ToList();
            if (Form != 0)
            {
                books = books.Where(e => e.FormId == Form).ToList();
            }
            if (Genus != 0)
            {
                books = books.Where(e => e.GenusId == Genus).ToList();
            }
            if (Content != 0)
            {
                books = books.Where(e => e.ContentId == Content).ToList();
            }
            if (Style != 0)
            {
                books = books.Where(e => e.StyleId == Style).ToList();
            }

            int pageSize = 10;
            var count = books.Count();
            var items = books.Skip((page - 1) * pageSize).Take(pageSize);

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            BookListViewModel viewModel = new BookListViewModel(items, pageViewModel);

            return View(viewModel);
        }

        // GET: Books/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            BookViewModel model = new BookViewModel
            {
                Book = _context.Books.FirstOrDefault(e => e.Id == id),
                Reviews = _context.Reviews.Where(e => e.BookId == id).ToList()
            };

            if (model.Book == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Details(int id, string Text, int Rating)
        {
            Review review = new Review
            {
                BookId = id,
                UserId = int.Parse(HttpContext.User.FindFirst("userId").Value),
                Text = Text,
                Rating = Rating
            };

            _context.Reviews.Add(review);
            _context.SaveChanges();
            return RedirectToAction(nameof(Details));
        }

        // GET: Books/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Name");
            ViewData["ContentId"] = new SelectList(_context.Set<Content>(), "Id", "Name");
            ViewData["FormId"] = new SelectList(_context.Set<Form>(), "Id", "Name");
            ViewData["GenusId"] = new SelectList(_context.Set<Genus>(), "Id", "Name");
            ViewData["StyleId"] = new SelectList(_context.Set<Style>(), "Id", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,ImageSource,AuthorId,Description,Quantity,FormId,GenusId,ContentId,StyleId")] Book book)
        {
            _context.Add(book);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Books/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = _context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            ViewData["Author"] = new SelectList(_context.Authors, "Id", "Name");
            ViewData["Content"] = new SelectList(_context.Set<Content>(), "Id", "Name");
            ViewData["Form"] = new SelectList(_context.Set<Form>(), "Id", "Name");
            ViewData["Genus"] = new SelectList(_context.Set<Genus>(), "Id", "Name");
            ViewData["Style"] = new SelectList(_context.Set<Style>(), "Id", "Name");
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,ImageSource,AuthorId,Description,Quantity,FormId,GenusId,ContentId,StyleId")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(book);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(book.Id))
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

        // GET: Books/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Content)
                .Include(b => b.Form)
                .Include(b => b.Genus)
                .Include(b => b.Style)
                .FirstOrDefault(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Books'  is null.");
            }
            var book = _context.Books.Find(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
