namespace GameStore.DAL.Northwind
{
    using System.Data.Entity;

    using GameStore.DAL.Abstract.Context.Abstract;
    using GameStore.DAL.Abstract.Entities.Abstract;

    public class NorthwindEntities : DbContext, INorthwindContext
    {
        public NorthwindEntities()
            : base("name=NorthwindEntities")
        {
        }

        public NorthwindEntities(string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<CustomerDemographic> CustomerDemographics { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Order_Detail> Order_Details { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Shipper> Shippers { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Territory> Territories { get; set; }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class, IEntity
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //throw new UnintentionalCodeFirstException();

            modelBuilder.Entity<Product>().HasKey(x => x.ProductID);
            modelBuilder.Entity<Product>().Property(x => x.ProductID).HasColumnName("ProductID");

            modelBuilder.Entity<Product>().Ignore(x => x.Id);
            modelBuilder.Entity<Category>().Ignore(x => x.Id);
            modelBuilder.Entity<Supplier>().Ignore(x => x.Id);
            modelBuilder.Entity<Shipper>().Ignore(x => x.Id);
            modelBuilder.Entity<Order>().Ignore(x => x.Id);
            modelBuilder.Entity<Order>().Ignore(x => x.Shipper);
            modelBuilder.Entity<Shipper>().Ignore(x => x.Orders);

            modelBuilder.Entity<Order>().HasKey(x => x.OrderID);
            modelBuilder.Entity<CustomerDemographic>().HasKey(x => x.CustomerTypeID);
            modelBuilder.Entity<Order_Detail>().HasKey(x => new { x.OrderID, x.ProductID });
            modelBuilder.Entity<Order_Detail>().ToTable("Order Details");
        }
    }
}