using Core.Entities;
using Service.DTOs;
namespace Service.Services
{
    public interface IRegionService
    {
        Task<IEnumerable< RegionDto>> GetAllRegisteredRegion(bool trackchanges);
        Task<RegionDto> GetRegionByID(int id, bool trackchanges);
        Task<RegionDto> CreateNewRegionAsync(string name);
        Task DeleteRegionAsync(int id);
        Task UpdateRegion(int regionId,RegionDto regionDto);
       Task<(Region region,RegionDto regionDto)> GetRegionForPartialUpdate(int regionId,bool trackchanges);
        Task SavePatchChanges(Region region,RegionDto regionDto);
	}
    
}
