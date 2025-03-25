using Contracts.Base;
using Core.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class FailureMaintain:ISoftDeletedModel
    {
        [ForeignKey(nameof(Failure))]
        public int FailureId { get; set; }
        public Failure Failure { get; set; }

        [ForeignKey(nameof(DeviceFailureHistory))]
        public int DeviceFailureHistoryId { get; set; }
        public DeviceFailureHistory DeviceFailureHistory { get; set; }
        public bool  IsDeleted { get; set; }
        public FailureActionDone FailureActionDone { get; set; } = FailureActionDone.NotSolved;
    }
}
