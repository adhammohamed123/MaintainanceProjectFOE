using Contracts.Base;
using Core.Entities;
using System.Linq.Expressions;

namespace Repository.Repository
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

    public interface IDepartmentRepo
    {
        IQueryable<Department> GetAll(int gateId, bool trackchanges);
        Department GetDeptBasedOnId(int gateId,int deptId, bool trackchanges);

        Task CreateNewDept(int gateId, Department dept);
      
    }
    public interface IOfficeRepo
    {
        IQueryable<Office> GetAll(int deptId, bool trackchanges);
        Office GetOfficeBasedOnId(int deptId, int officeId, bool trackchanges);

        Task CreateNewOffice(int deptId, Office office);
    }
    public interface IDeviceRepo
    {
        IQueryable<Device> GetAllRegisteredDevices(bool trackchanges);
        Device? GetById(int id,bool trackchanges);
    }
  
    
    public interface IMaintaninanceRepo
    {

    }
    public interface IFailureRepo
    {

    }
  
    public interface IStuffRepo
    {

    }
    public interface ISpecializationRepo
    {

    }


    public interface IRepositoryManager
    {
        public IDeviceRepo DeviceRepo { get; }
        public IDepartmentRepo DepartmentRepo { get; }
        public IMaintaninanceRepo MaintaninanceRepo { get; }
        public IOfficeRepo OfficeRepo { get;}
        public IFailureRepo FailureRepo { get;}
        public IGateRepo  GateRepo { get;  }
        public IRegionRepo RegionRepo { get; }
        public IStuffRepo StuffRepo { get;}
        public ISpecializationRepo SpecializationRepo { get;}

        public Task SaveAsync();
    }

}
