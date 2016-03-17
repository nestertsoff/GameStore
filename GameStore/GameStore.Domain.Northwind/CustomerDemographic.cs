
namespace GameStore.DAL.Northwind
{
    using System.Collections.Generic;
    
    public sealed class CustomerDemographic
    {
        public CustomerDemographic()
        {
            Customers = new HashSet<Customer>();
        }
    
        public string CustomerTypeID { get; set; }
        public string CustomerDesc { get; set; }
    
        public ICollection<Customer> Customers { get; set; }
    }
}
