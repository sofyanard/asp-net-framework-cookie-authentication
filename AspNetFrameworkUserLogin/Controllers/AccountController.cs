using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
// using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Host.SystemWeb;
using AspNetFrameworkUserLogin.Models;

namespace AspNetFrameworkUserLogin.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationDBContext db = new ApplicationDBContext();

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(UserLoginModel userLoginModel)
        {
            if (ModelState.IsValid)
            {
                var user = await db.ApplicationUsers
                    .AsNoTracking()
                    .Where(a => a.UserName.ToUpper() == userLoginModel.UserName.ToUpper())
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid User.");
                    return View(userLoginModel);
                }

                CustomPasswordHasher customPasswordHasher = new CustomPasswordHasher();

                if (!customPasswordHasher.VerifyPassword(user.Password, userLoginModel.Password))
                {
                    ModelState.AddModelError(string.Empty, "Invalid Password.");
                    return View(userLoginModel);
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("FullName", user.FullName)
                };

                var claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie");

                var context = Request.GetOwinContext();
                var authManager = context.Authentication;

                authManager.SignIn(new AuthenticationProperties { IsPersistent = false }, claimsIdentity);

                return RedirectToAction("Index", "Home");
            }

            return View(userLoginModel);
        }

        [HttpPost]
        public async Task<ActionResult> Logout()
        {
            var context = Request.GetOwinContext();
            var authManager = context.Authentication;

            authManager.SignOut("ApplicationCookie");

            return RedirectToAction("Index", "Home");
        }
    }
}