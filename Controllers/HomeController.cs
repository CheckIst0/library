using CourseWork.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CourseWork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Books(int Form, int Genus, int Content, int Style, int page = 1)
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

        [HttpGet]
        public IActionResult Book(int Id)
        {
            BookViewModel model = new BookViewModel
            {
                Book = _context.Books.FirstOrDefault(e => e.Id == Id),
                Reviews = _context.Reviews.Where(e => e.BookId == Id).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Book(int Id, string Text, int Rating)
        {
            Review review = new Review
            {
                BookId = Id,
                UserId = int.Parse(HttpContext.User.FindFirst("userId").Value),
                Text = Text,
                Rating = Rating
            };

            _context.Reviews.Add(review);
            _context.SaveChanges();
            return Redirect($"~/Home/Book/{Id}");
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteBook(int Id)
        {
            var book = _context.Books.Find(Id);
            _context.Books.Remove(book);
            _context.SaveChanges();
            return Redirect("~/Home/Books");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddBook(Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Add(book);
                _context.SaveChanges();
                return Redirect("~/Home/Books");
            }
            return View();
        }
    }
}