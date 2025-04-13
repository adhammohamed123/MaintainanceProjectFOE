using Core.Entities;
using Core.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.DTOs
{
    public record RegionDto(string Name,int Id);
	public record GateDto(int Id,string Name,int RegionId);
	public record DepartmentDto(int Id,string Name,int GateId);
	public record OfficeDto(int Id,string Name,int DepartmentId);
	public record NameWithIdentifierDto(int Id,string Name);

}
