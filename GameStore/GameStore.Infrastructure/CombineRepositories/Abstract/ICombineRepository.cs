namespace GameStore.DAL.Infrastructure.CombineRepositories.Abstract
{
    using System;
    using System.Collections.Generic;

    using global::GameStore.DAL.Abstract.Entities.Abstract;

    public interface ICombineRepository<TEntity>
        where TEntity : class, IEntity
    {
        IEnumerable<TEntity> Get();

        TEntity Get(int id);

        IEnumerable<TEntity> Find(Func<TEntity, bool> predicate);

        void Create(TEntity item);

        void Update(TEntity item);

        void Delete(int id);
    }
}