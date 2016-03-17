namespace GameStore.DAL.Infrastructure.UnitOfWork.Concrete
{
    using System.Data.Entity.Validation;
    using System.Text;

    using global::GameStore.DAL.Abstract.Context.Abstract;
    using global::GameStore.DAL.Abstract.Entities.Abstract;
    using global::GameStore.DAL.Abstract.Repositories.Abstract;
    using global::GameStore.DAL.Abstract.Repositories.Concrete;
    using global::GameStore.DAL.GameStore.Repositories.Abstract;
    using global::GameStore.DAL.GameStore.Repositories.Concrete;
    using global::GameStore.DAL.Infrastructure.CombineRepositories.Abstract;
    using global::GameStore.DAL.Infrastructure.CombineRepositories.Concrete;
    using global::GameStore.DAL.Infrastructure.UnitOfWork.Abstract;
    using global::GameStore.DAL.Northwind.Repositories.Abstract;
    using global::GameStore.DAL.Northwind.Repositories.Concrete;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IGameStoreContext _gameStoreDb;

        private readonly INorthwindContext _northwindDb;

        private ICategoryRepository _categoryRepository;

        private ICommentRepository _commentRepository;

        private IGameRepository _gameRepository;

        private IGameCombineRepository _gameCombineRepository;

        private IGenreRepository _genreRepository;

        private IGenreCombineRepository _genreCombineRepository;

        private IPlatformTypeRepository _platformTypeRepository;

        private IProductRepository _productRepository;

        private IPublisherRepository _publisherRepository;

        private IPublisherCombineRepository _publisherCombineRepository;

        private ISupplierRepository _supplierRepository;

        public UnitOfWork(IGameStoreContext gameStoreDb, INorthwindContext northwindDb)
        {
            _gameStoreDb = gameStoreDb;
            _northwindDb = northwindDb;
        }

        public IGameRepository GetGames()
        {
            return _gameRepository ?? (_gameRepository = new GameRepository(_gameStoreDb));
        }

        public IProductRepository GetProducts()
        {
            return _productRepository ?? (_productRepository = new ProductRepository(_northwindDb));
        }

        public IGameCombineRepository GetGamesFromAllDbs()
        {
            return _gameCombineRepository
                   ?? (_gameCombineRepository = new GameCombineRepository(GetGames(), GetProducts()));
        }

        public ICommentRepository GetComments()
        {
            return _commentRepository ?? (_commentRepository = new CommentRepository(_gameStoreDb));
        }

        public IGenreRepository GetGenres()
        {
            return _genreRepository ?? (_genreRepository = new GenreRepository(_gameStoreDb));
        }

        public IGenreCombineRepository GetGenresFromAllDbs()
        {
            return _genreCombineRepository
                   ?? (_genreCombineRepository = new GenreCombineRepository(GetGenres(), GetCategories()));
        }

        public ICategoryRepository GetCategories()
        {
            return _categoryRepository ?? (_categoryRepository = new CategoryRepository(_northwindDb));
        }

        public IPlatformTypeRepository GetPlatformTypes()
        {
            return _platformTypeRepository ?? (_platformTypeRepository = new PlatformTypeRepository(_gameStoreDb));
        }

        public IPublisherRepository GetPublishers()
        {
            return _publisherRepository ?? (_publisherRepository = new PublisherRepository(_gameStoreDb));
        }

        public ISupplierRepository GetSuppliers()
        {
            return _supplierRepository ?? (_supplierRepository = new SupplierRepository(_northwindDb));
        }

        public IPublisherCombineRepository GetPublishersFromAllDbs()
        {
            return _publisherCombineRepository
                   ?? (_publisherCombineRepository = new PublisherCombineRepository(GetPublishers(), GetSuppliers()));
        }

        public IRepository<TEntity> GetGameStoreEntities<TEntity>() where TEntity : class, IEntity
        {
            IRepository<TEntity> entityRepository = new GameStoreRepository<TEntity>(_gameStoreDb);
            return entityRepository;
        }

        public IRepository<TEntity> GetNorthwindEntities<TEntity>() where TEntity : class, IEntity
        {
            IRepository<TEntity> entityRepository = new NorthwindRepository<TEntity>(_northwindDb);
            return entityRepository;
        }

        public void Save()
        {
            try
            {
                _gameStoreDb.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb, ex
                    );
            }
        }
    }
}