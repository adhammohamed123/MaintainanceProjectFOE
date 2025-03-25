using Core.Entities;
using Core.RepositoryContracts;
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
    }
}