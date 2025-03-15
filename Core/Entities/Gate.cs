using Contracts.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Gate : SoftDeletedIdentityModel
    {
        // public ICollection<Region> Regions { get; set; } = new HashSet<Region>();
        [ForeignKey("Region")]
        public int RegionId { get; set; }
        public Region Region { get; set; }
        public ICollection<Department> Departments { get; set; } = new HashSet<Department>();
    }
}
