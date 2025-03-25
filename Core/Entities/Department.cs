using Contracts.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Department : SoftDeletedIdentityModel
    {
        [ForeignKey("Gate")]
        public int GateId { get; set; }
        public Gate Gate { get; set; }
        // public ICollection<Gate> Gates { get; set; } = new HashSet<Gate>();
        public ICollection<Office> Offices { get; set; } = new HashSet<Office>();
        public ICollection<User> StuffUsers { get; set; } = new HashSet<User>();
    }
}
