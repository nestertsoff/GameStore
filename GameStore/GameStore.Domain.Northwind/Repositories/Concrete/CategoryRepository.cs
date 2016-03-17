namespace GameStore.DAL.Northwind.Repositories.Concrete
{
    using GameStore.DAL.Abstract.Context.Abstract;
    using GameStore.DAL.Abstract.Repositories.Concrete;
    using GameStore.DAL.Northwind.Repositories.Abstract;

    public class CategoryRepository : NorthwindRepository<Category>, ICategoryRepository
    {
        private readonly INorthwindContext _northwindDb;

        public CategoryRepository(INorthwindContext northwindDb)
            : base(northwindDb)
        {
            _northwindDb = northwindDb;
        }
    }
}