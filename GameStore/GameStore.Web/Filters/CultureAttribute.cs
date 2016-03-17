namespace GameStore.Web.Filters
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;
    using System.Web.Mvc;

    public class CultureAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var cultureCookie = filterContext.HttpContext.Request.Cookies["lang"];
            var cultureName = cultureCookie != null ? cultureCookie.Value : "en";

            var cultures = new List<string> { "en", "ru" };
            if (!cultures.Contains(cultureName))
            {
                cultureName = "en";
            }

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);
        }
    }
}