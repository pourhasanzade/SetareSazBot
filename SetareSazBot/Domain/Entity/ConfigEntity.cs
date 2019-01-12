using System.Data.Entity.ModelConfiguration;

namespace SetareSazBot.Domain.Entity
{
    public class ConfigEntity : BaseEntity
    {
        public string LastMessageId { get; set; }
    }

    public class ConfigEntityConfiguration : EntityTypeConfiguration<ConfigEntity>
    {
        public ConfigEntityConfiguration()
        {
            Property(x => x.LastMessageId).HasMaxLength(200);

            ToTable("Configs");
        }
    }
}