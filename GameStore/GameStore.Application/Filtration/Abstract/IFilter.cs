namespace GameStore.BLL.Filtration.Abstract
{
    public interface IFilter<T> where T : class
    {
        T Execute(T input);

        void Register(IFilter<T> filter);
    }
}