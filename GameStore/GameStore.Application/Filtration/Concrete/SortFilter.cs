namespace GameStore.BLL.Filtration.Concrete
{
    using System;

    using GameStore.BLL.Enums;
    using GameStore.DAL.GameStore.Entities.Concrete;

    public class SortFilter : BaseFilter<Func<Game, object>>
    {
        private readonly SortType sortOption;

        public SortFilter(SortType sortOption)
        {
            this.sortOption = sortOption;
        }

        protected override Func<Game, object> Process(Func<Game, object> input)
        {
            switch (sortOption)
            {
                case SortType.MostCommented:
                    input = x => x.Comments.Count * -1;
                    break;

                case SortType.MostPopular:
                    input = x => x.ViewsCount * -1;
                    break;

                case SortType.New:
                    input = x => x.PublishDate.Ticks * -1;
                    break;

                case SortType.Old:
                    input = x => x.PublishDate.Ticks;
                    break;

                case SortType.PriceDesc:
                    input = x => x.Price * -1;
                    break;

                case SortType.PriceAsc:
                    input = x => x.Price;
                    break;
            }

            return input;
        }
    }
}