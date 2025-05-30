﻿using Contracts.Base;
using Core.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Index(nameof(Delievry))]
    public class DeviceFailureHistory :FullAduitbaseModel
    {
        #region Device
        [ForeignKey(nameof(Device))]
        public int DeviceId { get; set; }
        public Device Device { get; set; } 
        #endregion

        public ICollection<FailureMaintain> FailureMaintains { get; set; } = new HashSet<FailureMaintain>();

        #region MaintainerAndReceiver
        [ForeignKey(nameof(Maintainer)), MaxLength(64)]
        public string? MaintainerId { get; set; }
        public User? Maintainer { get; set; }

        [ForeignKey(nameof(Receiver)), MaxLength(64)]
        public string ReceiverID { get; set; }
        public User Receiver { get; set; }

        #endregion
        [MaxLength(50)]
        public string Delievry { get; set; }
        
       [MaxLength(11)]
        public string DelievryPhoneNumber { get; set; }
        [MaxLength(500)]
        public string? Notes { get; set; }
       // public bool IsProblemSolved { get; set; }
       // public bool IsDeliverd { get; set; }
       
        public MaintainOperationLocation MaintainLocation{ get; set; }
        public MaintainStatus State { get; set; }= MaintainStatus.WorkingOnIt;
        public bool IsDelivered { get; set; }
    }
}
