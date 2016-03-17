namespace GameStore.DAL.GameStore.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class M1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "ViewsCount", c => c.Int(false));
        }

        public override void Down()
        {
            DropColumn("dbo.Games", "ViewsCount");
        }
    }
}