using Contracts.Base;

namespace Core.Entities
{
    public class Specialization : SoftDeletedIdentityModel
    {
      
        public ICollection<User> User { get; set; } = new HashSet<User>();
        
    }
}
