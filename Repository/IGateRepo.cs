using Core.Entities;

namespace Repository.Repository
{
    public interface IGateRepo
    {
        IQueryable<Gate> GetAllGates(int regionId, bool trackchanges);
        Gate GetSpecificGate(int regionId, int gateId, bool trackchanges);
        Task CreateNewGate(Gate gate);
    }

}
