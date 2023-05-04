using CourseWork.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace CourseWork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

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

        public IActionResult Books(int Form, int Genus, int Content, int Style)
        {
            var books = _context.Books.ToList();
            if (Form != 0 ) {
                books = books.Where(e => e.FormId == Form).ToList();
            }
            if (Genus != 0) {
                books = books.Where(e => e.GenusId == Genus).ToList();
            }
            if (Content != 0) {
                books = books.Where(e => e.ContentId == Content).ToList();
            }
            if (Style != 0) {
                books = books.Where(e => e.StyleId == Style).ToList();
            }
            return View(books);
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
            Review review = new Review{
                BookId = Id,
                UserId = int.Parse(HttpContext.User.FindFirst("userId").Value),
                Text = Text,
                Rating = Rating
            };
            // review.UserId = int.Parse(HttpContext.User.FindFirst("studentId").Value);
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return Redirect($"~/Home/Book/{Id}");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
            {
                return Redirect(returnUrl??"/");
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string returnUrl, LoginViewModel login)
        {
            User dbUser = _context.Users.FirstOrDefault(u => u.Email == login.Email && u.Password == login.Password);

            if (dbUser == null)
            {
                ModelState.AddModelError("Email", "Введен неверный логин или пароль");
                return View(login);
            }

            var claims = new List<Claim> {
                new Claim(ClaimTypes.Email, dbUser.Email),
                new Claim("userId", dbUser.Id.ToString())
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return Redirect(returnUrl??"/");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            User dbUser = _context.Users.FirstOrDefault(u => u.Email == register.Email);
            if (ModelState.IsValid && dbUser == null)
            {
                User user = new User
                {
                    Name = register.Name,
                    Email = register.Email,
                    Password = register.Password,
                    Lastname = register.Lastname,
                    Patronymic = register.Patronymic,
                    Gender = register.Gender,
                    Address = register.Address,
                    Age = register.Age,
                    Phone = register.Phone,
                    RoleId = 1
                };
                _context.Users.Add(user);
                _context.SaveChanges();
                var claims = new List<Claim> {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("userId", user.Id.ToString())
            };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Books");
            }

            if (dbUser != null)
            {
                ModelState.AddModelError("Email", "Пользователь с таким адресом электронной почты уже существует");
            }

            return View(register);
        }
    }
}