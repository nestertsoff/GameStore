namespace GameStore.IoC.Modules
{
    using GameStore.DAL.Abstract.Context.Abstract;
    using GameStore.DAL.GameStore.Context.Concrete;
    using GameStore.DAL.GameStore.Repositories.Abstract;
    using GameStore.DAL.GameStore.Repositories.Concrete;
    using GameStore.DAL.Infrastructure.UnitOfWork.Abstract;
    using GameStore.DAL.Infrastructure.UnitOfWork.Concrete;
    using GameStore.DAL.Northwind;

    using Ninject.Modules;

    public class InfrastructureModule : NinjectModule
    {
        private readonly string _gameStoreConnectionString;

        private readonly string _northwindConnectionString;

        public InfrastructureModule(string gameStoreConnection, string northwindConnection)
        {
            _gameStoreConnectionString = gameStoreConnection;
            _northwindConnectionString = northwindConnection;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
            //Bind<IGameRepository>().To<GameRepository>();
            Bind<IGameStoreContext>()
                .To<GameStoreContext>()
                .WithConstructorArgument(_gameStoreConnectionString);
            Bind<INorthwindContext>()
                .To<NorthwindEntities>()
                .WithConstructorArgument(_northwindConnectionString);
        }
    }
}