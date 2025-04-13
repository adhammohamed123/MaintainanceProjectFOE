using Core.Entities;
using Core.RepositoryContracts;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Repository
{
    public class FailureMaintainRepo:BaseRepository<FailureMaintain>, IFailureMaintainRepo
    {
        public FailureMaintainRepo(FoeMaintainContext context):base(context)
        {
        }
        public async Task CreateFailureMaintain(FailureMaintain failureMaintain)
        => await Create(failureMaintain);

        public void DeleteFailureMaintain(FailureMaintain failureMaintain)
        =>SoftDelete(failureMaintain);
        public FailureMaintain? GetFailureMaintain(int maintainId, int failureId, bool trackchanges)
        => FindByCondition(x => x.DeviceFailureHistoryId == maintainId && x.FailureId == failureId, trackchanges).FirstOrDefault();


        // public IQueryable<FailureMaintain> GetAllFailureMaintains(int maitainId, bool trackchanges)
        // => FindByCondition(x => x.DeviceFailureHistoryId == maitainId, trackchanges);
    }
}