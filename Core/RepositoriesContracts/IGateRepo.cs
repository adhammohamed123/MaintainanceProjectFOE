using Core.Entities;

namespace Core.RepositoryContracts
{
    public interface IGateRepo
    {
        Task<IEnumerable<Gate>> GetAllGates(int regionId, bool trackchanges);
       Task<Gate> GetSpecificGate(int regionId, int gateId, bool trackchanges);
        Task CreateNewGate(Gate gate);
        bool ChackExistance(string name,int RegionId);
        void DeleteGate(Gate gate);
	}

}
