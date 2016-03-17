namespace GameStore.DAL.GameStore.Repositories.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    using global::GameStore.DAL.Abstract.Context.Abstract;
    using global::GameStore.DAL.Abstract.Repositories.Concrete;
    using global::GameStore.DAL.GameStore.Entities.Concrete;
    using global::GameStore.DAL.GameStore.Repositories.Abstract;

    public class GameRepository : GameStoreRepository<Game>, IGameRepository
    {
        private readonly IGameStoreContext _gameStoreDb;

        public GameRepository(IGameStoreContext db)
            : base(db)
        {
            _gameStoreDb = db;
        }

        public override IEnumerable<Game> Get()
        {
            return
                _gameStoreDb.Set<Game>()
                    .Include(_ => _.Genres)
                    .Include(_ => _.PlatformTypes)
                    .Include(_ => _.Comments)
                    .Include(_ => _.Publisher)
                    .Where(_ => _.IsDeleted == false)
                    .ToList();
        }

        public IEnumerable<Game> GetSorted(Func<Game, object> sortPredicate, Expression<Func<Game, bool>> predicate)
        {
            return
                _gameStoreDb.Set<Game>()
                    .Include(_ => _.Genres)
                    .Include(_ => _.PlatformTypes)
                    .Include(_ => _.Comments)
                    .Include(_ => _.Publisher)
                    .Where(_ => _.IsDeleted == false)
                    .Where(predicate)
                    .OrderBy(sortPredicate)
                    .ToList();
        }

        public override IEnumerable<Game> Find(Func<Game, bool> predicate)
        {
            return
                _gameStoreDb.Set<Game>()
                    .Include(_ => _.Genres)
                    .Include(_ => _.PlatformTypes)
                    .Include(_ => _.Comments)
                    .Include(_ => _.Publisher)
                    .Where(predicate)
                    .ToList();
        }

        public override void Create(Game game)
        {
            foreach (var genre in game.Genres)
            {
                _gameStoreDb.Set<Genre>().Attach(genre);
            }

            foreach (var platformType in game.PlatformTypes)
            {
                _gameStoreDb.Set<PlatformType>().Attach(platformType);
            }

            _gameStoreDb.Set<Game>().Add(game);
        }

        public override void Update(Game game)
        {
            foreach (var genre in game.Genres)
            {
                _gameStoreDb.Set<Genre>().Attach(genre);
            }

            foreach (var platformType in game.PlatformTypes)
            {
                _gameStoreDb.Set<PlatformType>().Attach(platformType);
            }

            _gameStoreDb.Entry(game).State = EntityState.Modified;
        }

        public override void CreateOrUpdate(Game game)
        {
            if (Get(game.Id) == null)
            {
                _gameStoreDb.Entry(game).State = EntityState.Added;
            }
            else
            {
                _gameStoreDb.Entry(game).State = EntityState.Modified;
            }
        }

        public virtual void Delete(string key)
        {
            var game = _gameStoreDb.Set<Game>().SingleOrDefault(_ => _.Key == key);
            if (game != null)
            {
                game.IsDeleted = true;
                _gameStoreDb.Entry(game).State = EntityState.Modified;
            }
        }

        public void IncreaseViews(int id)
        {
            var game = _gameStoreDb.Set<Game>().First(_ => _.Id == id);
            game.ViewsCount++;
            Update(game);
        }

        public void IncreaseViews(string key)
        {
            var game = _gameStoreDb.Set<Game>().First(_ => _.Key == key);
            game.ViewsCount++;
            Update(game);
        }
    }
}