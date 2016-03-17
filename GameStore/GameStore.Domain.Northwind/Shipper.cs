namespace GameStore.DAL.Northwind
{
    using System.Collections.Generic;

    using GameStore.DAL.Abstract.Entities.Abstract;

    public sealed class Shipper : IEntity
    {
        public Shipper()
        {
            Orders = new HashSet<Order>();
        }

        public int ShipperID { get; set; }

        public string CompanyName { get; set; }

        public string Phone { get; set; }

        public ICollection<Order> Orders { get; set; }

        public int Id
        {
            get
            {
                return ShipperID;
            }
            set
            {
                ShipperID = value;
            }
        }
    }
}