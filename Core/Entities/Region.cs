using Contracts.Base;

namespace Core.Entities
{
    public class Region : SoftDeletedIdentityModel
    {
        public ICollection<Gate> Gates { get; set; } = new HashSet<Gate>();
    }
}
