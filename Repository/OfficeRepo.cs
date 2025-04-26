using Core.RepositoryContracts;
using Core.Entities;

namespace Repository
{
    public class OfficeRepo : BaseRepository<Office>, IOfficeRepo
    {
        public OfficeRepo(FoeMaintainContext context) : base(context)
        {
        }

        public bool CheckExistance(string name, int departmentId)
        => context.Offices.Any(o => o.Name.Equals(name));

        public async Task CreateNewOffice(int deptId, Office office)
        {
            office.DepartmentId = deptId;
            await Create(office);
        }

		public void DeleteOffice(Office office)
		=>SoftDelete(office);

		public IQueryable<Office> GetAll(int deptId, bool trackchanges)
        => FindByCondition(o => o.DepartmentId.Equals(deptId), trackchanges);

        public Office GetOfficeBasedOnId(int deptId, int officeId, bool trackchanges)
        => FindByCondition(o => o.DepartmentId.Equals(deptId) && o.Id.Equals(officeId), trackchanges).SingleOrDefault();
    }
}
