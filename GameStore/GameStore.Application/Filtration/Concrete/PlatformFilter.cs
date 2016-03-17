namespace GameStore.BLL.Filtration.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using GameStore.BLL.Infrastructure;
    using GameStore.DAL.GameStore.Entities.Concrete;

    public class PlatformFilter : BaseFilter<Expression<Func<Game, bool>>>
    {
        private readonly IEnumerable<int> platformIds;

        public PlatformFilter(IEnumerable<int> platformIds)
        {
            this.platformIds = platformIds;
        }

        protected override Expression<Func<Game, bool>> Process(Expression<Func<Game, bool>> input)
        {
            return platformIds.Count() != 0
                       ? input.And(x => Enumerable.Any<PlatformType>(x.PlatformTypes, platform => platformIds.Contains(platform.Id)))
                       : input;
        }
    }
}