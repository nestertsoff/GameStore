namespace GameStore.BLL.Filtration.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using GameStore.BLL.Infrastructure;
    using GameStore.DAL.GameStore.Entities.Concrete;

    public class PublisherFilter : BaseFilter<Expression<Func<Game, bool>>>
    {
        private readonly IEnumerable<int> publisherIds;

        public PublisherFilter(IEnumerable<int> publisherIds)
        {
            this.publisherIds = publisherIds;
        }

        protected override Expression<Func<Game, bool>> Process(Expression<Func<Game, bool>> input)
        {
            return publisherIds.Count() != 0 ? input.And(x => publisherIds.Contains(x.Publisher.Id)) : input;
        }
    }
}