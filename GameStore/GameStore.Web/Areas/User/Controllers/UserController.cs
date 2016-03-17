namespace GameStore.Web.Areas.User.Controllers
{
    using System.Web.Mvc;

    using GameStore.Web.Areas.User.Models;

    public class UserController : Controller
    {
        public ActionResult Ban()
        {
            return View("Ban", BanType.Day);
        }

        [HttpPost]
        public ActionResult Ban(object ban)
        {
            return RedirectToAction("Games", "Game", new { area = "Common" });
        }
    }
}