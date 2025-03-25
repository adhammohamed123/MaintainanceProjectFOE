using Core.Entities;
using Core.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.DTOs
{

	public record DeviceForCreationDto(string? DomainIDIfExists, string? Owner, string? PhoneNmber, string Type, string MAC, string CPU, string GPU, string RAMTotal);

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
		public int OfficeId { get; set; }
        public DeviceStatus DeviceStatus { get; set; }
    }
	
	public record RegionDto(string Name,int Id);

	public record GateDto(int Id,string Name,int RegionId);
	public record DepartmentDto(int Id,string Name,int GateId);

	public record OfficeDto(int Id,string Name,int DepartmentId);

	public record NameWithIdentifierDto(int Id,string Name);


	public class DeviceFailureHistoryForCreationDto
	{
		public int DeviceId { get; set; }
		public List<int> FailureIds { get; set; } = new();
		public string Delievry { get; set; }
		public string DelievryPhoneNumber { get; set; }
		public string? Notes { get; set; }
        public MaintainOperationLocation MaintainLocation { get; set; }
    }

	public record FailureDto(string Name,FailureActionDone State);
    public class DeviceFailureHistoryDto
	{
		public int Id { get; set; }
		public int DeviceId { get; set; }
		public string ReceiverID { get; set; }
		public List<FailureDto> FailureMaintains { get; set; } = new();
		public string Delievry { get; set; }
		public string DelievryPhoneNumber { get; set; }
		public string? Notes { get; set; }
		public string? MaintainerId { get; set; }
		
		public string CreatedByUserId { get; set; }
		public DateTime CreatedDate { get; set; }
		public string? LastModifiedUserId { get; set; }
		public DateTime? LastModifiedDate { get; set; }
		public bool IsDeleted { get; set; }
        public MaintainOperationLocation MaintainLocation { get; set; }
        public MaintainStatus State { get; set; }
    }

    
    public record UserForRegistrationDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; init; }
        [Required(ErrorMessage = "Username is required")]
        public string? UserName { get; init; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; init; }
		[Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string ConfirmPass { get; set; }
        public string? Email { get; init; }
        public string? PhoneNumber { get; init; }
        public int DepartmentId { get; set; }
        public ICollection<string>? Roles { get; init; }
    }
    public record UserForAuthenticationDto
    {
        [Required(ErrorMessage = "User name is required")]
        public string? UserName { get; init; }
        [Required(ErrorMessage = "Password name is required")]
        public string? Password { get; init; }
    }
    public record TokenDto(string AccessToken, string RefreshToken);

}
