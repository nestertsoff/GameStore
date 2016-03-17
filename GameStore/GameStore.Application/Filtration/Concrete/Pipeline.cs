namespace GameStore.BLL.Filtration.Concrete
{
    using System.Linq.Expressions;

    using GameStore.BLL.Filtration.Abstract;

    public class Pipeline<T> : IPipeline<T>
        where T : Expression
    {
        private IFilter<T> root;

        public T Execute(T input)
        {
            return root != null ? root.Execute(input) : input;
        }

        public IPipeline<T> Register(IFilter<T> nextFilter)
        {
            if (root == null)
            {
                root = nextFilter;
            }
            else
            {
                root.Register(nextFilter);
            }

            return this;
        }
    }
}