using Core.Entities.Enums;

namespace Service.DTOs.DeviceDtos
{
    public class DeviceDto
    {
        public string CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? LastModifiedUserId { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int Id { get; set; }
        public string DomainIDIfExists { get; set; }
        public string Owner { get; set; }
        public string PhoneNmber { get; set; }
        public string Type { get; set; }
        public string MAC { get; set; }
        public string CPU { get; set; }
        public string GPU { get; set; }
        public string RAMTotal { get; set; }
        public NameWithIdentifierDto Office { get; set; }
        public NameWithIdentifierDto Department { get; set; }
        public NameWithIdentifierDto Gate { get; set; }
        public NameWithIdentifierDto Region { get; set; }
        public DeviceStatus DeviceStatus { get; set; }
    }

}
