using CourseWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace CourseWork.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
            {
                return Redirect(returnUrl ?? "/");
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

            EntryHistory entry = new EntryHistory
            {
                Email = dbUser.Email,
                EntryDate = DateTime.Now
            };
            _context.EntryHistories.Add(entry);
            _context.SaveChanges();

            return Redirect(returnUrl ?? "/");
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
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("userId", user.Id.ToString())
                };

                EntryHistory entry = new EntryHistory
                {
                    Email = user.Email,
                    EntryDate = DateTime.Now
                };
                _context.EntryHistories.Add(entry);
                _context.SaveChanges();

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

        public IActionResult Account(int page = 1)
        {
            User user = _context.Users.Where(u => u.Id == int.Parse(HttpContext.User.FindFirst("userId").Value)).First();
            var issues = _context.IssueHistories.Where(e => e.UserId == user.Id).ToList();

            int pageSize = 10;
            var count = issues.Count();
            var items = issues.Skip((page - 1) * pageSize).Take(pageSize);

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            AccountViewModel viewModel = new AccountViewModel(items, pageViewModel, user);

            return View(viewModel);
        }

        public IActionResult Entries(int page = 1)
        {
            User user = _context.Users.Where(u => u.Id == int.Parse(HttpContext.User.FindFirst("userId").Value)).First();
            var entries = _context.EntryHistories.Where(e => e.Email == user.Email).ToList();

            int pageSize = 10;
            var count = entries.Count();
            var items = entries.Skip((page - 1) * pageSize).Take(pageSize);

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            EntriesViewModel viewModel = new EntriesViewModel(items, pageViewModel);

            return View(viewModel);
        }
    }
}