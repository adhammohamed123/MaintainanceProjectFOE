﻿using Contracts.Base;
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
        Device? GetById(int id, bool trackchanges);
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
          Task RegisterNew(DeviceFailureHistory deviceFailureHistory);
          
     	}




	public interface IUserRepo
    {
	    IQueryable<User> GetAllUser(bool trackchanges);
	    User? GetFromUserById(string id, bool trackchanges);
        Task CreateUser(User User);  
        void DeleteUser(User User);
        void associateUserWithSpecialization(User user, Specialization specialization);
    }
    public interface ISpecializationRepo
    {
        Specialization? GetSpecializationById(int id, bool trackchanges);
        IQueryable<Specialization> GetAllSpecializations(bool trackchanges);
        Task CreateSpecialization(Specialization specialization);
        void DeleteSpecialization(Specialization specialization);
    }

}
