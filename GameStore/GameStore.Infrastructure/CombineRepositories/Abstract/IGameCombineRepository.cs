namespace GameStore.DAL.Infrastructure.CombineRepositories.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using global::GameStore.DAL.GameStore.Entities.Concrete;

    public interface IGameCombineRepository : ICombineRepository<Game>
    {
        IEnumerable<Game> GetSorted(Func<Game, object> sortPredicate, Expression<Func<Game, bool>> predicate);

        void Delete(string key);
    }
}