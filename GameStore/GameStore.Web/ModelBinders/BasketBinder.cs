namespace GameStore.Web.ModelBinders
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using GameStore.Web.Areas.Payment.Models;

    public class BasketBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            const int CustomerIdStub = 1;

            if (controllerContext.HttpContext.Session != null)
            {
                var model = controllerContext.HttpContext.Session["Order"] as OrderViewModel;

                if (model == null)
                {
                    model = new OrderViewModel
                                {
                                    OrderDate = DateTime.UtcNow, 
                                    CustomerId = CustomerIdStub, 
                                    OrderDetailsList = new List<OrderDetailsViewModel>()
                                };
                    controllerContext.HttpContext.Session["Order"] = model;
                }
            }

            if (controllerContext.HttpContext.Session != null)
            {
                return (OrderViewModel)controllerContext.HttpContext.Session["Order"];
            }

            return new OrderViewModel();
        }
    }
}