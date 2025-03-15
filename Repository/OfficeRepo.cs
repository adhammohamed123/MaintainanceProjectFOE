using Repository.Repository;
using Core.Entities;

namespace Repository
{
    public class OfficeRepo : BaseRepository<Office>, IOfficeRepo
    {
        public OfficeRepo(FoeMaintainContext context) : base(context)
        {
        }

        public async Task CreateNewOffice(int deptId, Office office)
        {
            office.DepartmentId = deptId;
            await Create(office);
        }

        public IQueryable<Office> GetAll(int deptId, bool trackchanges)
        => FindByCondition(o => o.DepartmentId.Equals(deptId), trackchanges);

        public Office GetOfficeBasedOnId(int deptId, int officeId, bool trackchanges)
        => FindByCondition(o => o.DepartmentId.Equals(deptId) && o.Id.Equals(officeId), trackchanges).SingleOrDefault();
    }
}
