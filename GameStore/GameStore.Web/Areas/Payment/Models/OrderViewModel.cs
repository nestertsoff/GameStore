namespace GameStore.Web.Areas.Payment.Models
{
    using System;
    using System.Collections.Generic;

    using GameStore.Web.Areas.Payment.Models.PaymentTypes;

    public class OrderViewModel
    {
        public int CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public ICollection<OrderDetailsViewModel> OrderDetailsList { get; set; }

        public List<PaymentTypeInfoViewModel> PaymentInfo { get; set; }

        public int Id { get; set; }
    }
}