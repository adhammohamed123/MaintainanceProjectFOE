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
        CreateMap<DeviceForCreationDto, Device>();
		CreateMap<Region, RegionDto>().ReverseMap();
        CreateMap<Gate, GateDto>();
        CreateMap<Department, DepartmentDto>();
        CreateMap<Office, OfficeDto>();
        CreateMap<DeviceFailureHistory, DeviceFailureHistoryDto>()
             .ForMember(dest => dest.Failures, opt => opt.MapFrom(src => src.Failures.Select(f => f.Name)));
		CreateMap<DeviceFailureHistoryDto, DeviceFailureHistory>().ForMember(d=>d.Failures, opt => opt.Ignore());
		CreateMap<DeviceFailureHistoryForCreationDto, DeviceFailureHistory>();
        CreateMap<UserForRegistrationDto, User>();
       


    }
}