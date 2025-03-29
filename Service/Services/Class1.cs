using Core.Entities;
using Core.Features;
using Microsoft.AspNetCore.Identity;
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
        Task SavePartialUpdate(DeviceFailureHistoryDto dto,DeviceFailureHistory entity,string UserId);
        Task UpdateMaintainanceRecord(DeviceFailureHistoryDto dto);
        Task MakeDeviceDone(int deviceId);
    }
  
    
    
    public interface IUserService
    {
		IQueryable<User> GetAllUser(bool trackchanges);
		User? GetFromUserById(string id, bool trackchanges);
		//Task<NameWithIdentifierDto> CreateUser(string name);
        Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration);
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<TokenDto> CreateToken(bool populateExp);
        Task<TokenDto> RefreshToken(TokenDto tokenDto);
        Task DeleteUser(string id,bool trackchanges);
        Task AssociateUserWithSpecialization(string userId, int specializationId);

    }
    public interface ISpecializationService
    {
        IQueryable<NameWithIdentifierDto> GetAllSpecializations(bool trackchanges);
        NameWithIdentifierDto? GetSpecializationById(int id, bool trackchanges);
        Task<NameWithIdentifierDto> CreateSpecialization(string specializationName);
        Task DeleteSpecialization(int id);
    }
}
