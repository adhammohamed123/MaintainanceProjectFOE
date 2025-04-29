using AutoMapper;
using Contracts.Base;
using Core.Entities;
using Core.Exceptions;
using Core.Features;
using Core.RepositoryContracts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Service.DTOs.DeviceDtos;
using Service.Services;
using System.Threading.Tasks;
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
		  await  CheckParentExistance(regionId, gateId, deptId, officeId, trackchanges);
			var deviceEntity = mapper.Map<Device>(device);
			if((await repository.DeviceRepo.GetAllDevices(new DeviceRequestParameters(), trackchanges)).Any(x => x.MAC == deviceEntity.MAC))
                throw new DeviceAlreadyExistsException(deviceEntity.MAC);
            await  repository.DeviceRepo.CreateDevice(officeId, deviceEntity,UserID);
			await  repository.SaveAsync();
			return mapper.Map<DeviceDto>(deviceEntity);
		}

		public async Task DeleteDevice(int regionId, int gateId, int deptId, int officeId,int device, string UserID, bool trackchanges)
		{
			await CheckParentExistance(regionId, gateId, deptId, officeId, trackchanges);
			var deviceEntity =await GetObjectAndCheckExistance(officeId, device, trackchanges);
			var ifDeviceHasMaintainHistory =(await repository.MaintaninanceRepo.GetDeviceFailureHistoriesByDeviceId(deviceEntity.Id, trackchanges: false)).Count() > 0;
            if (ifDeviceHasMaintainHistory)
                throw new CannotDeleteParentObjectThatHasChildrenException(deviceEntity.MAC);
             repository.DeviceRepo.DeleteDevice(deviceEntity, UserID);
			await repository.SaveAsync();
		}

		public async Task<(IEnumerable<DeviceDto>devices,MetaData metadata)> GetAllDevices(DeviceRequestParameters deviceRequestParameters,bool trackchanges)
		{
			var devicesWithMeta=await repository.DeviceRepo.GetAllDevices(deviceRequestParameters,trackchanges);
			var devices = mapper.Map<IEnumerable<DeviceDto>>(devicesWithMeta);
			return (devices, devicesWithMeta.metaData);
		}
		public async Task<IEnumerable<DeviceDto>> GetAllRegisteredDevices(int regionId, int gateId, int deptId, int officeId, bool trackchanges)
		{
			await CheckParentExistance(regionId, gateId, deptId, officeId, trackchanges);
			var result = await repository.DeviceRepo.GetAllRegisteredDevicesInSpecificOffice(officeId, trackchanges);
			return mapper.Map<IEnumerable< DeviceDto>>(result);
		}

		public async Task<DeviceDto?> GetById(int regionId, int gateId, int deptId, int officeId, int id, bool trackchanges)
		{
			await CheckParentExistance(regionId, gateId, deptId, officeId, trackchanges);
			var device = await GetObjectAndCheckExistance(officeId, id, trackchanges);
			return mapper.Map<DeviceDto>(device);
		}

        public async Task UpdateDevice(int regionId, int gateId, int deptId, int officeId, DeviceForUpdateDto deviceForUpdateDto, string userId, bool v)
        {
            var Parents=await CheckParentExistance(regionId, gateId, deptId, officeId, false);
            var device =await GetObjectAndCheckExistance(officeId, deviceForUpdateDto.Id, true);
            if ((await repository.DeviceRepo.GetAllDevices(new DeviceRequestParameters(), false)).Any(x => x.MAC == device.MAC && x.Id!=device.Id))
                throw new DeviceAlreadyExistsException(deviceForUpdateDto.MAC);
            mapper.Map(deviceForUpdateDto, device);
			device.LastModifiedUserId = userId;
			//device.Office = Parents.office;
            //device.OfficeId = Parents.office.Id;
            await repository.SaveAsync();
        }

        private async Task<(Region region,Gate gate,Department dept,Office office )> CheckParentExistance(int regionId, int gateId, int deptId, int officeId,bool trackchanges)
		{
			var region= await repository.RegionRepo.GetRegionBasedOnId(regionId, trackchanges);
			if (region == null)
				throw new RegionNotFoundException(regionId);
			var gate = await repository.GateRepo.GetSpecificGate(regionId,gateId, trackchanges);
			if (gate == null)
				throw new GateNotFoundException(gateId);
			var dept = await repository.DepartmentRepo.GetDeptBasedOnId(gateId, deptId, trackchanges);
			if (dept == null)
				throw new DepartmentNotFoundException(deptId);
			var office =await repository.OfficeRepo.GetOfficeBasedOnId(deptId, officeId, trackchanges);
			if (office == null)
				throw new OfficeNotFoundException(officeId);
			return (region, gate, dept, office);
		}

		private async Task<Device> GetObjectAndCheckExistance(int officeId, int id, bool trackchanges)
		{
			var device = await repository.DeviceRepo.GetById(officeId, id, trackchanges);
			if (device == null)
				throw new DeviceNotFoundException(id);
			return device;
		}
	}
}
