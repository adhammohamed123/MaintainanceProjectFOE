using Core.Entities;

namespace Core.RepositoryContracts
{
	public interface IFailureRepo
    {
        Task<IEnumerable<Failure>> GetAllFailures(bool trackchanges);
		Task<Failure?> GetById(int id, bool trackchanges);
		Task CreateFailure(Failure failure);
		void DeleteFailure(Failure failure);
		bool CheckExistance(string name);
    }

}
