using Contracts.Base;
using Core.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Device : FullAduitbaseModel
    {
        public string? DomainIDIfExists { get; set; }
        public string? Owner { get; set; }
        public string? PhoneNmber { get; set; }
        public string Type { get; set; }
        public string MAC { get; set; }
        public string CPU { get; set; }
        public string GPU { get; set; }
        public string  RAMTotal { get; set; }

        public DeviceStatus DeviceStatus { get; set; } = DeviceStatus.WithOwner;


        [ForeignKey(nameof(Office))]
        public int OfficeId { get; set; }
        public Office Office { get; set; }
        public ICollection<DeviceFailureHistory> DeviceFailureHistories { get; set; } = new HashSet<DeviceFailureHistory>();
    }
}
