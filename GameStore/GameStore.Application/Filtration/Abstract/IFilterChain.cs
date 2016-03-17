namespace GameStore.BLL.Filtration.Abstract
{
    using System.Linq.Expressions;

    public interface IFilterChain<T>
        where T : Expression
    {
        IFilter<T> Root { get; }

        T Execute(T input);

        IFilterChain<T> Register(IFilter<T> filter);
    }
}