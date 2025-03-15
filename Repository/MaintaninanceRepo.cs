using Repository.Repository;
using Core.Entities;

namespace Repository
{
    public class MaintaninanceRepo : BaseRepository<DeviceFailureHistory>, IMaintaninanceRepo
    {
        public MaintaninanceRepo(FoeMaintainContext context) : base(context)
        {
        }
    }
}
