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
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Principal;

namespace Workers.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
    /*    private WorkerContext db;*/
        public IEFGenericRepository<User> Userrepository { get; set; }
        public AccountController(WorkerContext context, IEFGenericRepository<User> userrepository)
        {
       /*     db = context;*/
            Userrepository = userrepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
       [Route("/Account/Login")]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home"); 
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                /* User user =await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);*/
                User user =await Userrepository.FindAsyncMethod(u => u.Email == model.Email && u.Password == model.Password);
                if (user!=null)
                {
                    await Authenticate(model.Email); 

                    return RedirectToAction("Index", "Home");

                }
                ModelState.AddModelError("", "incorrect email or password");
            }
            return View(model);
        }
        [HttpGet]
        [Route("/Account/Register")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Account/Register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                /*              User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);*/
                User user = await Userrepository.FindAsyncMethod(u => u.Email == model.Email );
                if (user == null)
                {

                    /* db.Users.Add(new User { Email = model.Email, Password = model.Password });*/
                    /* await db.SaveChangesAsync();*/
                   await Userrepository.AddAsyn(new User { Email = model.Email, Password = model.Password,Id=Guid.NewGuid() });
                    await Authenticate(model.Email); 

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "incorrect email or password");
                }
            }
            return View(model);
        }

        [HttpGet]
        [Route("/Account/Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
         
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}