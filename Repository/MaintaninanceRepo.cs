using Core.RepositoryContracts;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Core.Features;
using Repository.Extensions;

namespace Repository
{
	public class MaintaninanceRepo : BaseRepository<DeviceFailureHistory>, IMaintaninanceRepo
	{
		public MaintaninanceRepo(FoeMaintainContext context) : base(context)
		{
		}

		public void DeleteMaintainance(DeviceFailureHistory deviceFailureHistory,string userId)
		{
			deviceFailureHistory.LastModifiedUserId = userId;
            SoftDelete(deviceFailureHistory);
		}
        public PagedList<DeviceFailureHistory> GetDeviceFailureHistories(MaintainanceRequestParameters maintainanceRequestParameters, bool trackchanges)
		{
			
            //-->		
            var items =FindAll(trackchanges)
                .Include(h=>h.Maintainer)
				.Include(h=>h.Device)
				.Include(h=>h.Receiver)
				.Include(h => h.FailureMaintains)
				.ThenInclude(f => f.Failure)
                .Search(maintainanceRequestParameters.SearchTerm)
				.Sort(maintainanceRequestParameters.OrderBy)
                ;
			return PagedList<DeviceFailureHistory>.ToPagedList(items, maintainanceRequestParameters.PageNumber, maintainanceRequestParameters.PageSize);
		}
		public IQueryable<DeviceFailureHistory> GetDeviceFailureHistoriesByDeviceId(int deviceId, bool trackchanges)
		=> FindByCondition(x => x.DeviceId == deviceId, trackchanges)
            .Include(h => h.Maintainer).Include(h => h.Receiver)
            .Include(h => h.FailureMaintains).ThenInclude(f=>f.Failure)
			.OrderByDescending(d=>d.LastModifiedDate ?? d.CreatedDate);

		public DeviceFailureHistory? GetDeviceFailureHistoryById(int id, bool trackchanges)
		=> FindByCondition(x => x.Id == id, trackchanges).Include(h => h.FailureMaintains).ThenInclude(f=>f.Failure).SingleOrDefault();

		public async Task RegisterNew(DeviceFailureHistory deviceFailureHistory)
		=>await Create(deviceFailureHistory);
		
	}

}
