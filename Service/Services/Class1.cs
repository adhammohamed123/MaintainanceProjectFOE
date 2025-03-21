using Core.Entities;
using Core.Features;
using Service.DTOs;
namespace Service.Services
{

   
    public interface IMaintaninanceService
    {
		(IEnumerable<DeviceFailureHistoryDto> maintainRecords,MetaData metaData) GetAllAsync(MaintainanceRequestParameters maintainanceRequestParameters,bool trackchanges);
        IEnumerable<DeviceFailureHistoryDto> GetDeviceFailureHistoriesByDeviceId(int deviceId, bool trackchanges);
		DeviceFailureHistoryDto? GetByIdAsync(int id);
		Task<DeviceFailureHistoryDto> CreateAsync(DeviceFailureHistoryForCreationDto dto);
        (DeviceFailureHistoryDto dto,DeviceFailureHistory entity) GetDeviceFailureHistoryByIdForPartialUpdate(int id, bool trackchanges);
        Task SavePartialUpdate(DeviceFailureHistoryDto dto,DeviceFailureHistory entity);
	}
  
    
    
    public interface IStuffService
    {
		IQueryable<NameWithIdentifierDto> GetAllStuff(bool trackchanges);
		NameWithIdentifierDto? GetFromStuffById(int id, bool trackchanges);
		Task<NameWithIdentifierDto> CreateStuff(string name);
        Task DeleteStuff(int id,bool trackchanges);
	}
    public interface ISpecializationService
    {

    }
}
