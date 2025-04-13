using AutoMapper;
using Contracts.Base;
using Core.Entities;
using Core.Exceptions;
using Core.Features;
using Core.RepositoryContracts;
using Service.DTOs.DeviceDtos;
using Service.Services;
namespace Service
{
    public class DeviceService : IDeviceService
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;
        private readonly ILoggerManager logger;

        public DeviceService(IRepositoryManager repository, IMapper mapper,ILoggerManager logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }

		public async Task<DeviceDto> CreateDevice(int regionId, int gateId, int deptId, int officeId, DeviceForCreationDto device, string UserID,bool trackchanges)
		{
			CheckParentExistance(regionId, gateId, deptId, officeId, trackchanges);
			var deviceEntity = mapper.Map<Device>(device);
			await  repository.DeviceRepo.CreateDevice(officeId, deviceEntity,UserID);
	        await  repository.SaveAsync();
			return mapper.Map<DeviceDto>(deviceEntity);
		}

		public async Task DeleteDevice(int regionId, int gateId, int deptId, int officeId,int device, string UserID, bool trackchanges)
		{
			CheckParentExistance(regionId, gateId, deptId, officeId,trackchanges);
			var deviceEntity = GetObjectAndCheckExistance(officeId, device, trackchanges);
			repository.DeviceRepo.DeleteDevice(deviceEntity,UserID);
			await repository.SaveAsync();
		}

		public (IEnumerable<DeviceDto>devices,MetaData metadata) GetAllDevices(DeviceRequestParameters deviceRequestParameters,bool trackchanges)
		{
			var devicesWithMeta= repository.DeviceRepo.GetAllDevices(deviceRequestParameters,trackchanges);
			var devices = mapper.Map<IEnumerable<DeviceDto>>(devicesWithMeta);
			return (devices, devicesWithMeta.metaData);
		}
		public IEnumerable<DeviceDto> GetAllRegisteredDevices(int regionId, int gateId, int deptId, int officeId, bool trackchanges)
		{
			CheckParentExistance(regionId, gateId, deptId, officeId, trackchanges);
			var result = repository.DeviceRepo.GetAllRegisteredDevicesInSpecificOffice(officeId, trackchanges).ToList();
			return mapper.Map<IEnumerable< DeviceDto>>(result);
		}

		public DeviceDto? GetById(int regionId, int gateId, int deptId, int officeId, int id, bool trackchanges)
		{
			CheckParentExistance(regionId, gateId, deptId, officeId, trackchanges);
			var device = GetObjectAndCheckExistance(officeId, id, trackchanges);
			return mapper.Map<DeviceDto>(device);
		}

        public async Task UpdateDevice(int regionId, int gateId, int deptId, int officeId, DeviceForUpdateDto deviceForUpdateDto, string userId, bool v)
        {
            var Parents= CheckParentExistance(regionId, gateId, deptId, officeId, false);
            var device = GetObjectAndCheckExistance(officeId, deviceForUpdateDto.Id, true);
			mapper.Map(deviceForUpdateDto, device);
			device.LastModifiedUserId = userId;
			device.Office = Parents.office;
            device.OfficeId = Parents.office.Id;
            await repository.SaveAsync();
        }

        private (Region region,Gate gate,Department dept,Office office ) CheckParentExistance(int regionId, int gateId, int deptId, int officeId,bool trackchanges)
		{
			var region=repository.RegionRepo.GetRegionBasedOnId(regionId, trackchanges);
			if (region == null)
				throw new RegionNotFoundException(regionId);
			var gate = repository.GateRepo.GetSpecificGate(regionId,gateId, trackchanges);
			if (gate == null)
				throw new GateNotFoundException(gateId);
			var dept = repository.DepartmentRepo.GetDeptBasedOnId(gateId, deptId, trackchanges);
			if (dept == null)
				throw new DepartmentNotFoundException(deptId);
			var office = repository.OfficeRepo.GetOfficeBasedOnId(deptId, officeId, trackchanges);
			if (office == null)
				throw new OfficeNotFoundException(officeId);
			return (region, gate, dept, office);
		}

		private Device GetObjectAndCheckExistance(int officeId, int id, bool trackchanges)
		{
			var device = repository.DeviceRepo.GetById(officeId, id, trackchanges);
			if (device == null)
				throw new DeviceNotFoundException(id);
			return device;
		}
	}
}
