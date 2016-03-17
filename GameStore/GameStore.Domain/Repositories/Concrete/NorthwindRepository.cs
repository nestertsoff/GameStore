namespace GameStore.DAL.Abstract.Repositories.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using GameStore.DAL.Abstract.Context.Abstract;
    using GameStore.DAL.Abstract.Entities.Abstract;
    using GameStore.DAL.Abstract.Repositories.Abstract;

    public class NorthwindRepository<TEntity> : INorthwindRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly INorthwindContext _northwindDb;

        public NorthwindRepository(INorthwindContext northwindDb)
        {
            _northwindDb = northwindDb;
        }

        public virtual IEnumerable<TEntity> Get()
        {
            return _northwindDb.Set<TEntity>().ToList();
        }

        public virtual TEntity Get(int id)
        {
            return _northwindDb.Set<TEntity>().Find(id);
        }

        public virtual IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
        {
            return _northwindDb.Set<TEntity>().Where(predicate).ToList();
        }
    }
}