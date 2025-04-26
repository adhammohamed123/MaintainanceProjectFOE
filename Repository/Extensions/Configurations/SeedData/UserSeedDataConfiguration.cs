using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Extensions.Configurations.SeedData
{
    public class UserSeedDataConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var hasher = new PasswordHasher<User>();

            var user = new User
            {
                Id = "41DE9DCE-5A19-4C25-B336-8BA113BC9886",
                UserName = "Admin",
                Name = "ElD0ma",
                NormalizedUserName = "ADMIN",
                Email = "adhammo909@gmail.com",
                //DepartmentId = 1
            };
            user.PasswordHash = hasher.HashPassword(user, "SuperAdmin123");
            builder.HasData(user);
            builder.Property(u => u.Id).HasMaxLength(64);
        }
    }
}
