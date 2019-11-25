using System.Collections.Generic;
using System.Security.Claims;
using CleanArch.Application.Interfaces;
using CleanArch.Application.Security;
using CleanArch.Application.ViewModels.Account;
using CleanArch.Application.ViewModels.User;
using CleanArch.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Mvc.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        #region Register

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
                return View(register);
            CheckUser checkUser = _userService.CheckUser(register.UserName, register.Email);
            if (checkUser != CheckUser.Ok)
            {
                ViewBag.Check = checkUser;
                return View(register);
            }
            var user = new User
            {
                Email = register.Email.Trim().ToLower(),
                Password = PasswordHelper.EncodePasswordMd5(register.Password),
                UserName = register.UserName
            };
            _userService.RegisterUser(user);
            return View("SuccessRegister", register);
        }

        #endregion

        #region Login

        [Route("Login")]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
                return View(login);
            if (!_userService.IsExistUser(login.Email, login.Password))
            {
                ModelState.AddModelError("Email", "User Not Fount ...");
                return View(login);
            }

            //TODO: Login User
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,login.Email),
                new Claim(ClaimTypes.NameIdentifier,login.Email)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties
            {
                IsPersistent = login.RememberMe
            };
            HttpContext.SignInAsync(principal, properties);
            return Redirect(login.ReturnUrl);
        }

        #endregion

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}