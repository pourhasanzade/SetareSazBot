using System.Data.Entity.ModelConfiguration;

namespace SetareSazBot.Domain.Entity
{
    public class BankEntity : BaseEntity
    {
        public string Name { get; set; }
    }

    public class BankEntityConfiguration : EntityTypeConfiguration<BankEntity>
    {
        public BankEntityConfiguration()
        {
            Property(x => x.Name).HasMaxLength(50);
            
            ToTable("Banks");
        }
    }
}
