using Contracts.Base;
using Core.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class DeviceFailureHistory :FullAduitbaseModel
    {
        #region Device
        [ForeignKey(nameof(Device))]
        public int DeviceId { get; set; }
        public Device Device { get; set; } 
        #endregion

        public ICollection<FailureMaintain> FailureMaintains { get; set; } = new HashSet<FailureMaintain>();

        #region MaintainerAndReceiver
        [ForeignKey(nameof(Maintainer))]
        public string? MaintainerId { get; set; }
        public User? Maintainer { get; set; }

        [ForeignKey(nameof(Receiver))]
        public string ReceiverID { get; set; }
        public User Receiver { get; set; }

        #endregion

        public string Delievry { get; set; }
        
       // [Phone]
        public string DelievryPhoneNumber { get; set; }
       
        public string? Notes { get; set; }
       // public bool IsProblemSolved { get; set; }
       // public bool IsDeliverd { get; set; }
       
        public MaintainOperationLocation MaintainLocation{ get; set; }
        public MaintainStatus State { get; set; }= MaintainStatus.WorkingOnIt;
    }
}
