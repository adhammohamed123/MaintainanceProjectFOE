using Core.RepositoryContracts;
using Core.Entities;
using System.Threading.Tasks;

namespace Repository
{
    public class StuffRepo : BaseRepository<Stuff>, IStuffRepo
    {
        public StuffRepo(FoeMaintainContext context) : base(context)
        {
        }

		public async Task CreateStuff(Stuff stuff)
		=> await Create(stuff);

		public void DeleteStuff(Stuff stuff)
		=>SoftDelete(stuff);

		public IQueryable<Stuff> GetAllStuff(bool trackchanges)
		=>FindAll(trackchanges);

		public Stuff? GetFromStuffById(int id, bool trackchanges)
		=> FindByCondition(s => s.Id == id, trackchanges).SingleOrDefault();
	}
}
