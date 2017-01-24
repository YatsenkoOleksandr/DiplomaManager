using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DiplomaManager.BLL.DTOs.TeacherDTOs;
using DiplomaManager.BLL.Interfaces;
using DiplomaManager.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private IUserService UserService { get; }

        public AccountController(IUserService userService)
        {
            UserService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var userDto = UserService.GetUser<AdminDTO>(loginViewModel.Login, loginViewModel.Password);
                if (userDto != null)
                {
                    await Authenticate(loginViewModel.Login); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(loginViewModel);
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
                    };

            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            // установка аутентификационных куки
            await HttpContext.Authentication.SignInAsync("Admin", new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("Admin");
            return RedirectToAction(nameof(Login));
        }
    }
}
