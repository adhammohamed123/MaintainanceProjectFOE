using Core.Entities;

namespace Core.RepositoryContracts
{
    public interface ISpecializationRepo
    {
        Specialization? GetSpecializationById(int id, bool trackchanges);
        IQueryable<Specialization> GetAllSpecializations(bool trackchanges);
        Task CreateSpecialization(Specialization specialization);
        void DeleteSpecialization(Specialization specialization);
    }

}
