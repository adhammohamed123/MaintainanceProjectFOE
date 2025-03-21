using Core.RepositoryContracts;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Core.Features;

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
				.filter(deviceRequestParameters.RegionName, deviceRequestParameters.GateName, deviceRequestParameters.DeptName, deviceRequestParameters.OfficeName)
				.Search(deviceRequestParameters.SearchTerm)
				.ToList();
			return PagedList<Device>.ToPagedList(device, deviceRequestParameters.PageNumber, deviceRequestParameters.PageSize);
		}
		public IQueryable<Device> GetAllRegisteredDevicesInSpecificOffice(int officeId, bool trackchanges)
		=>FindByCondition(d=>d.OfficeId.Equals(officeId), trackchanges);

		public Device? GetById(int officeId, int id, bool trackchanges)
		=> FindByCondition(d => d.OfficeId.Equals(officeId) && d.Id.Equals(id), trackchanges).SingleOrDefault();
	}

	public static class DeviceRepoExtensions
	{
		public static IQueryable<Device> Search(this IQueryable<Device> devices, string? searchTerm)
		{
			if (!string.IsNullOrWhiteSpace(searchTerm))
			{
					return	devices.Where(d =>
					d.MAC.Contains(searchTerm) ||
					d.Owner.Contains(searchTerm) ||
					d.DomainIDIfExists.Contains(searchTerm)
					);
			}
			return devices;

		}
		public static IQueryable<Device> filter(this IQueryable<Device> devices, string? regionName,string?gateName, string?deptName,string? officeName)
		{
			if (regionName != null)
			{
				devices = devices.Where(d => d.Office.Department.Gate.Region.Name.ToLower().Equals(regionName.Trim().ToLower()));
			}
			if (gateName != null)
			{
				devices = devices.Where(d => d.Office.Department.Gate.Name.ToLower().Equals(gateName.Trim().ToLower()));
			}
			if (deptName != null)
			{
				devices = devices.Where(d => d.Office.Department.Name.ToLower().Equals(deptName.Trim().ToLower()));
			}
			if (officeName != null)
			{
				devices = devices.Where(d => d.Office.Name.ToLower().Equals(officeName.Trim().ToLower()));
			}
			return devices;
		}

	}
}
