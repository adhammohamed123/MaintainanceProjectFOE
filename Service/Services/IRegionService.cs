using Core.Entities;
using Service.DTOs;
namespace Service.Services
{
    public interface IRegionService
    {
        IQueryable<RegionDto> GetAllRegisteredRegion(bool trackchanges);
        RegionDto GetRegionByID(int id, bool trackchanges);
        Task<RegionDto> CreateNewRegionAsync(string name);
        Task DeleteRegionAsync(int id);
        Task UpdateRegion(int regionId,RegionDto regionDto);
       (Region region,RegionDto regionDto) GetRegionForPartialUpdate(int regionId,bool trackchanges);
        Task SavePatchChanges(Region region,RegionDto regionDto);
	}
}
