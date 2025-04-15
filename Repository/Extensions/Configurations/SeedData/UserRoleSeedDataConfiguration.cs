using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Extensions.Configurations.SeedData
{
    public class UserRoleSeedDataConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "41DE9DCE-5A19-4C25-B336-8BA113BC9886",
                    UserId = "41DE9DCE-5A19-4C25-B336-8BA113BC9886"
                }
                );
        }
    }
}
