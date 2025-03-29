using Core.RepositoryContracts;
using Core.Entities;

namespace Repository
{
    public class SpecializationRepo : BaseRepository<Specialization>, ISpecializationRepo
    {
        public SpecializationRepo(FoeMaintainContext context) : base(context)
        {
        }

        public Task CreateSpecialization(Specialization specialization)
       => Create(specialization);

        public void DeleteSpecialization(Specialization specialization)
        => SoftDelete(specialization);

        public IQueryable<Specialization> GetAllSpecializations(bool trackchanges)
        => FindAll(trackchanges);

        public Specialization? GetSpecializationById(int id, bool trackchanges)
        => FindByCondition(x => x.Id == id, trackchanges).SingleOrDefault();
    }
}
