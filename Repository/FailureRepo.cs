using Core.RepositoryContracts;
using Core.Entities;

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

		public IQueryable<Failure> GetAllFailures(bool trackchanges)	=> FindAll(trackchanges);

		public Failure GetById(int id, bool trackchanges)	=> FindByCondition(f => f.Id .Equals( id), trackchanges).SingleOrDefault();
	}
}
