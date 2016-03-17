namespace GameStore.Tests.WebTests
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Routing;

    using GameStore.Web;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class RouteTests
    {
        private static RouteCollection routes;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
        }

        [TestMethod]
        public void Get_All_Games_Route_Test()
        {
            TestRouteMatch("~/", "Game", "Games");
        }

        [TestMethod]
        public void Create_Game_Route_Test()
        {
            TestRouteMatch("~/games/new", "Game", "Create");
        }

        [TestMethod]
        public void Update_Game_Route_Test()
        {
            TestRouteMatch("~/games/gamekey/update", "Game", "Edit");
        }

        public void Delete_Game_Route_Test()
        {
            TestRouteMatch("~/games/gamekey/remove", "Game", "Delete");
        }

        [TestMethod]
        public void Create_New_Comment_Route_Test()
        {
            TestRouteMatch("~/game/gamekey/newcomment", "Comment", "Comments");
        }

        [TestMethod]
        public void Go_To_Basket_Route_Test()
        {
            TestRouteMatch("~/basket", "Order", "Basket");
        }

        [TestMethod]
        public void Get_Publisher_By_Company_Name()
        {
            TestRouteMatch("~/publisher/First%20Company", "Publisher", "Details");
        }

        [TestMethod]
        public void Create_Publisher_Route_Test()
        {
            TestRouteMatch("~/publisher/new", "Publisher", "Create");
        }

        private static HttpContextBase CreateHttpContext(string targetUrl = null, string httpMethod = "GET")
        {
            var mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(m => m.AppRelativeCurrentExecutionFilePath).Returns(targetUrl);
            mockRequest.Setup(m => m.HttpMethod).Returns(httpMethod);

            var mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(m => m.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);

            var mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(m => m.Request).Returns(mockRequest.Object);
            mockContext.Setup(m => m.Response).Returns(mockResponse.Object);

            return mockContext.Object;
        }

        private static bool TestIncomingRouteResult(
            RouteData routeResult,
            string controller,
            string action,
            object propertySet = null)
        {
            Func<object, object, bool> valCompare =
                (v1, v2) => StringComparer.InvariantCultureIgnoreCase.Compare(v1, v2) == 0;
            var result = valCompare(routeResult.Values["controller"], controller)
                         && valCompare(routeResult.Values["action"], action);
            if (propertySet != null)
            {
                var propInfo = propertySet.GetType().GetProperties();
                if (
                    propInfo.Any(
                        pi =>
                        !(routeResult.Values.ContainsKey(pi.Name)
                          && valCompare(routeResult.Values[pi.Name], pi.GetValue(propertySet, null)))))
                {
                    result = false;
                }
            }

            return result;
        }

        private static void TestRouteMatch(
            string url,
            string controller,
            string action,
            object routeProperties = null,
            string httpMethod = "GET")
        {
            // Act
            var result = routes.GetRouteData(CreateHttpContext(url, httpMethod));

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestIncomingRouteResult(result, controller, action, routeProperties));
        }
    }
}