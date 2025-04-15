using Core.Entities;
using Core.Features;

namespace Core.RepositoryContracts
{
    public interface IDeviceRepo
    {
        Device? GetById(int id, bool trackchanges);
        PagedList<Device> GetAllDevices(DeviceRequestParameters deviceRequestParameters,bool trackchanges);
        IQueryable<Device> GetAllRegisteredDevicesInSpecificOffice(int officeId,bool trackchanges);
        Device? GetById(int officeId,int id,bool trackchanges);
        Task CreateDevice(int officeId,Device device,string UserId);
        void DeleteDevice(Device device,string UserId);
	}

}
