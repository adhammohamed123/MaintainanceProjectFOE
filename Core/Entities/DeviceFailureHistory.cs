using Contracts.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class DeviceFailureHistory :FullAduitbaseModel
    {
        [ForeignKey(nameof(Device))]
        public int DeviceId { get; set; }
        public Device Device { get; set; }

        public ICollection<Failure> Failures { get; set; } = new HashSet<Failure>();

        [ForeignKey(nameof(Maintainer))]
        public string? MaintainerId { get; set; }
        public User? Maintainer { get; set; }

        [ForeignKey(nameof(Receiver))]
        public string ReceiverID { get; set; }
        public User Receiver { get; set; }

        public string Delievry { get; set; }
        public string DelievryPhoneNumber { get; set; }

       
        public string? Notes { get; set; }
        public bool IsProblemSolved { get; set; }

        public bool IsDeliverd { get; set; }
    }
}
