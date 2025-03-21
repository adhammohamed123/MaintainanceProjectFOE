using Service.DTOs;
namespace Service.Services
{
    public interface IGateService
    {
        IQueryable<GateDto> GetAllGates(int regionId, bool trackchanges);
         GateDto GetSpecificGate(int regionId, int gateId, bool trackchanges);
         Task<GateDto> CreateNewGateInRegion(int regionId, string gateName, bool trackchanges);
        //IQueryable<GateDto> GetAllGatesInGeneral(bool trackchanges);
        Task DeleteGateAsync(int regionId, int gateId);
	}
}
