namespace GameStore.BLL.Filtration.Concrete
{
    using GameStore.BLL.Filtration.Abstract;

    public abstract class BaseFilter<T> : IFilter<T> where T : class
    {
        private IFilter<T> _nextFilter;

        public T Execute(T input)
        {
            var val = Process(input);
            if (_nextFilter != null)
            {
                val = _nextFilter.Execute(val);
            }

            return val;
        }

        public void Register(IFilter<T> nextFilter)
        {
            if (_nextFilter == null)
            {
                _nextFilter = nextFilter;
            }
            else
            {
                _nextFilter.Register(nextFilter);
            }
        }

        protected abstract T Process(T input);
    }
}