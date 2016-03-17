namespace GameStore.DAL.GameStore.Context.Concrete.Configuration
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using global::GameStore.DAL.GameStore.Entities.Concrete;

    public class CommentConfiguration : EntityTypeConfiguration<Comment>
    {
        public CommentConfiguration()
        {
            HasKey(_ => _.Id);
            Property(_ => _.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(_ => _.Name).IsRequired().HasMaxLength(30);
            Property(_ => _.Body).IsRequired();
            Property(_ => _.Date).IsRequired().HasColumnType("datetime2");
        }
    }
}