using Service.DTOs;
namespace Service.Services
{
    public interface ISpecializationService
    {
        Task<IEnumerable<NameWithIdentifierDto>> GetAllSpecializations(bool trackchanges);
        Task<NameWithIdentifierDto?> GetSpecializationById(int id, bool trackchanges);
        Task<NameWithIdentifierDto> CreateSpecialization(string specializationName);
        Task DeleteSpecialization(int id);
    }
}
