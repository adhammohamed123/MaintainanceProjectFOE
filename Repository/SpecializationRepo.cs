using Core.RepositoryContracts;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<Specialization>> GetAllSpecializations(bool trackchanges)
        => await FindAll(trackchanges).ToListAsync();

        public async Task<Specialization?> GetSpecializationById(int id, bool trackchanges)
        => await FindByCondition(x => x.Id == id, trackchanges).SingleOrDefaultAsync();
    }
}
