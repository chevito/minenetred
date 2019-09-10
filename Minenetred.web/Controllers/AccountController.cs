using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Minenetred.web.Context;
using Minenetred.web.Context.ContextModels;
using Minenetred.web.Infrastructure;

namespace Minenetred.web.Controllers
{
    public class AccountController : Controller
    {
        private readonly MinenetredContext _context;
        private readonly IHashHelper _hashHelper;
        public AccountController(MinenetredContext context, IHashHelper hashHelper)
        {
            _context = context;
            _hashHelper = hashHelper;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);    
        }

        public IActionResult Login()
        {
            if (!User.Identity.IsAuthenticated)
                return View();
            return RedirectToAction("GetProjectsAsync", "Projects");

        }

        [HttpPost]
        public IActionResult Login(string userName, string password)
        {
            if (!string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(password))
            {
                return RedirectToAction("Login");
            }

            var hashedPassword = _hashHelper.GetHashString(password);
            var userId = _context.Users.SingleOrDefault(c =>
            c.UserName == userName && c.Password == hashedPassword)
                .UserId;

            if(userId == null)
            {
                return RedirectToAction("Login");
            }

            var identity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, userName), 
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            }, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");
           
        }
        [HttpPost]
        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        public IActionResult Register()
        {
            if (!User.Identity.IsAuthenticated)
                return View();
            return RedirectToAction("GetProjectsAsync", "Projects");
        }
        [HttpPost]
        public IActionResult Register(string userName, string password)
        {
            if (!string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(password))
            {
                return RedirectToAction("Login");
            }

            var validateUnique = _context.Users.SingleOrDefault(c=>c.UserName==userName);
            if (validateUnique != null)
            {
                throw new Exception("User already exist");
            }

            var hashedPassword = _hashHelper.GetHashString(password);
            var user = new User()
            {
                UserName = userName,
                Password = hashedPassword,
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}