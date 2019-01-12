using System.Data.Entity.ModelConfiguration;

namespace SetareSazBot.Domain.Entity
{
    public class UserDataEntity : BaseEntity
    {
        public string ChatId { get; set; }
        public long StateId { get; set; }
    }

    public class UserDataEntityConfiguration : EntityTypeConfiguration<UserDataEntity>
    {
        public UserDataEntityConfiguration()
        {
            Property(x => x.ChatId).HasMaxLength(250);

            HasIndex(x => x.ChatId).IsUnique(true);

            ToTable("UserData");
        }
    }
}
