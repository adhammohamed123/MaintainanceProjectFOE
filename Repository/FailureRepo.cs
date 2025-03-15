using Repository.Repository;
using Core.Entities;

namespace Repository
{
    public class FailureRepo : BaseRepository<Failure>, IFailureRepo
    {
        public FailureRepo(FoeMaintainContext context) : base(context)
        {
        }
    }
}
