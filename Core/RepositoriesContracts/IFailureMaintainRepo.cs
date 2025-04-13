using Core.Entities;

namespace Core.RepositoryContracts
{
    public interface IFailureMaintainRepo
    {
        Task CreateFailureMaintain(FailureMaintain failureMaintain);
        void DeleteFailureMaintain(FailureMaintain failureMaintain);
        FailureMaintain? GetFailureMaintain(int maintainId,int failureId, bool trackchanges);



    }

}
