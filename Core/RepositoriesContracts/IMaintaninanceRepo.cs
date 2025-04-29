using Core.Entities;
using Core.Features;

namespace Core.RepositoryContracts
{
    public interface IMaintaninanceRepo
		{
		  PagedList<DeviceFailureHistory> GetDeviceFailureHistories(MaintainanceRequestParameters maintainanceRequestParameters,bool trackchanges);
          Task<IEnumerable<DeviceFailureHistory>> GetDeviceFailureHistoriesByDeviceId(int deviceId, bool trackchanges);
          Task<DeviceFailureHistory?> GetDeviceFailureHistoryById(int id, bool trackchanges);
          Task RegisterNew(DeviceFailureHistory deviceFailureHistory);
          void DeleteMaintainance(DeviceFailureHistory deviceFailureHistory,string userId);
    }

}
