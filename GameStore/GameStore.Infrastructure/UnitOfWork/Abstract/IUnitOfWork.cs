namespace GameStore.DAL.Infrastructure.UnitOfWork.Abstract
{
    using global::GameStore.DAL.Abstract.Entities.Abstract;
    using global::GameStore.DAL.Abstract.Repositories.Abstract;
    using global::GameStore.DAL.GameStore.Repositories.Abstract;
    using global::GameStore.DAL.Infrastructure.CombineRepositories.Abstract;
    using global::GameStore.DAL.Northwind.Repositories.Abstract;

    public interface IUnitOfWork
    {
        IGameRepository GetGames();

        IProductRepository GetProducts();

        IGameCombineRepository GetGamesFromAllDbs();

        ICommentRepository GetComments();

        IGenreRepository GetGenres();

        ICategoryRepository GetCategories();

        IGenreCombineRepository GetGenresFromAllDbs();

        IPlatformTypeRepository GetPlatformTypes();

        IPublisherRepository GetPublishers();

        ISupplierRepository GetSuppliers();

        IPublisherCombineRepository GetPublishersFromAllDbs();

        IRepository<TEntity> GetGameStoreEntities<TEntity>() where TEntity : class, IEntity;

        IRepository<TEntity> GetNorthwindEntities<TEntity>() where TEntity : class, IEntity;

        void Save();
    }
}