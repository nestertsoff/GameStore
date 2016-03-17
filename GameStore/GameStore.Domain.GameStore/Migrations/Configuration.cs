namespace GameStore.DAL.GameStore.Migrations
{
    using System.Data.Entity.Migrations;

    using global::GameStore.DAL.GameStore.Context.Concrete;

    internal sealed class Configuration : DbMigrationsConfiguration<GameStoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GameStoreContext context)
        {
        }
    }
}