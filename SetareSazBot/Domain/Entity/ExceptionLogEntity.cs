using System.Data.Entity.ModelConfiguration;

namespace SetareSazBot.Domain.Entity
{
    public class ExceptionLogEntity : BaseEntity
    {
        public string ChatId { get; set; }
        public string Title { get; set; }
        public string Exception { get; set; }
        public string WebException { get; set; }
    }

    public class ExceptionLogEntityConfiguration : EntityTypeConfiguration<ExceptionLogEntity>
    {
        public ExceptionLogEntityConfiguration()
        {
            Property(x => x.ChatId).HasMaxLength(250);
            Property(x => x.Title).HasMaxLength(250);


            ToTable("ExceptionLogs");
        }
    }
}