using Service.DTOs;
namespace Service.Services
{
	public interface IFailureService
    {
		Task<IEnumerable<NameWithIdentifierDto>> GetAllFailures(bool trackchanges);
		Task<NameWithIdentifierDto> GetById(int id, bool trackchanges);
		Task<NameWithIdentifierDto> CreateFailure(string failureName);
		Task DeleteFailure(int id);
	}
}
