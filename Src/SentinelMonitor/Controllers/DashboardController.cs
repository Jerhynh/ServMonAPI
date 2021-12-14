using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibAtlas.Authentication;

namespace SentinelMonitor.Controllers
{
    public class DashboardController : Controller
    {
        public readonly string conStr = AppAssets.conStr;

        // GET: DashboardController
        public async Task<ActionResult> Index()
        {
            if (HttpContext.Session.GetString("AuthOpCode") == "Authenticated")
            {
                ViewBag.UsersName = AppAssets.ToUpperFirstCharacter(await UserAssetRetrieval.GetNameFromTokenAsync(HttpContext.Session.GetString("Authorization"), conStr));
                return View();
            }
            return RedirectToAction("Index", "Auth");
        }
    }
}
