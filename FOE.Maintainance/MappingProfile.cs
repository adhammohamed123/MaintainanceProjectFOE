using AutoMapper;
using Contracts.Base;
using Core.Entities;
using Core.Entities.Enums;
using Core.Features;
using Service.DTOs;
using System.Collections.Generic;
using System.Linq;

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
        CreateMap<DeviceFailureHistoryDto, DeviceFailureHistory>();
		CreateMap<DeviceFailureHistoryForCreationDto, DeviceFailureHistory>();
        CreateMap<UserForRegistrationDto, User>();
        CreateMap<DeviceFailureHistory, DeviceFailureHistoryDto>()
                 .ForMember(dest => dest.FailureMaintains, opt =>
                 opt.MapFrom(src => src.FailureMaintains.Select(fm => new FailureDto(fm.Failure.Name, fm.FailureActionDone))));
        CreateMap<DeviceFailureHistoryDto, DeviceFailureHistory>();

    }
}