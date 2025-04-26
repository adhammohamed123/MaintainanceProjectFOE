using Core.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Service.DTOs.MaintainanceDtos
{
    public class DeviceFailureHistoryForCreationDto
    {
        [Required(ErrorMessage = "رقم الجهاز مطلوب")]
        public int DeviceId { get; set; }
        [Required(ErrorMessage = "رقم معرف المستلم مطلوب ")]
        public string ReceiverID { get; set; }
        public string? MaintainerId { get; set; }

        public List<int> FailureIds { get; set; } = new();
        [Required(ErrorMessage = "اسم العميل مطلوب")]
        [MaxLength(50)]
        public string Delievry { get; set; }
        [Required(ErrorMessage = "رقم هاتف العميل مطلوب")]
        [StringLength(11, MinimumLength = 11)]
        public string DelievryPhoneNumber { get; set; }
        public string? Notes { get; set; }
        public MaintainOperationLocation MaintainLocation { get; set; }
    }
}
