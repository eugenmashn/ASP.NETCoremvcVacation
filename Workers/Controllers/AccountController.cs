using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using DataAccessLayer.Repository;
using Workers.ModelsView;
using Workers.Models_View;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity;
using Workers.Models;

namespace Workers.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        /*    private WorkerContext db;*/
        private readonly UserManager<UserAuthentication> _userManager;
        private readonly SignInManager<UserAuthentication> _signInManager;
        public AccountController(UserManager<UserAuthentication> userManager, SignInManager<UserAuthentication> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                UserAuthentication userAuthentication = new UserAuthentication { Email = model.Email, UserName = model.Email,FirstName=model.Firstname,LastName=model.LastName };

                var result = await _userManager.CreateAsync(userAuthentication, model.Password);
                if (result.Succeeded)
                {

                    await _signInManager.SignInAsync(userAuthentication, false);
                    await _userManager.AddToRoleAsync(userAuthentication, "user");
                    return Redirect("/");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginModel { ReturnUrl = returnUrl });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
               
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return Redirect("/");
                    }
                }
                else
                {
                    if ( await _userManager.FindByEmailAsync(model.Email) == null)
                    {
                        ModelState.AddModelError("Email", " email is false");
                    }
                    else
                    { 
                       ModelState.AddModelError("Password", "passwors  is false");
                    }
                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Account/Logout")]
        public async Task<IActionResult> Logout()
        {
            
            await _signInManager.SignOutAsync();
            return Redirect("/");
        }
    }
}