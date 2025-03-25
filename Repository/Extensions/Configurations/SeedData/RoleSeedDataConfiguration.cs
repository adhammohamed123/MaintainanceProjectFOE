using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions.Configurations.SeedData
{
    public class RoleSeedDataConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(

                new IdentityRole
                {
                    Id = "41DE9DCE-5A19-4C25-B336-8BA113BC9886",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = "C0969547-A084-4839-836C-F41F4CF5D2DC",
                    Name = "User",
                    NormalizedName = "USER"
                }
                );
        }
    }
    public class UserSeedDataConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User
                {
                    Id = "41DE9DCE-5A19-4C25-B336-8BA113BC9886",
                    UserName = "Super",
                    Name = "ElD0ma",
                    NormalizedUserName = "SUPER",
                    Email = "adhammo909@gmail.com",
                    PasswordHash = "SuperAdmin123",
                    DepartmentId = 1
                });
        }
    }
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
                ,new Gate
                {
                    Id = 3,
                    RegionId = 2,
                    Name = "السعودية",
                }
                );
        }
    }
    public class DepartmentSeedDataConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasData(
                new Department
                {
                    Id = 1,
                    Name = "النظم و الرقمنة",
                    GateId = 3
                }
                );
        }
    }
}
