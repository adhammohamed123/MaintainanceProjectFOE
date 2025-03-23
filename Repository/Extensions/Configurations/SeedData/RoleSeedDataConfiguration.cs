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
                    UserName = "Admin",
                    Name = "Admin",
                    NormalizedUserName = "ADMIN",
                    Email = "Admin@Admin.com",
                    PasswordHash = "SuperAdmin",
                });
        }
    }
}
