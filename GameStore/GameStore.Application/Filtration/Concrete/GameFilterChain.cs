namespace GameStore.BLL.Filtration.Concrete
{
    using System;
    using System.Linq.Expressions;

    using GameStore.BLL.Filtration.Abstract;
    using GameStore.DAL.GameStore.Entities.Concrete;

    public class GameFilterChain : IFilterChain<Expression<Func<Game, bool>>>
    {
        public IFilter<Expression<Func<Game, bool>>> Root { get; private set; }

        public Expression<Func<Game, bool>> Execute(Expression<Func<Game, bool>> input)
        {
            return Root.Execute(input);
        }

        public IFilterChain<Expression<Func<Game, bool>>> Register(IFilter<Expression<Func<Game, bool>>> filter)
        {
            if (Root == null)
            {
                Root = filter;
            }
            else
            {
                Root.Register(filter);
            }

            return this;
        }
    }
}