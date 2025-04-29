using Core.RepositoryContracts;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

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

		public async Task<IEnumerable<Office>> GetAll(int deptId, bool trackchanges)
        =>  await FindByCondition(o => o.DepartmentId.Equals(deptId), trackchanges).ToListAsync();

        public async Task<Office> GetOfficeBasedOnId(int deptId, int officeId, bool trackchanges)
        => await FindByCondition(o => o.DepartmentId.Equals(deptId) && o.Id.Equals(officeId), trackchanges).SingleOrDefaultAsync();
    }
}
