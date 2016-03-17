namespace GameStore.DAL.GameStore.Entities.Concrete
{
    using global::GameStore.DAL.Abstract.Entities.Concrete;

    public class OrderDetails : Entity
    {
        public decimal Price { get; set; }

        public short Quantity { get; set; }

        public float Discount { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public virtual Game Product { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}