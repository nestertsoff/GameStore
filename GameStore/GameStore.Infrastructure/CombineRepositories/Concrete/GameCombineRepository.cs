namespace GameStore.DAL.Infrastructure.CombineRepositories.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using global::GameStore.DAL.GameStore.Entities.Concrete;
    using global::GameStore.DAL.GameStore.Repositories.Abstract;
    using global::GameStore.DAL.Infrastructure.CombineRepositories.Abstract;
    using global::GameStore.DAL.Infrastructure.Utils;
    using global::GameStore.DAL.Northwind;
    using global::GameStore.DAL.Northwind.Repositories.Abstract;

    public class GameCombineRepository : GenericCombineRepository<Game, Product>, IGameCombineRepository
    {
        private readonly IGameRepository _gameRepository;

        private readonly IProductRepository _productRepository;

        public GameCombineRepository(IGameRepository gameRepository, IProductRepository productRepository)
            : base(gameRepository, productRepository)
        {
            _gameRepository = gameRepository;
            _productRepository = productRepository;
        }

        public IEnumerable<Game> GetSorted(Func<Game, object> sortPredicate, Expression<Func<Game, bool>> predicate)
        {
            return Get().AsQueryable().Where(predicate).OrderBy(sortPredicate).ToList();
        }

        //public new void Create(Game game)
        //{
        //    game.Id = DbIdentifier.DecodeId(game.Id);
        //    game.
        //    _entityGameStoreRepository.Create(item);
        //}

        //public new void Update(Game game)
        //{
        //    _entityGameStoreRepository.CreateOrUpdate(item);
        //}

        public void Delete(string key)
        {
            var item = Find(_ => _.Key == key).SingleOrDefault();
            if (item != null)
            {
                item.IsDeleted = true;
                this._gameRepository.CreateOrUpdate(item);
            }
        }
    }
}