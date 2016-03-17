namespace GameStore.BLL.Filtration.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using GameStore.BLL.Infrastructure;
    using GameStore.DAL.GameStore.Entities.Concrete;

    public class GenreFilter : BaseFilter<Expression<Func<Game, bool>>>
    {
        private readonly IEnumerable<int> genreIds;

        public GenreFilter(IEnumerable<int> genreIds)
        {
            this.genreIds = genreIds;
        }

        protected override Expression<Func<Game, bool>> Process(Expression<Func<Game, bool>> input)
        {
            return genreIds.Count() != 0
                       ? input.And(x => Enumerable.Any<Genre>(x.Genres, genre => genreIds.Contains(genre.Id)))
                       : input;
        }
    }
}