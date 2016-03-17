namespace GameStore.DAL.Northwind.Repositories.Concrete
{
    using GameStore.DAL.Abstract.Context.Abstract;
    using GameStore.DAL.Abstract.Repositories.Concrete;
    using GameStore.DAL.Northwind.Repositories.Abstract;

    public class ProductRepository : NorthwindRepository<Product>, IProductRepository
    {
        private readonly INorthwindContext _northwindDb;

        public ProductRepository(INorthwindContext northwindDb)
            : base(northwindDb)
        {
            _northwindDb = northwindDb;
        }
    }
}