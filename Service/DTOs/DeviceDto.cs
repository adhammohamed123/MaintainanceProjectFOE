using Core.Entities;
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
    }
	
	public record RegionDto(string Name,int Id);

	public record GateDto(int Id,string Name,int RegionId);
	public record DepartmentDto(int Id,string Name,int GateId);

	public record OfficeDto(int Id,string Name,int DepartmentId);

	public record NameWithIdentifierDto(int Id,string Name);


	public class DeviceFailureHistoryForCreationDto
	{
		public int DeviceId { get; set; }
		public int ReceiverID { get; set; }
		public List<int> FailureIds { get; set; } = new();
		public string Delievry { get; set; }
		public string DelievryPhoneNumber { get; set; }
		public string? Notes { get; set; }
	}

	public class DeviceFailureHistoryDto
	{
		public int Id { get; set; }
		public int DeviceId { get; set; }
		public int ReceiverID { get; set; }
		public List<string> Failures { get; set; } = new();
		public string Delievry { get; set; }
		public string DelievryPhoneNumber { get; set; }
		public string? Notes { get; set; }
		public int? MaintainerId { get; set; }
		public bool IsProblemSolved { get; set; }
		public bool IsDeliverd { get; set; }
		public string CreatedByUserId { get; set; }
		public DateTime CreatedDate { get; set; }
		public string? LastModifiedUserId { get; set; }
		public DateTime? LastModifiedDate { get; set; }
		public bool IsDeleted { get; set; }
	}


}
