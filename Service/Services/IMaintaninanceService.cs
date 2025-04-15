using Core.Entities;
using Core.Entities.Enums;
using Core.Features;
using Service.DTOs.MaintainanceDtos;
namespace Service.Services
{

   
    public interface IMaintaninanceService
    {
		(IEnumerable<DeviceFailureHistoryDto> maintainRecords,MetaData metaData) GetAllAsync(MaintainanceRequestParameters maintainanceRequestParameters,bool trackchanges);
        IEnumerable<DeviceFailureHistoryDto> GetDeviceFailureHistoriesByDeviceId(int deviceId, bool trackchanges);
		DeviceFailureHistoryDto? GetByIdAsync(int id);
		Task<DeviceFailureHistoryDto> CreateAsync(DeviceFailureHistoryForCreationDto dto,string userId);
        (DeviceFailureHistoryDto dto,DeviceFailureHistory entity) GetDeviceFailureHistoryByIdForPartialUpdate(int id, bool trackchanges);
        Task SavePartialUpdate(DeviceFailureHistoryDto dto,DeviceFailureHistory entity,string userId);
        Task UpdateMaintainanceRecord(DeviceFailureHistoryDto dto, string userId);
        Task MakeDeviceDone(int MaintainId,string userId);
        Task ChangeFailureStatus(int MaintainId, int FailureId, FailureActionDone status); 
        Task DeleteMaintain(int MaintainId,string userId);
    }
}
