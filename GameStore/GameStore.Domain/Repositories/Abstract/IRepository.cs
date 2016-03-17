namespace GameStore.DAL.Abstract.Repositories.Abstract
{
    using System;
    using System.Collections.Generic;

    using GameStore.DAL.Abstract.Entities.Abstract;

    public interface IRepository<TEntity>
        where TEntity : class, IEntity
    {
        IEnumerable<TEntity> Get();

        TEntity Get(int id);

        IEnumerable<TEntity> Find(Func<TEntity, bool> predicate);
    }
}