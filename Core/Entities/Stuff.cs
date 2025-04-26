using Contracts.Base;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public abstract class FullAduitbaseModel : IIdentityModel, IAuditedModel, ISoftDeletedModel
    {
        [ForeignKey(nameof(User)),MaxLength(64)]
        public string CreatedByUserId { get; set; }
        public User CreatedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        [ForeignKey(nameof(LastModifiedUser)),MaxLength(64)]
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
        [MaxLength(100)]
        public string Name { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        //[ForeignKey(nameof(Department))]
        //public int DepartmentId { get; set; }
        //public Department Department { get; set; }

    }
}
