using Contracts.Base;

namespace Core.Entities
{
    public class Failure : SoftDeletedIdentityModel
    {
        public ICollection<DeviceFailureHistory> DeviceFailureHistories { get; set; } = new HashSet<DeviceFailureHistory>();
    }
}
