namespace GameStore.Web.Strategies.PaymentStrategy.Abstract
{
    using System.Web.Mvc;

    using GameStore.Web.Areas.Payment.Models;

    public interface IPaymentStrategy
    {
        ActionResult PaymentView(OrderViewModel model);
    }
}
