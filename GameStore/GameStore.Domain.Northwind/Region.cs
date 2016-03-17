namespace GameStore.DAL.Northwind
{
    using System.Collections.Generic;

    public sealed class Region
    {
        public Region()
        {
            Territories = new HashSet<Territory>();
        }

        public int RegionID { get; set; }

        public string RegionDescription { get; set; }

        public ICollection<Territory> Territories { get; set; }
    }
}