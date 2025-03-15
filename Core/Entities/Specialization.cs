using Contracts.Base;

namespace Core.Entities
{
    public class Specialization : SoftDeletedIdentityModel
    {
      
        public ICollection<Stuff> Stuff { get; set; } = new HashSet<Stuff>();
        
    }
}
