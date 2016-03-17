namespace GameStore.DAL.GameStore.Context.Concrete.Configuration
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using global::GameStore.DAL.GameStore.Entities.Concrete;

    public class GameConfiguration : EntityTypeConfiguration<Game>
    {
        public GameConfiguration()
        {
            HasKey(_ => _.Id);
            Property(_ => _.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(_ => _.Key).IsRequired().HasMaxLength(50);
            Property(_ => _.Name).IsRequired().HasMaxLength(50);
            Property(_ => _.Description).IsRequired();
            Property(_ => _.Price).HasColumnType("money").IsRequired();
            Property(_ => _.UnitsInStock).HasColumnType("smallint").IsRequired();
            Property(_ => _.Discontinued).HasColumnType("bit").IsRequired();
            Property(_ => _.PublishDate).HasColumnType("datetime2").IsRequired();
        }
    }
}