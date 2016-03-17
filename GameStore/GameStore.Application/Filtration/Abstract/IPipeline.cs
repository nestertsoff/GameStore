namespace GameStore.BLL.Filtration.Abstract
{
    using System.Linq.Expressions;

    public interface IPipeline<TExpression>
        where TExpression : Expression
    {
        TExpression Execute(TExpression input);

        IPipeline<TExpression> Register(IFilter<TExpression> nextFilter);
    }
}