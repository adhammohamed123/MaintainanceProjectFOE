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

        public bool ChackExistance(string name)
        => context.Regions.Any(r => r.Name.Equals(name));
            
        

        public async Task CreateNewRegionAsync(Region region) => await Create(region);

		public void DeleteRegion(Region region)
		=>SoftDelete(region);

		//public IQueryable<Gate> GetAllGates(int regionId, bool trackchanges)
		//=> FindByCondition(r => r.Id.Equals(regionId), trackchanges).SelectMany(r => r.Gates);


		public async Task<IEnumerable<Region>> GetAllRegisteredRegion(bool trackchanges) => await FindAll(trackchanges).OrderBy(e=>e.Name).ToListAsync();

        public async Task<Region> GetRegionBasedOnId(int id, bool trackchanges)
        => await FindByCondition(r => r.Id.Equals(id), trackchanges).SingleOrDefaultAsync();    
            
            
    }
}
