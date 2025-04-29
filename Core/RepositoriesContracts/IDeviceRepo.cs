using Core.Entities;
using Core.Features;

namespace Core.RepositoryContracts
{
    public interface IDeviceRepo
    {
        Task<Device?> GetById(int id, bool trackchanges);
        Task<PagedList<Device>> GetAllDevices(DeviceRequestParameters deviceRequestParameters,bool trackchanges);
        Task<IEnumerable<Device>> GetAllRegisteredDevicesInSpecificOffice(int officeId,bool trackchanges);
        Task<Device?> GetById(int officeId,int id,bool trackchanges);
        Task CreateDevice(int officeId,Device device,string UserId);
        void DeleteDevice(Device device,string UserId);
	}

}
