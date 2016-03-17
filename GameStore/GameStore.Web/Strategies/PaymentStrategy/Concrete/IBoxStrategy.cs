namespace GameStore.Web.Strategies.PaymentStrategy.Concrete
{
    using System.Linq;
    using System.Web.Mvc;

    using GameStore.Web.Areas.Payment.Models;
    using GameStore.Web.Areas.Payment.Models.PaymentTypes;
    using GameStore.Web.Strategies.PaymentStrategy.Abstract;

    public class IBoxStrategy : IPaymentStrategy
    {
        public ActionResult PaymentView(OrderViewModel model)
        {
            var iBox = new IBoxViewModel
                           {
                               InvoiceNumber = 11111, 
                               AccountNumber = 22222, 
                               Sum = model.OrderDetailsList.Sum(item => item.Price)
                           };

            return new ViewResult { ViewName = "IBox", ViewData = new ViewDataDictionary(iBox) };
        }
    }
}