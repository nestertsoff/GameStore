namespace GameStore.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("User", "User/{action}", new { controller = "User", action = "Users" })
                .DataTokens.Add("area", "User");

            routes.MapRoute("CreatePublisher", "Publisher/New", new { controller = "Publisher", action = "Create" })
                .DataTokens.Add("area", "Common");

            routes.MapRoute("Publisher", "Publisher/{companyname}", new { controller = "Publisher", action = "Details" })
                .DataTokens.Add("area", "Common");

            routes.MapRoute("Comments", "Game/{gamekey}/Comments", new { controller = "Comment", action = "Comments" })
               .DataTokens.Add("area", "Common");

            routes.MapRoute("NewComment", "Game/{gamekey}/NewComment", new { controller = "Comment", action = "Comments" })
                .DataTokens.Add("area", "Common");
            
            routes.MapRoute("CreateGame", "Games/New", new { controller = "Game", action = "Create" })
                .DataTokens.Add("area", "Common");

            routes.MapRoute("DeleteGame", "Games/{gamekey}/Remove", new { controller = "Game", action = "Delete" })
                .DataTokens.Add("area", "Common");

            routes.MapRoute("EditGame", "Games/{gamekey}/Update", new { controller = "Game", action = "Edit" })
                .DataTokens.Add("area", "Common");

            routes.MapRoute("Order", "Order/{action}", new { controller = "Order", action = "Create" })
               .DataTokens.Add("area", "Payment");

            routes.MapRoute("Remove", "{controller}/Remove/{id}", new { action = "Delete" })
                .DataTokens.Add("area", "Common");

            routes.MapRoute("Update", "{controller}/Update/{id}", new { action = "Edit" })
                .DataTokens.Add("area", "Common");

            routes.MapRoute("Games", "Games", new { controller = "Game", action = "Games" })
                .DataTokens.Add("area", "Common");

            routes.MapRoute("Basket", "Basket", new { controller = "Order", action = "Basket" })
                .DataTokens.Add("area", "Payment");

            routes.MapRoute("Game", "{controller}/{gamekey}/{action}", new { controller = "Game", action = "Details" })
                .DataTokens.Add("area", "Common");

            routes.MapRoute("Culture", "Home/{action}", new { controller = "Home", action = "ChangeCulture" })
                .DataTokens.Add("area", "Common");

            routes.MapRoute("Default", "{controller}/{action}", new { controller = "Game", action = "Games" })
                .DataTokens.Add("area", "Common");
        }
    }
}