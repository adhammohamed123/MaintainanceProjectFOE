using Core.Entities;

namespace Repository.Repository
{
    public interface IRegionRepo
    {
       // IQueryable<Gate> GetAllGates(int regionId, bool trackchanges);
        IQueryable<Region> GetAllRegisteredRegion(bool trackchanges);
        Region GetRegionBasedOnId(int id, bool trackchanges);

        Task CreateNewRegionAsync(Region region);

    }

}
