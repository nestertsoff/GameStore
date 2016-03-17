namespace GameStore.DAL.Abstract.Context.Abstract
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using GameStore.DAL.Abstract.Entities.Abstract;

    public interface INorthwindContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class, IEntity;

        DbEntityEntry Entry(object entity);

        int SaveChanges();

        void Dispose();
    }
}