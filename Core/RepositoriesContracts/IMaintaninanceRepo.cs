using Core.Entities;
using Core.Features;

namespace Core.RepositoryContracts
{
    public interface IMaintaninanceRepo
		{
		  PagedList<DeviceFailureHistory> GetDeviceFailureHistories(MaintainanceRequestParameters maintainanceRequestParameters,bool trackchanges);
          IQueryable<DeviceFailureHistory> GetDeviceFailureHistoriesByDeviceId(int deviceId, bool trackchanges);
          DeviceFailureHistory? GetDeviceFailureHistoryById(int id, bool trackchanges);
          Task RegisterNew(DeviceFailureHistory deviceFailureHistory);
          void DeleteMaintainance(DeviceFailureHistory deviceFailureHistory,string userId);
    }

}
