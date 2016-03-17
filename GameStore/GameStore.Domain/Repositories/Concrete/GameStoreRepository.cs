namespace GameStore.DAL.Abstract.Repositories.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using GameStore.DAL.Abstract.Context.Abstract;
    using GameStore.DAL.Abstract.Entities.Abstract;
    using GameStore.DAL.Abstract.Repositories.Abstract;

    public class GameStoreRepository<TEntity> : IGameStoreRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly IGameStoreContext _gameStoreDb;

        public GameStoreRepository(IGameStoreContext db)
        {
            _gameStoreDb = db;
        }

        public virtual IEnumerable<TEntity> Get()
        {
            return _gameStoreDb.Set<TEntity>().ToList();
        }

        public virtual TEntity Get(int id)
        {
            return _gameStoreDb.Set<TEntity>().Find(id);
        }

        public virtual IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
        {
            return _gameStoreDb.Set<TEntity>().Where(predicate).ToList();
        }

        public virtual void Create(TEntity item)
        {
            _gameStoreDb.Entry(item).State = EntityState.Added;
        }

        public virtual void Update(TEntity item)
        {
            _gameStoreDb.Entry(item).State = EntityState.Modified;
        }

        public virtual void CreateOrUpdate(TEntity item)
        {
            if (Get(item.Id) == null)
            {
                Create(item);
            }
            else
            {
                Update(item);
            }
        }

        public virtual void Delete(int id)
        {
            var item = Get(id);
            _gameStoreDb.Entry(item).State = EntityState.Deleted;
        }
    }
}