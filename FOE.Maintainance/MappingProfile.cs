using AutoMapper;
using Contracts.Base;
using Core.Entities;
using Service.DTOs;

public class MappingProfile:Profile
{
    public MappingProfile()
    {

        CreateMap<SoftDeletedIdentityModel, NameWithIdentifierDto>();

        CreateMap<Device, DeviceDto>();
            

        CreateMap<Region, RegionDto>();
        CreateMap<Gate, GateDto>();
        CreateMap<Department, DepartmentDto>();

        CreateMap<Office, OfficeDto>();
         


    }
}