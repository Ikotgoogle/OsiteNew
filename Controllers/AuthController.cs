using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OsiteNew.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace OsiteNew.Controllers {
    public class AuthController : Controller {
        private MyAppContext _context;
        public AuthController(MyAppContext context) {
            _context = context;
        }

        private async Task Authenticate(User user) {

            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name),
                };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public IActionResult Registration() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(User regUser) {
            if(ModelState.IsValid) {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == regUser.Email);
                if(user == null) {
                    user = new User {
                        Name = regUser.Name,
                        LastName = regUser.LastName,
                        Birthday = regUser.Birthday,
                        PhoneNumber = regUser.PhoneNumber,
                        Email = regUser.Email,
                        Password = regUser.Password,
                        Address = regUser.Address,
                        About = regUser.About,
                        Nickname = regUser.Nickname
                    };

                    Role role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "user");
                    if(user.Role == null)
                        user.Role = role;

                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    await Authenticate(user);

                    return RedirectToAction("HomePage", "Home");
                } else
                    ModelState.AddModelError("", "Некорректные логин или пароль");
            }
            return View(regUser);
        }

        public IActionResult Login() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel logUser) {
            if(ModelState.IsValid) {
                //User user = _context.Users.Where(u => u.Email == logUser.Email && u.Password == logUser.Password) as User;
                User user = await _context.Users.Include(u=>u.Role).FirstOrDefaultAsync(u=> u.Email == logUser.Email && u.Password == logUser.Password);
                if(user != null) {
                    await Authenticate(user); // аутентификация

                    return RedirectToAction("HomePage", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(logUser);
        }

        [Authorize()]
        public async Task<IActionResult> Logout() {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("MianPage", "Home");
        }
    }
}
