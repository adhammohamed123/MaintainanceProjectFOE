using Repository.Repository;
using Core.Entities;

namespace Repository
{
    public class SpecializationRepo : BaseRepository<Specialization>, ISpecializationRepo
    {
        public SpecializationRepo(FoeMaintainContext context) : base(context)
        {
        }
    }
}
