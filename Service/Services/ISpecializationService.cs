using Service.DTOs;
namespace Service.Services
{
    public interface ISpecializationService
    {
        IQueryable<NameWithIdentifierDto> GetAllSpecializations(bool trackchanges);
        NameWithIdentifierDto? GetSpecializationById(int id, bool trackchanges);
        Task<NameWithIdentifierDto> CreateSpecialization(string specializationName);
        Task DeleteSpecialization(int id);
    }
}
