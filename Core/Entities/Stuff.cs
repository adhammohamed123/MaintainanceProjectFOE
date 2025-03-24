using Contracts.Base;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public abstract class FullAduitbaseModel : IIdentityModel, IAuditedModel, ISoftDeletedModel
    {
        [ForeignKey(nameof(User))]
        public string CreatedByUserId { get; set; }
        public User CreatedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        [ForeignKey(nameof(LastModifiedUser))]
        public string? LastModifiedUserId { get; set; }
        public User LastModifiedUser { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int Id { get; set; }
    }

    public class User : IdentityUser,ISoftDeletedModel
    {
        // user related to Department
        public ICollection<Specialization> Specializations { get; set; } = new HashSet<Specialization>();
        public bool IsDeleted { get ; set ; }
        public string Name { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

    }
}
