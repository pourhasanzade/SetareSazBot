using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace SetareSazBot.Domain.Entity
{
    public class ProvinceEntity : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<CityEntity> CityCollection { get; set; }
    }

    public class ProvinceEntityConfiguration : EntityTypeConfiguration<ProvinceEntity>
    {
        public ProvinceEntityConfiguration()
        {
            Property(x => x.Name).HasMaxLength(50);

            HasIndex(x => x.Name).IsUnique(false);

            ToTable("Provinces");
        }
    }
}