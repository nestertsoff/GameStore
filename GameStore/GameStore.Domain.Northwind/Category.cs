namespace GameStore.DAL.Northwind
{
    using System.Collections.Generic;

    using GameStore.DAL.Abstract.Entities.Abstract;

    public sealed class Category : IEntity
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int Id
        {
            get
            {
                return CategoryID;
            }
            set
            {
                CategoryID = value;
            }
        }

        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public byte[] Picture { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}