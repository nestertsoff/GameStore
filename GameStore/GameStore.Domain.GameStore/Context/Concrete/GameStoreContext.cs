namespace GameStore.DAL.GameStore.Context.Concrete
{
    using System.Data.Entity;

    using global::GameStore.DAL.Abstract.Context.Abstract;
    using global::GameStore.DAL.Abstract.Entities.Abstract;
    using global::GameStore.DAL.GameStore.Context.Concrete.Configuration;
    using global::GameStore.DAL.GameStore.Entities.Concrete;

    public class GameStoreContext : DbContext, IGameStoreContext
    {
        public GameStoreContext()
            : base("GameStoreDb")
        {
            Database.SetInitializer(new ContextInitializer());
        }

        public GameStoreContext(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer(new ContextInitializer());
            Configuration.ProxyCreationEnabled = false;
        }

        public IDbSet<Game> Games { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<Genre> Genres { get; set; }

        public IDbSet<PlatformType> PlatformTypes { get; set; }

        public IDbSet<Publisher> Publishers { get; set; }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class, IEntity
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new GameConfiguration());
            modelBuilder.Configurations.Add(new CommentConfiguration());
            modelBuilder.Configurations.Add(new GenreConfiguration());
            modelBuilder.Configurations.Add(new PlatformTypeConfiguration());
            modelBuilder.Configurations.Add(new PublisherConfiguration());
            modelBuilder.Configurations.Add(new OrderConfiguration());
            modelBuilder.Configurations.Add(new OrderDetailsConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}