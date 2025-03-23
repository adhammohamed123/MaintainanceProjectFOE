using Core.Entities;
using Repository.Extensions.Utility;
using System.Linq.Dynamic.Core;
namespace Repository.Extensions
{
    public static class MaintainanceRepoExtensions
	{
		public static IQueryable<DeviceFailureHistory> Search(this IQueryable<DeviceFailureHistory> query, string? serachTerm)
		{
			if (!string.IsNullOrWhiteSpace(serachTerm))
			{
				return query.Where(x => x.Delievry.Contains(serachTerm));
			}
			return query;
		}
        public static IQueryable<DeviceFailureHistory> Sort(this IQueryable<DeviceFailureHistory> history, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return history.OrderByDescending(e => e.LastModifiedDate);
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Device>(orderByQueryString);
            if (string.IsNullOrWhiteSpace(orderQuery))
                return history.OrderByDescending(e => e.LastModifiedDate);
            return history.OrderBy(orderQuery);
        }
    }

}
