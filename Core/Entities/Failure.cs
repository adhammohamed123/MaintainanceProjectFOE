using Contracts.Base;

namespace Core.Entities
{
    public class Failure : SoftDeletedIdentityModel
    {
        public ICollection<FailureMaintain> FailureMaintains { get; set; } = new HashSet<FailureMaintain>();
    }
}
