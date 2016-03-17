namespace GameStore.Web
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using AutoMapper;

    using GameStore.BLL.Infrastructure;
    using GameStore.Web.Areas.Payment.Models;
    using GameStore.Web.Filters;
    using GameStore.Web.ModelBinders;
    using GameStore.Web.Utils;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            GlobalFilters.Filters.Add(new LogAttribute());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            System.Web.Mvc.ModelBinders.Binders.Add(typeof(OrderViewModel), new BasketBinder());
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;

            Mapper.Initialize(
                cfg =>
                    {
                        cfg.AddProfile(new ApplicationMappingProfile());
                        cfg.AddProfile(new WebMappingProfile());
                    });
        }
    }
}