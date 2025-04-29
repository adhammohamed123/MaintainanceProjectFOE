using Core.Entities;

namespace Core.RepositoryContracts
{
    public interface IRegionRepo
    {
       // IQueryable<Gate> GetAllGates(int regionId, bool trackchanges);
        Task<IEnumerable<Region>> GetAllRegisteredRegion(bool trackchanges);
        bool ChackExistance(string name);
        Task<Region> GetRegionBasedOnId(int id, bool trackchanges);
        Task CreateNewRegionAsync(Region region);
        void DeleteRegion(Region region);
    }

}
