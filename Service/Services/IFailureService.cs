using Service.DTOs;
namespace Service.Services
{
	public interface IFailureService
    {
		IQueryable<NameWithIdentifierDto> GetAllFailures(bool trackchanges);
		NameWithIdentifierDto GetById(int id, bool trackchanges);
		Task<NameWithIdentifierDto> CreateFailure(string failureName);
		Task DeleteFailure(int id);
	}
}
