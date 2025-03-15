using Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.DTOs
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
		public int OfficeId { get; set; }
		#region Collections Informations
		//public string OfficeName { get; set; }
  //      public string DepartmentName { get; set; }
  //      public string GateName { get; set; }
  //      public string RegionName { get; set; }
        #endregion
    }

   public record RegionDto(string Name,int Id);

	public record GateDto(int Id,string Name,int RegionId);
	public record DepartmentDto(int Id,string Name,int GateId);

	public record OfficeDto(int Id,string Name,int DepartmentId);

	public record NameWithIdentifierDto(int Id,string Name);

}
