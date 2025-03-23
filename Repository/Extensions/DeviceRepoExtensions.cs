using Core.Entities;
using Repository.Extensions.Utility;
using System.Linq.Dynamic.Core;
namespace Repository.Extensions
{
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
		public static IQueryable<Device> filter(this IQueryable<Device> devices, int? regionId,int?gateId, int?deptId,int? officeId)
		{
			if (regionId != null)
			{
				devices = devices.Where(d => d.Office.Department.Gate.RegionId.Equals(regionId));
			}
			if (gateId != null)
			{
				devices = devices.Where(d => d.Office.Department.GateId.Equals(gateId));
			}
			if (deptId != null)
			{
				devices = devices.Where(d => d.Office.DepartmentId.Equals(deptId));
			}
			if (officeId != null)
			{
				devices = devices.Where(d => d.OfficeId.Equals(officeId));
			}
			return devices;
		}
        public static IQueryable<Device> Sort(this IQueryable<Device> devices, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return devices.OrderByDescending(e => e.LastModifiedDate);
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Device>(orderByQueryString);
            if (string.IsNullOrWhiteSpace(orderQuery))
                return devices.OrderByDescending(e => e.LastModifiedDate);
            return devices.OrderBy(orderQuery);
        }
    }
}
