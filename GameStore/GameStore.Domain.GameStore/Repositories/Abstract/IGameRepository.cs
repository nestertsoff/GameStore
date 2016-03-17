namespace GameStore.DAL.GameStore.Repositories.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using global::GameStore.DAL.Abstract.Repositories.Abstract;
    using global::GameStore.DAL.GameStore.Entities.Concrete;

    public interface IGameRepository : IGameStoreRepository<Game>
    {
        void Delete(string key);

        void IncreaseViews(int id);

        void IncreaseViews(string key);

        IEnumerable<Game> GetSorted(Func<Game, object> sortPredicate, Expression<Func<Game, bool>> predicate);
    }
}