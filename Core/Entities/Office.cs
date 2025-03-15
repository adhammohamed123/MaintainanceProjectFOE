using Contracts.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Office : SoftDeletedIdentityModel
    {
        [ForeignKey(nameof(Department))]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        // public ICollection<Department> Departments { get; set; } = new HashSet<Department>();
        public ICollection<Device> Devices { get; set; } = new HashSet<Device>();
    }
}
