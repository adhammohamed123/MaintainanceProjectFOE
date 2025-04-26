using Core.Entities;

namespace Core.RepositoryContracts
{
	public interface IOfficeRepo
    {
        IQueryable<Office> GetAll(int deptId, bool trackchanges);
        Office GetOfficeBasedOnId(int deptId, int officeId, bool trackchanges);
        Task CreateNewOffice(int deptId, Office office);
        void DeleteOffice(Office office);
        bool CheckExistance(string name, int departmentId);
    }

}
