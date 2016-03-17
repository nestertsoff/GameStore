namespace GameStore.DAL.Northwind
{
    using System.Collections.Generic;

    public sealed class Territory
    {
        public Territory()
        {
            Employees = new HashSet<Employee>();
        }

        public string TerritoryID { get; set; }

        public string TerritoryDescription { get; set; }

        public int RegionID { get; set; }

        public Region Region { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}