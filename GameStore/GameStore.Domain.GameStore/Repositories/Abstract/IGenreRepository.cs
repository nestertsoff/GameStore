namespace GameStore.DAL.GameStore.Repositories.Abstract
{
    using global::GameStore.DAL.Abstract.Repositories.Abstract;
    using global::GameStore.DAL.GameStore.Entities.Concrete;

    public interface IGenreRepository : IGameStoreRepository<Genre>
    {
    }
}
