namespace GameStore.DAL.Northwind
{
    using System.Collections.Generic;

    using GameStore.DAL.Abstract.Entities.Abstract;

    public sealed class Product : IEntity
    {
        public Product()
        {
            Order_Details = new HashSet<Order_Detail>();
        }

        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public int? SupplierID { get; set; }

        public int? CategoryID { get; set; }

        public string QuantityPerUnit { get; set; }

        public decimal? UnitPrice { get; set; }

        public short? UnitsInStock { get; set; }

        public short? UnitsOnOrder { get; set; }

        public short? ReorderLevel { get; set; }

        public bool Discontinued { get; set; }

        public Category Category { get; set; }

        public ICollection<Order_Detail> Order_Details { get; set; }

        public Supplier Supplier { get; set; }

        public int Id
        {
            get
            {
                return ProductID;
            }
            set
            {
                ProductID = value;
            }
        }
    }
}