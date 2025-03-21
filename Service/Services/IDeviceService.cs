using Core.Features;
using Service.DTOs;
namespace Service.Services
{
	public interface IDeviceService
    {
        (IEnumerable<DeviceDto>devices,MetaData metadata) GetAllDevices(DeviceRequestParameters deviceRequestParameters, bool trackchanges);
		IEnumerable<DeviceDto> GetAllRegisteredDevices(int regionId, int gateId, int deptId, int officeId, bool trackchanges);
		DeviceDto? GetById(int regionId, int gateId, int deptId, int officeId, int id, bool trackchanges);
		Task<DeviceDto> CreateDevice(int regionId, int gateId, int deptId, int officeId, DeviceForCreationDto device,string UserID,bool trackchanges);
		Task DeleteDevice(int regionId, int gateId, int deptId, int officeId,int device,string UserID,bool trackchanges);
	}
}
