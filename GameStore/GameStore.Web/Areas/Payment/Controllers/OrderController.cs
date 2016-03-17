namespace GameStore.Web.Areas.Payment.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using GameStore.BLL.Services.Abstract;
    using GameStore.Web.Areas.Payment.Models;
    using GameStore.Web.Areas.Payment.Models.PaymentTypes;
    using GameStore.Web.Strategies.PaymentStrategy.Abstract;
    using GameStore.Web.Strategies.PaymentStrategy.Concrete;

    public class OrderController : Controller
    {
        private readonly IGameService _gameService;

        public OrderController(IGameService gameService)
        {
            _gameService = gameService;
        }

        private IPaymentStrategy PaymentStrategy { get; set; }

        public ActionResult Basket(OrderViewModel order)
        {
            return View(order);
        }

        public ActionResult Buy(string gameKey, OrderViewModel basket)
        {
            var game = _gameService.GetByKey(gameKey);

            if (basket.OrderDetailsList == null)
            {
                basket.OrderDetailsList = new List<OrderDetailsViewModel>();
            }

            if (basket.OrderDetailsList.FirstOrDefault(x => x.ProductId == game.Id) == null)
            {
                var orderDetails = new OrderDetailsViewModel
                                       {
                                           ProductId = game.Id, 
                                           ProductName = game.Name, 
                                           Price = game.Price, 
                                           Discount = 0, 
                                           Quantity = 1
                                       };

                basket.OrderDetailsList.Add(orderDetails);
            }
            else
            {
                var orderDetailsViewModel = basket.OrderDetailsList.FirstOrDefault(x => x.ProductId == game.Id);
                if (orderDetailsViewModel != null)
                {
                    orderDetailsViewModel.Quantity++;
                }
            }

            return RedirectToAction("Basket", basket);
        }

        public ActionResult Create()
        {
            if (Session["Order"] != null)
            {
                var model = (OrderViewModel)Session["Order"];
                model.PaymentInfo = new List<PaymentTypeInfoViewModel>
                                        {
                                            new PaymentTypeInfoViewModel
                                                {
                                                    Type
                                                        =
                                                        PaymentType
                                                        .Bank,
                                                    ImagePath =
                                                        "/Content/images/bank.png",
                                                    Description
                                                        =
                                                        "An invoice, bill or tab is a commercial document issued by a seller to a buyer, "
                                                        + "relating to a sale transaction and indicating the products, quantities, and agreed prices for products or "
                                                        + "services the seller had provided the buyer. Payment terms are usually stated on the invoice. These may specify that "
                                                        + "the buyer has a maximum number of days in which to pay, and is sometimes offered a discount if paid before the due date. "
                                                        + "The buyer could have already paid for the products or services listed on the invoice."
                                                },
                                            new PaymentTypeInfoViewModel
                                                {
                                                    Type
                                                        =
                                                        PaymentType
                                                        .IBox,
                                                    ImagePath =
                                                        "/Content/images/ibox.png",
                                                    Description
                                                        =
                                                        "IBOX is a multi-function computer box which is "
                                                        + "developed based on ITEAD A10/20 core board. It can run several operating systems such as Android and Linux"
                                                        + " to meet the needs of various multimedia applications. With external function expansion modules, IBOX can be transformed "
                                                        + "into a variety of virtual instruments, which can even be used as the control and gateway of intelligent home system. "
                                                },
                                            new PaymentTypeInfoViewModel
                                                {
                                                    Type
                                                        =
                                                        PaymentType
                                                        .Visa,
                                                    ImagePath =
                                                        "/Content/images/visa.png",
                                                    Description
                                                        =
                                                        "Join the Visa team and help change how the world pays. "
                                                }
                                        };
                return View(model);
            }

            return RedirectToAction("Games", "Game", new { area = "Common" });
        }

        public ActionResult Pay(PaymentType paymentType)
        {
            switch (paymentType)
            {
                case PaymentType.Bank:
                    PaymentStrategy = new BankStrategy();
                    break;
                case PaymentType.IBox:
                    PaymentStrategy = new IBoxStrategy();
                    break;
                case PaymentType.Visa:
                    PaymentStrategy = new VisaStrategy();
                    break;
            }

            return PaymentStrategy.PaymentView((OrderViewModel)Session["Order"]);
        }
    }
}