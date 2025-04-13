using AutoMapper;
using Contracts.Base;
using Core.Entities;
using Core.Entities.Enums;
using Core.Features;
using Service.DTOs;
using Service.DTOs.DeviceDtos;
using Service.DTOs.MaintainanceDtos;
using Service.DTOs.UserDtos;
using System.Collections.Generic;
using System.Linq;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<SoftDeletedIdentityModel, NameWithIdentifierDto>();
        CreateMap<Device, DeviceDto>()
            .ForMember(d => d.Office, opt => opt.MapFrom(src => new NameWithIdentifierDto(src.OfficeId, src.Office.Name)))
            .ForMember(d => d.Department, opt => opt.MapFrom(src => new NameWithIdentifierDto(src.Office.DepartmentId, src.Office.Department.Name)))
            .ForMember(d => d.Gate, opt => opt.MapFrom(src => new NameWithIdentifierDto(src.Office.Department.GateId, src.Office.Department.Gate.Name)))
            .ForMember(d => d.Region, opt => opt.MapFrom(src => new NameWithIdentifierDto(src.Office.Department.Gate.RegionId, src.Office.Department.Gate.Region.Name)));

        CreateMap<DeviceForCreationDto, Device>();
		CreateMap<Region, RegionDto>().ReverseMap();
        CreateMap<Gate, GateDto>();
        CreateMap<Department, DepartmentDto>();
        CreateMap<Office, OfficeDto>();
		CreateMap<DeviceFailureHistoryForCreationDto, DeviceFailureHistory>();
        CreateMap<UserForRegistrationDto, User>();
        CreateMap<DeviceFailureHistory, DeviceFailureHistoryDto>()
                 .ForMember(dest => dest.FailureMaintains, opt =>
                opt.MapFrom(src => src.FailureMaintains.Select
                (fm => new FailureDto(fm.FailureId, fm.Failure.Name, fm.FailureActionDone))))
                 .ForMember(d => d.ReceiverName, opt => opt.MapFrom(s => s.Receiver.Name))
                 .ForMember(d => d.MaintainerName, opt => opt.MapFrom(s => s.Maintainer.Name));
                 //.ForMember(d=>d.MAC,opt=>opt.MapFrom(s=>s.Device.MAC))
                 //.ForMember(d=>d.DomainIDIfExists,opt=>opt.MapFrom(s=>s.Device.DomainIDIfExists));
        CreateMap<DeviceFailureHistoryDto, DeviceFailureHistory>()
            .ForMember(d=>d.FailureMaintains,
            op=>op.MapFrom(s=>s.FailureMaintains.Select(h=> new FailureMaintain() 
            { FailureId = h.id, DeviceFailureHistoryId = s.Id,FailureActionDone = h.State }) ));

    }
}