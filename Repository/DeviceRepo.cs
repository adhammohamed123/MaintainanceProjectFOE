using Repository.Repository;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class DeviceRepo : BaseRepository<Device>, IDeviceRepo
    {
        public DeviceRepo(FoeMaintainContext context) : base(context)
        {
        }

        public IQueryable<Device> GetAllRegisteredDevices(bool trackchanges) =>  FindAll(trackchanges).OrderByDescending(d=> d.CreatedDate);

        public Device? GetById(int id, bool trackchanges) => FindByCondition(d => d.Id.Equals(id), trackchanges).SingleOrDefault();

    }
}
