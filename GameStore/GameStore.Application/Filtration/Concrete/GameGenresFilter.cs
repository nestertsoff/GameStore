namespace GameStore.BLL.Filtration.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using GameStore.DAL.GameStore.Entities.Concrete;

    internal class GameGenresFilter : BaseFilter<Expression<Func<Game, bool>>>
    {
        private readonly IEnumerable<string> sortGenres;

        public GameGenresFilter(IEnumerable<string> genreList)
        {
            sortGenres = genreList;
        }

        protected override Expression<Func<Game, bool>> Process(Expression<Func<Game, bool>> input)
        {
            if (sortGenres == null || !sortGenres.Any())
            {
                return input;
            }

            return input;
        }
    }
}