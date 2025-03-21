using Contracts.Base;
using Core.Entities;
using Core.Features;
using System.Linq.Expressions;

namespace Core.RepositoryContracts
{
    public interface IGenericRepository<T> where T :class,ISoftDeletedModel
    {
         IQueryable<T>  FindAll(bool trackchanges);
         IQueryable<T> FindByCondition(Expression<Func<T,bool>> expression ,bool trackchanges);

        Task Create(T entity);
        void Update(T entity);
        void SoftDelete(T entity);
        void HardDelete(T entity);

    }
    public interface IDeviceRepo
    {
        PagedList<Device> GetAllDevices(DeviceRequestParameters deviceRequestParameters,bool trackchanges);
        IQueryable<Device> GetAllRegisteredDevicesInSpecificOffice(int officeId,bool trackchanges);
        Device? GetById(int officeId,int id,bool trackchanges);
        Task CreateDevice(int officeId,Device device,string UserId);
        void DeleteDevice(Device device,string UserId);
	}
  
    
   
		public interface IMaintaninanceRepo
		{
		  PagedList<DeviceFailureHistory> GetDeviceFailureHistories(MaintainanceRequestParameters maintainanceRequestParameters,bool trackchanges);
          IQueryable<DeviceFailureHistory> GetDeviceFailureHistoriesByDeviceId(int deviceId, bool trackchanges);
          DeviceFailureHistory? GetDeviceFailureHistoryById(int id, bool trackchanges);
          Task RegisterNew(DeviceFailureHistory deviceFailureHistory,List<int> failureIds);
          
     	}




	public interface IStuffRepo
    {
	    IQueryable<Stuff> GetAllStuff(bool trackchanges);
	    Stuff? GetFromStuffById(int id, bool trackchanges);
        Task CreateStuff(Stuff stuff);  
        void DeleteStuff(Stuff stuff);
	}
    public interface ISpecializationRepo
    {

    }

}
