using Core.RepositoryContracts;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Repository
{
    public class RegionRepo : BaseRepository<Region>, IRegionRepo
    {
        public RegionRepo(FoeMaintainContext context) : base(context)
        {
        }

        public async Task CreateNewRegionAsync(Region region) => await Create(region);

		public void DeleteRegion(Region region)
		=>SoftDelete(region);

		//public IQueryable<Gate> GetAllGates(int regionId, bool trackchanges)
		//=> FindByCondition(r => r.Id.Equals(regionId), trackchanges).SelectMany(r => r.Gates);


		public IQueryable<Region> GetAllRegisteredRegion(bool trackchanges) => FindAll(trackchanges).OrderBy(e=>e.Name);

        public Region GetRegionBasedOnId(int id, bool trackchanges)
        => FindByCondition(r => r.Id.Equals(id), trackchanges).SingleOrDefault();    
            
            
    }
}
