using Core.Entities;
using Service.DTOs;
namespace Service.Services
{


    public interface IOfficeService
    {
        IEnumerable<OfficeDto> GetAll(int regionId,int gateId, int deptId, bool trackchanges);
        OfficeDto GetOfficeBasedOnId(int regionId, int gateId, int deptId, int officeId, bool trackchanges);

        Task<OfficeDto> CreateNewOffice(int regionId, int gateId, int deptId,string officeName ,bool trackchanges);

    }
    public interface IDeviceService
    {
        IQueryable<DeviceDto> GetAllDevices(bool trackchanges);
        DeviceDto GetDeviceByID(int id,bool trackchanges);
    }

   
    public interface IMaintaninanceService
    {

    }
    public interface IFailureService
    {

    }
  
    
    
    public interface IStuffService
    {

    }
    public interface ISpecializationService
    {

    }


    public interface IServiceManager
    {
        public IDeviceService DeviceService { get; }
        public IDepartmentService DepartmentService { get; }
        public IMaintaninanceService MaintaninanceService { get; }
        public IOfficeService OfficeService { get; }
        public IFailureService FailureService { get; }
        public IGateService GateService { get; }
        public IRegionService RegionService { get; }
        public IStuffService StuffService { get; }
        public ISpecializationService SpecializationService { get; }
    }
}
