using Service.DTOs;
namespace Service.Services
{
    public interface IDepartmentService 
    {
       Task< IEnumerable<DepartmentDto>> GetAllDepartments(int regionId,int gateId, bool trakchanages);
        Task<DepartmentDto> GetDept(int regionId, int gateId,int deptId, bool trakchanages);
        Task<DepartmentDto> CreateNewDepartment(int regionId, int gateId, string deptName, bool trackchanges);
        Task DeleteDepartment(int regionId, int gateId, int deptId, bool trakchanages);
    }
}
