using Core.Features;
using Service.DTOs.DeviceDtos;
namespace Service.Services
{
	public interface IDeviceService
    {
        Task<(IEnumerable<DeviceDto>devices,MetaData metadata)> GetAllDevices(DeviceRequestParameters deviceRequestParameters, bool trackchanges);
		Task<IEnumerable<DeviceDto>> GetAllRegisteredDevices(int regionId, int gateId, int deptId, int officeId, bool trackchanges);
		Task<DeviceDto?> GetById(int regionId, int gateId, int deptId, int officeId, int id, bool trackchanges);
		Task<DeviceDto> CreateDevice(int regionId, int gateId, int deptId, int officeId, DeviceForCreationDto device,string UserID,bool trackchanges);
		Task DeleteDevice(int regionId, int gateId, int deptId, int officeId,int device,string UserID,bool trackchanges);
        Task UpdateDevice(int regionId, int gateId, int deptId, int officeId, DeviceForUpdateDto deviceForUpdateDto, string userId, bool v);
    }
}
