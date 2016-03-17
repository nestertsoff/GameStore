namespace GameStore.DAL.Abstract.Repositories.Abstract
{
    using GameStore.DAL.Abstract.Entities.Abstract;

    public interface INorthwindRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
         
    }
}