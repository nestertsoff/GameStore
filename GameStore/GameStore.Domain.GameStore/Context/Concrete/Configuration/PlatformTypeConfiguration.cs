namespace GameStore.DAL.GameStore.Context.Concrete.Configuration
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using global::GameStore.DAL.GameStore.Entities.Concrete;

    public class PlatformTypeConfiguration : EntityTypeConfiguration<PlatformType>
    {
        public PlatformTypeConfiguration()
        {
            HasKey(_ => _.Id);
            Property(_ => _.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(_ => _.Type).IsRequired().HasMaxLength(20);
        }
    }
}