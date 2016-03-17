namespace GameStore.Web.Strategies.PaymentStrategy.Concrete
{
    using System;
    using System.Web.Mvc;

    using GameStore.Web.Areas.Payment.Models;
    using GameStore.Web.Areas.Payment.Models.PaymentTypes;
    using GameStore.Web.Strategies.PaymentStrategy.Abstract;

    public class BankStrategy : IPaymentStrategy
    {
        public ActionResult PaymentView(OrderViewModel model)
        {
            var invoice = new BankViewModel()
            {
                InvoiceDate = DateTime.UtcNow,
                InvoiceNumber = 1616848,
                ClientAdress = "USA, st-d Uneva, app 18",
                ClientName = "Vova Sharikov",
                ClientPhone = "350-48-88-80",
                ClientEmail = "VovaSharikov@gmail.com",
                ClientId = "A0789-11",
                ContractorName = "Gineva Areva",
                ContractorAdress = "Backers street 19/7",
                ContractorPhone = "486-479-11-91",
                ContractorEmail = "Gineva@areva.com",
                Order = model
            };

            return new ViewResult()
            {
                ViewName = "Bank",
                ViewData = new ViewDataDictionary(invoice),
            };
        }
    }
}