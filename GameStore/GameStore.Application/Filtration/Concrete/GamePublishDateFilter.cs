namespace GameStore.BLL.Filtration.Concrete
{
    using System;
    using System.Linq.Expressions;

    using GameStore.BLL.Enums;
    using GameStore.BLL.Infrastructure;
    using GameStore.DAL.GameStore.Entities.Concrete;

    public class GamePublishDateFilter : BaseFilter<Expression<Func<Game, bool>>>
    {
        private readonly PeriodType _period;

        public GamePublishDateFilter(PeriodType period)
        {
            _period = period;
        }

        protected override Expression<Func<Game, bool>> Process(Expression<Func<Game, bool>> input)
        {
            var date = DateTime.UtcNow;
            switch (_period)
            {
                case PeriodType.All:
                    return input;

                case PeriodType.LastMonth:
                    date = date.AddMonths(-1);
                    break;

                case PeriodType.LastWeek:
                    date = date.AddDays(-7);
                    break;

                case PeriodType.LastYear:
                    date = date.AddYears(-1);
                    break;

                case PeriodType.ThreeYear:
                    date = date.AddYears(-3);
                    break;

                case PeriodType.TwoYear:
                    date = date.AddYears(-2);
                    break;
            }

            return input.And(x => x.PublishDate >= date);
        }
    }
}