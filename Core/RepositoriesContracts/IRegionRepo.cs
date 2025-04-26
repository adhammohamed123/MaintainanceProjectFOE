using Core.Entities;

namespace Core.RepositoryContracts
{
    public interface IRegionRepo
    {
       // IQueryable<Gate> GetAllGates(int regionId, bool trackchanges);
        IQueryable<Region> GetAllRegisteredRegion(bool trackchanges);
        bool ChackExistance(string name);
        Region GetRegionBasedOnId(int id, bool trackchanges);
        Task CreateNewRegionAsync(Region region);
        void DeleteRegion(Region region);
    }

}
