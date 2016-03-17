namespace GameStore.Web
{
    using System.Web.Mvc;

    using GameStore.Web.Filters;

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleAllErrorAttribute());
            filters.Add(new CultureAttribute());
        }
    }
}