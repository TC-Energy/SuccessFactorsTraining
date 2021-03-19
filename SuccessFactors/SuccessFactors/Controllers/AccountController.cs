using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.Owin.Security;
using SuccessFactors.Models;
namespace SuccessFactors.Controllers
{
    public class AccountController : Controller
    {
        public void SignIn()
        {
            // Send an OpenID Connect sign-in request.
            if (!Request.IsAuthenticated)
            {
                HttpContext.GetOwinContext().Authentication.Challenge(new AuthenticationProperties { RedirectUri = "/" },
                    OpenIdConnectAuthenticationDefaults.AuthenticationType);
            }
        }

        public void SignOut()
        {
            string callbackUrl = Url.Action("SignOutCallback", "Account", routeValues: null, protocol: Request.Url.Scheme);

            HttpContext.GetOwinContext().Authentication.SignOut(
                new AuthenticationProperties { RedirectUri = callbackUrl },
                OpenIdConnectAuthenticationDefaults.AuthenticationType, CookieAuthenticationDefaults.AuthenticationType);
        }

        public ActionResult SignOutCallback()
        {
            if (Request.IsAuthenticated)
            {
                // Redirect to home page if the user is authenticated.
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("ExternalHomePage");
        }

        public ActionResult ExtSignOut()
        {
            return RedirectToAction("ExternalHomePage", "Account");
        }

        public ActionResult ExternalHomePage()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult ValidateExtUser()
        {
            string username = Request.Form["email"];
            string password = Request.Form["password"];
            bool result = SQLConnection.ValidateExternalUsers(username, password);
            if (result)
            {
                return RedirectToAction("ExtStudents", "ExtHome");
            }
            else
            {
                return RedirectToAction("ExternalHomePage", "Account");
            }

        }
    }
}
