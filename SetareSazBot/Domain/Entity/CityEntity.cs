using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace SetareSazBot.Domain.Entity
{
    public class CityEntity : BaseEntity
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public ProvinceEntity Province { get; set; }
        public long ProvinceId { get; set; }

    }

    public class CityEntityConfiguration : EntityTypeConfiguration<CityEntity>
    {
        public CityEntityConfiguration()
        {
            Property(x => x.Name).HasMaxLength(50);

            HasIndex(x => x.Name).IsUnique(false);

            ToTable("Cities");
        }
    }
}