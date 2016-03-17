namespace GameStore.BLL.Filtration.Concrete
{
    using System;
    using System.Linq.Expressions;

    using GameStore.BLL.Infrastructure;
    using GameStore.DAL.GameStore.Entities.Concrete;

    public class GamePriceFilter : BaseFilter<Expression<Func<Game, bool>>>
    {
        private readonly decimal? maxPrice;

        private readonly decimal? minPrice;

        public GamePriceFilter(decimal? minPrice, decimal? maxPrice)
        {
            this.minPrice = minPrice;
            this.maxPrice = maxPrice;
        }

        protected override Expression<Func<Game, bool>> Process(Expression<Func<Game, bool>> input)
        {
            if (minPrice != null && maxPrice != null)
            {
                input = input.And(x => x.Price >= minPrice && x.Price <= maxPrice);
            }
            else if (minPrice != null)
            {
                input = input.And(x => x.Price >= minPrice);
            }
            else if (maxPrice != null)
            {
                input = input.And(x => x.Price <= maxPrice);
            }

            return input;
        }
    }
}