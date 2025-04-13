using Contracts.Base;
using Core.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Index(nameof(MAC),IsUnique =true)]
    [Index(nameof(DomainIDIfExists))]
    [Index(nameof(Owner))]
    public class Device : FullAduitbaseModel
    {
        [MaxLength(8)]
        public string? DomainIDIfExists { get; set; }
        [MaxLength(50)]
        public string? Owner { get; set; }
        [MaxLength(11)]
        public string? PhoneNmber { get; set; }
        [MaxLength(50)]
        public string Type { get; set; }
        [MaxLength(20)]
        public string MAC { get; set; }
        [MaxLength(100)]
        public string CPU { get; set; }
        [MaxLength(100)]
        public string GPU { get; set; }
        [MaxLength(8)]
        public string  RAMTotal { get; set; }

        public DeviceStatus DeviceStatus { get; set; } = DeviceStatus.WithOwner;


        [ForeignKey(nameof(Office))]
        public int OfficeId { get; set; }
        public Office Office { get; set; }
        public ICollection<DeviceFailureHistory> DeviceFailureHistories { get; set; } = new HashSet<DeviceFailureHistory>();
    }
}
