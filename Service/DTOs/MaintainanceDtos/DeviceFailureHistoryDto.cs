using Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.MaintainanceDtos
{
    public class DeviceFailureHistoryDto
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public string? DomainIDIfExists { get; set; }
        public string MAC { get; set; }
        public string ReceiverID { get; set; }
        public string? ReceiverName { get; set; }
        public List<FailureDto> FailureMaintains { get; set; } = new();
        //public List<Failure> Failures { get; set; } = new List<Failure>();
        public string Delievry { get; set; }
        public string DelievryPhoneNumber { get; set; }
        public string? Notes { get; set; }
        public string? MaintainerId { get; set; }
        public string? MaintainerName { get; set; }

        public string CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? LastModifiedUserId { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public MaintainOperationLocation MaintainLocation { get; set; }
        public MaintainStatus State { get; set; }
        public bool IsDelivered { get; set; }
    }
}
