using System.Data.Entity.ModelConfiguration;
using SetareSazBot.Domain.Enum;

namespace SetareSazBot.Domain.Entity
{
    public class ButtonEntity : BaseEntity
    {
        public string Code { get; set; }
        public string Text { get; set; }
        public ButtonTypeEnum Type { get; set; }
        public ButtonSimpleTypeEnum ViewType { get; set; }
        public string ImageUrl { get; set; }
        public int Row { get; set; }
        public int Order { get; set; }
        public int StateId { get; set; }
        public string Data { get; set; }
        public MessageBehaviourTypeEnum BehaviourType { get; set; }

    }

    public class ButtonEntityConfiguration : EntityTypeConfiguration<ButtonEntity>
    {
        public ButtonEntityConfiguration()
        {
            Property(x => x.Text).HasMaxLength(50);
            Property(x => x.ImageUrl).HasMaxLength(250);
            Property(x => x.Data).HasMaxLength(500);

            HasIndex(x => x.StateId).IsUnique(false);

            ToTable("Buttons");
        }
    }
}
