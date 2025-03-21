using Core.Entities;

namespace Core.RepositoryContracts
{
	public interface IFailureRepo
    {
        IQueryable<Failure> GetAllFailures(bool trackchanges);
		Failure? GetById(int id, bool trackchanges);
		Task CreateFailure(Failure failure);
		void DeleteFailure(Failure failure);
	}

}
