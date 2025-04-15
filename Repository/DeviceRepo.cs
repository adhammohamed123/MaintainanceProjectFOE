using Core.RepositoryContracts;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Core.Features;
using Repository.Extensions;

namespace Repository
{
    public class DeviceRepo : BaseRepository<Device>, IDeviceRepo
    {
        public DeviceRepo(FoeMaintainContext context) : base(context)
        {
        }

		public async Task CreateDevice(int officeId, Device device, string UserId)
		{
			device.OfficeId = officeId;
			device.CreatedByUserId = UserId;
			await Create(device);
		}

		public void DeleteDevice(Device device, string UserId)
		{
			device.LastModifiedUserId = UserId;
			SoftDelete(device);
		}

		public PagedList<Device> GetAllDevices(DeviceRequestParameters deviceRequestParameters, bool trackchanges)
		{
			
		    var device=FindAll(trackchanges)
				.Include(d => d.Office)
				.ThenInclude(o=>o.Department)
				.ThenInclude(d => d.Gate)
				.ThenInclude(g => g.Region)
				.Search(deviceRequestParameters.SearchTerm,deviceRequestParameters.SearchOptions)
                .filter(deviceRequestParameters.RegionId, deviceRequestParameters.GateId, deviceRequestParameters.DeptId, deviceRequestParameters.OfficeId)
				.Sort(deviceRequestParameters.OrderBy)
                .ToList();
			return PagedList<Device>.ToPagedList(device, deviceRequestParameters.PageNumber, deviceRequestParameters.PageSize);
		}
		public IQueryable<Device> GetAllRegisteredDevicesInSpecificOffice(int officeId, bool trackchanges)
		=>FindByCondition(d=>d.OfficeId.Equals(officeId), trackchanges);

		public Device? GetById(int officeId, int id, bool trackchanges)
		=> FindByCondition(d => d.OfficeId.Equals(officeId) && d.Id.Equals(id), trackchanges)
			.Include(c => c.Office)
			.Include(c => c.Office.Department)
			.Include(c => c.Office.Department.Gate)
			.Include(c => c.Office.Department.Gate.Region)
			.SingleOrDefault();

        public Device? GetById(int id, bool trackchanges)
        =>FindByCondition(d=>d.Id.Equals(id), trackchanges).SingleOrDefault();
    }
}
