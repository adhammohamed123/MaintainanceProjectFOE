using Service.DTOs;
namespace Service.Services
{
    public interface IRegionService
    {
        IQueryable<RegionDto> GetAllRegisteredRegion(bool trackchanges);
        RegionDto GetRegionByID(int id, bool trackchanges);
        Task<RegionDto> CreateNewRegionAsync(string name);

    }
}
