using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SentinelMonitor.Models;
using LibAtlas.Authentication;
using LibAtlas.Enums;

namespace SentinelMonitor.Controllers
{
    public class AuthController : Controller
    {
        public readonly string conStr = AppAssets.conStr;

        // GET: AuthController
        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("SignIn");
        }

        [HttpGet]
        public ActionResult SignIn()
        {
            if (TempData.TryGetValue("LogoutMsg", out var logoutMsg)) // If a logout msg is present display it.
            {
                ViewBag.LoginStatus = logoutMsg;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AuthUser(AuthModel model)
        {
            var res = await Authentication.AuthUserAsync(model.Username, model.Password, conStr);
            if (res == AuthOpCode.AuthenticationSuccessful)
            {
                var UserToken = await UserAssetRetrieval.GetUserTokenAsync(model.Username, conStr);
                var UserRole = await UserAssetRetrieval.GetUserRoleAsync(UserToken, conStr);
                ViewBag.LoginStatus = "Success!";
                HttpContext.Session.SetString("Authorization", UserToken);
                HttpContext.Session.SetString("AuthOpCode", "Authenticated");
                if (UserRole == "Developer" || UserRole == "Administrator") 
                {
                    return RedirectToAction("Index","Dashboard");
                }
                else
                {
                    ViewBag.LoginStatus = "You are not authorized to access this resource!";
                    return View("SignIn", model);
                }
            }else if (res == AuthOpCode.AuthenticationDisabled)
            {
                ViewBag.LoginStatus = "Authentication failed because the requested account is disabled!";
                return View("SignIn", model);
            }else if (res == AuthOpCode.AuthenticationFailed)
            {
                ViewBag.LoginStatus = "Authentication failed, please check the entered information and try again, if the problem persists contact support.";
                return View("SignIn", model);
            }
            else
            {
                ViewBag.LoginStatus = "An error occured while authenticating!";
                return View("SignIn", model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData.Add("LogoutMsg", "You have been logged out successfully!");
            return RedirectToAction("SignIn");
        }
    }
}
