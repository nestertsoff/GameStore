namespace GameStore.DAL.Abstract.Repositories.Abstract
{
    using GameStore.DAL.Abstract.Entities.Abstract;

    public interface IGameStoreRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        void Create(TEntity item);

        void Update(TEntity item);

        void CreateOrUpdate(TEntity item);

        void Delete(int id);
    }
}