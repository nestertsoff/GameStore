namespace GameStore.DAL.Northwind
{
    using System;
    using System.Collections.Generic;

    using GameStore.DAL.Abstract.Entities.Abstract;

    public sealed class Order : IEntity
    {
        public Order()
        {
            Order_Details = new HashSet<Order_Detail>();
        }

        public int OrderID { get; set; }

        public string CustomerID { get; set; }

        public int? EmployeeID { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public int? ShipVia { get; set; }

        public decimal? Freight { get; set; }

        public string ShipName { get; set; }

        public string ShipAddress { get; set; }

        public string ShipCity { get; set; }

        public string ShipRegion { get; set; }

        public string ShipPostalCode { get; set; }

        public string ShipCountry { get; set; }

        public Customer Customer { get; set; }

        public Employee Employee { get; set; }

        public ICollection<Order_Detail> Order_Details { get; set; }

        public Shipper Shipper { get; set; }

        public int Id
        {
            get
            {
                return OrderID;
            }
            set
            {
                OrderID = value;
            }
        }
    }
}