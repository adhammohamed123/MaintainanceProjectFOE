using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Extensions.Configurations.SeedData
{
    public class RegionsSeedDataConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.HasData(
                new Region
                {
                    Id = 1,
                    Name = "جهاز مستقبل مصر"
                },
                new Region
                {
                    Id = 2,
                    Name = "الضبعة"
                }
                );
        }
    }
}
