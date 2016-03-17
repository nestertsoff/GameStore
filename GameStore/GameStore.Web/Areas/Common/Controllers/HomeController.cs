namespace GameStore.Web.Areas.Common.Controllers
{
    using System;
    using System.Web;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult ChangeCulture()
        {
            var cultureCookie = Request.Cookies["lang"];
            var cultureName = "en";

            if (cultureCookie != null)
            {
                cultureName = cultureCookie.Value == "en" ? "ru" : "en";
            }

            var urlReferrer = Request.UrlReferrer;
            if (urlReferrer != null)
            {
                var returnUrl = urlReferrer.AbsolutePath;

                cultureCookie = new HttpCookie("lang")
                                    {
                                        HttpOnly = false, 
                                        Value = cultureName, 
                                        Expires = DateTime.Now.AddYears(1)
                                    };

                Response.Cookies.Add(cultureCookie);
                return Redirect(returnUrl);
            }
            return Redirect("/");
        }
    }
}