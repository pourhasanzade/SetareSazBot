using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SetareSazBot.Domain.Enum;

namespace SetareSazBot.Domain.Entity
{
    public class UserInfoEntity : BaseEntity
    {
        public string ChatId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string BirthDate { get; set; }
        public string NationalCode { get; set; }

        public long? ProvinceId { get; set; }
        public string Province { get; set; }
        public long? CityId { get; set; }
        public string City { get; set; }
        public PopulationStatusEnum? PopulationStatus { get; set; }
        public string Resident { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }

        public PositionTypeEnum? PositionType { get; set; }
        public string FavoritePost { get; set; }
        public string VideoSrc { get; set; }
        public string VideoStatus { get; set; }
        public bool Submitted { get; set; }
    }

    public class UserInfoEntityConfiguration : EntityTypeConfiguration<UserInfoEntity>
    {
        public UserInfoEntityConfiguration()
        {
            Property(x => x.ChatId).HasMaxLength(50);
            Property(x => x.FirstName).HasMaxLength(50);
            Property(x => x.LastName).HasMaxLength(100);
            Property(x => x.Mobile).HasMaxLength(11);
            Property(x => x.NationalCode).HasMaxLength(10);
            Property(x => x.BirthDate).HasMaxLength(100);

            Property(x => x.City).HasMaxLength(100);
            Property(x => x.Province).HasMaxLength(100);
            Property(x => x.Address).HasMaxLength(1000);
            Property(x => x.PostalCode).HasMaxLength(10);

            Property(x => x.FavoritePost).HasMaxLength(50);
            Property(x => x.Resident).HasMaxLength(50);
            Property(x => x.VideoStatus).HasMaxLength(1000);


            HasIndex(x => x.ChatId).IsUnique(false);

            ToTable("UserInfo");
        }
    }
}
