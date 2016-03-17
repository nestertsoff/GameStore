namespace GameStore.Web.Strategies.PaymentStrategy.Concrete
{
    using System;
    using System.Web.Mvc;

    using GameStore.Web.Areas.Payment.Models;
    using GameStore.Web.Areas.Payment.Models.PaymentTypes;
    using GameStore.Web.Strategies.PaymentStrategy.Abstract;

    public class VisaStrategy : IPaymentStrategy
    {
        public ActionResult PaymentView(OrderViewModel model)
        {
            var visa = new VisaViewModel()
            {
                CardNumber = 1234856923578954,
                DateOfExpity = new DateTime(2018, 01, 01),
                CardVerificationValue = 111,
                CardHoldersName = "John Smith"
            };
            return new ViewResult()
            {
                ViewName = "Visa",
                ViewData = new ViewDataDictionary(visa),
            };
        }
    }
}