namespace GameStore.Web.Areas.Payment.Models
{
    using GameStore.Web.Areas.Common.Models;

    public class OrderDetailsViewModel
    {
        public decimal Price { get; set; }

        public short Quantity { get; set; }

        public float Discount { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public GameViewModel Product { get; set; }

        public int OrderId { get; set; }

        public OrderViewModel Order { get; set; }

        public int Id { get; set; }
    }
}