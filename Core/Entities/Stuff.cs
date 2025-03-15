using Contracts.Base;

namespace Core.Entities
{
    public class Stuff : SoftDeletedIdentityModel
    {
        public ICollection<Specialization> Specializations { get; set; } = new HashSet<Specialization>();
    }
}
