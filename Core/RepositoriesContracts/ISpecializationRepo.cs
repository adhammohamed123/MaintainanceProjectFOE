using Core.Entities;

namespace Core.RepositoryContracts
{
    public interface ISpecializationRepo
    {
        Task<Specialization?> GetSpecializationById(int id, bool trackchanges);
        Task<IEnumerable<Specialization>> GetAllSpecializations(bool trackchanges);
        Task CreateSpecialization(Specialization specialization);
        void DeleteSpecialization(Specialization specialization);
    }

}
