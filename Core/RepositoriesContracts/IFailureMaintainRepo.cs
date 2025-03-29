using Core.Entities;

namespace Core.RepositoryContracts
{
    public interface IFailureMaintainRepo
    {
        Task CreateFailureMaintain(FailureMaintain failureMaintain);
        void DeleteFailureMaintain(FailureMaintain failureMaintain);
        //IQueryable<FailureMaintain> GetAllFailureMaintains(int maitainId, bool trackchanges);



    }

}
