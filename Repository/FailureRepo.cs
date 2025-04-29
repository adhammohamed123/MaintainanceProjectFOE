using Core.RepositoryContracts;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class FailureRepo : BaseRepository<Failure>, IFailureRepo
    {
        public FailureRepo(FoeMaintainContext context) : base(context)
        {
        }

        public bool CheckExistance(string name)
        =>context.Failures.Any(f => f.Name.Equals(name));

        public Task CreateFailure(Failure failure)=> Create(failure);

		public void DeleteFailure(Failure failure) => SoftDelete(failure) ; 

		public async Task<IEnumerable<Failure>> GetAllFailures(bool trackchanges)	=> await FindAll(trackchanges).ToListAsync();

		public async Task<Failure?> GetById(int id, bool trackchanges) => await FindByCondition(f => f.Id.Equals(id), trackchanges).SingleOrDefaultAsync();
	}
}
