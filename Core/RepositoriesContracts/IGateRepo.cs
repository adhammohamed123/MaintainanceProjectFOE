using Core.Entities;

namespace Core.RepositoryContracts
{
    public interface IGateRepo
    {
        IQueryable<Gate> GetAllGates(int regionId, bool trackchanges);
        Gate GetSpecificGate(int regionId, int gateId, bool trackchanges);
        Task CreateNewGate(Gate gate);
        void DeleteGate(Gate gate);
	}

}
