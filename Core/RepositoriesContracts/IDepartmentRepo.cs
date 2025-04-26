using Core.Entities;

namespace Core.RepositoryContracts
{
	public interface IDepartmentRepo
    {
        IQueryable<Department> GetAll(int gateId, bool trackchanges);
        Department GetDeptBasedOnId(int gateId,int deptId, bool trackchanges);
        Task CreateNewDept(int gateId, Department dept);
       void DeleteDepartment(Department department);
        bool ChackExistance(string name);
    }

}
