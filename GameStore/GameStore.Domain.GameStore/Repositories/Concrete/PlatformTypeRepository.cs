namespace GameStore.DAL.GameStore.Repositories.Concrete
{
    using global::GameStore.DAL.Abstract.Context.Abstract;
    using global::GameStore.DAL.Abstract.Repositories.Concrete;
    using global::GameStore.DAL.GameStore.Entities.Concrete;
    using global::GameStore.DAL.GameStore.Repositories.Abstract;

    public class PlatformTypeRepository : GameStoreRepository<PlatformType>, IPlatformTypeRepository
    {
        public PlatformTypeRepository(IGameStoreContext db)
            : base(db)
        {
        }
    }
}