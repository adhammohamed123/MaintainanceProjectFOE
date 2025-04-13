using Core.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Service.DTOs.DeviceDtos
{
    public class DeviceForUpdateDto
    {
        public int Id { get; set; }
        [MaxLength(8)]
        public string? DomainIDIfExists { get; set; }
        [MaxLength(50)]
        public string? Owner { get; set; }
        [MaxLength(11)]
        public string? PhoneNmber { get; set; }
        [MaxLength(20)]
        public string Type { get; set; }
        [MaxLength(20)]
        public string MAC { get; set; }
        [MaxLength(100)]
        public string CPU { get; set; }
        [MaxLength(100)]
        public string GPU { get; set; }
        [MaxLength(8)]
        public string RAMTotal { get; set; }
        public DeviceStatus DeviceStatus { get; set; }


    }

}
