
namespace GameStore.Web.Filters
{
    using System.Diagnostics;
    using System.Web.Mvc;

    using NLog;

                public class LogAttribute : IActionFilter
    {
                                private readonly ILogger _logger;

                                private readonly Stopwatch watch = new Stopwatch();

                                public LogAttribute()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

                                                        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _logger.Info("Request IP: " + filterContext.RequestContext.HttpContext.Request.UserHostAddress);
            watch.Start();
        }

                                                        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            watch.Stop();
            _logger.Trace(
                "Controller: " + filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + " Action: "
                + filterContext.ActionDescriptor.ActionName + " Time: " + watch.ElapsedMilliseconds);
        }
    }
}