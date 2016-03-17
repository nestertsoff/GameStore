namespace GameStore.DAL.GameStore.Context.Concrete.Configuration
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using global::GameStore.DAL.GameStore.Entities.Concrete;

    internal class PublisherConfiguration : EntityTypeConfiguration<Publisher>
    {
        public PublisherConfiguration()
        {
            HasKey(_ => _.Id);
            Property(_ => _.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(_ => _.CompanyName).IsRequired().HasColumnType("nvarchar").HasMaxLength(40);
            Property(_ => _.Description).IsRequired().HasColumnType("ntext");
            Property(_ => _.HomePage).IsRequired().HasColumnType("ntext");
        }
    }
}