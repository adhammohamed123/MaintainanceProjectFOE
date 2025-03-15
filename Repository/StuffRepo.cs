using Repository.Repository;
using Core.Entities;

namespace Repository
{
    public class StuffRepo : BaseRepository<Stuff>, IStuffRepo
    {
        public StuffRepo(FoeMaintainContext context) : base(context)
        {
        }
    }
}
