namespace GameStore.DAL.GameStore.Context.Concrete.Configuration
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using global::GameStore.DAL.GameStore.Entities.Concrete;

    public class OrderDetailsConfiguration : EntityTypeConfiguration<OrderDetails>
    {
        public OrderDetailsConfiguration()
        {
            HasKey(_ => _.Id);
            Property(_ => _.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(_ => _.Price).IsRequired().HasColumnType("money");
            Property(_ => _.Quantity).IsRequired().HasColumnType("smallint");
            Property(_ => _.Discount).IsRequired().HasColumnType("real");
        }
    }
}