using Core.RepositoryContracts;
using Core.Entities;

namespace Repository
{
    public class DepartmentRepo : BaseRepository<Department>, IDepartmentRepo
    {
        //private readonly FoeMaintainContext context;

        public DepartmentRepo(FoeMaintainContext context) : base(context)
        {
            //this.context = context;
        }

        public bool ChackExistance(string name, int GateId)
        => context.Departments.Any(d => d.Name.Equals(name)&&d.GateId.Equals(GateId));

        public async Task CreateNewDept(int gateId,Department dept)
        {
            dept.GateId = gateId;
            await  Create(dept);
        }

        public void DeleteDepartment(Department department)
        => SoftDelete(department);

		public IQueryable<Department> GetAll(int gateId, bool trackchanges)
        => FindByCondition(d => d.GateId.Equals(gateId) , trackchanges);

        public Department GetDeptBasedOnId( int gateId, int deptId, bool trackchanges)
        => FindByCondition(d => d.Id.Equals(deptId) && d.GateId.Equals(gateId), trackchanges).SingleOrDefault();
    }
}
