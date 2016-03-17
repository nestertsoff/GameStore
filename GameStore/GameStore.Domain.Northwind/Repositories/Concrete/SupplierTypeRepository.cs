namespace GameStore.DAL.Northwind.Repositories.Concrete
{
    using GameStore.DAL.Abstract.Context.Abstract;
    using GameStore.DAL.Abstract.Repositories.Concrete;
    using GameStore.DAL.Northwind.Repositories.Abstract;

    public class SupplierRepository : NorthwindRepository<Supplier>, ISupplierRepository
    {
        private readonly INorthwindContext _northwindDb;

        public SupplierRepository(INorthwindContext northwindDb)
            : base(northwindDb)
        {
            _northwindDb = northwindDb;
        }
    }
}