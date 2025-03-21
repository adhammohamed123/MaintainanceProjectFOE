using Core.RepositoryContracts;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Core.Features;

namespace Repository
{
	public class MaintaninanceRepo : BaseRepository<DeviceFailureHistory>, IMaintaninanceRepo
	{
		public MaintaninanceRepo(FoeMaintainContext context) : base(context)
		{
		}

		public PagedList<DeviceFailureHistory> GetDeviceFailureHistories(MaintainanceRequestParameters maintainanceRequestParameters, bool trackchanges)
		{
			//-->		
			var items =FindAll(trackchanges).Include(h => h.Failures)
				.Search(maintainanceRequestParameters.SearchTerm)
				.OrderByDescending(x => x.LastModifiedDate?? x.CreatedDate)
				.ToList();
			return PagedList<DeviceFailureHistory>.ToPagedList(items, maintainanceRequestParameters.PageNumber, maintainanceRequestParameters.PageSize);
		}
		public IQueryable<DeviceFailureHistory> GetDeviceFailureHistoriesByDeviceId(int deviceId, bool trackchanges)
		=> FindByCondition(x => x.DeviceId == deviceId, trackchanges).Include(h => h.Failures)
			.OrderByDescending(d=>d.LastModifiedDate ?? d.CreatedDate);

		public DeviceFailureHistory? GetDeviceFailureHistoryById(int id, bool trackchanges)
		=> FindByCondition(x => x.Id == id, trackchanges).Include(h => h.Failures).SingleOrDefault();

		public async Task RegisterNew(DeviceFailureHistory deviceFailureHistory, List<int> failureIds)
		{
			var failures = await context.Failures.Where(f => failureIds.Contains(f.Id)).ToListAsync();
			deviceFailureHistory.Failures = failures;
			await Create(deviceFailureHistory);
		}
	}
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
	}

}
