namespace GameStore.BLL.Filtration.Concrete
{
    using System;
    using System.Linq.Expressions;

    using GameStore.BLL.Infrastructure;
    using GameStore.DAL.GameStore.Entities.Concrete;

    public class GameNameFilter : BaseFilter<Expression<Func<Game, bool>>>
    {
        private readonly string searchString;

        public GameNameFilter(string searchString)
        {
            this.searchString = searchString;
        }

        protected override Expression<Func<Game, bool>> Process(Expression<Func<Game, bool>> input)
        {
            return string.IsNullOrEmpty(searchString)
                       ? input
                       : input.And(x => x.Name.ToLower().Contains(searchString.ToLower()));
        }
    }
}