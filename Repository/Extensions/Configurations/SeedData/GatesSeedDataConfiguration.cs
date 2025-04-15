using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Extensions.Configurations.SeedData
{
    public class GatesSeedDataConfiguration : IEntityTypeConfiguration<Gate>
    {
        public void Configure(EntityTypeBuilder<Gate> builder)
        {
            builder.HasData(
                new Gate
                {
                    Id = 1,
                    RegionId = 2,
                    Name = "العراق"
                },
                new Gate
                {
                    Id = 2,
                    RegionId = 2,
                    Name = "الامارات"
                }
                , new Gate
                {
                    Id = 3,
                    RegionId = 2,
                    Name = "السعودية",
                }
                );
        }
    }
}
