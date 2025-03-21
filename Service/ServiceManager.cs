using AutoMapper;
using Contracts.Base;
using Microsoft.Extensions.Logging;
using Core.RepositoryContracts;
using Service.Services;
using System.Net.Http.Headers;


namespace Service
{

    public class ServiceManager:IServiceManager
    {

        private Lazy<IDeviceService> _DeviceService;
        private Lazy<IDepartmentService> _DepartmentService;
        private Lazy<IMaintaninanceService> _MaintaninanceService;
        private Lazy<IOfficeService> _OfficeService;
        private Lazy<IFailureService> _FailureService;
        private Lazy<IGateService> _GateService;
        private Lazy<IRegionService> _RegionService;
        private Lazy<IStuffService> _StuffService;
        private Lazy<ISpecializationService> _SpecializationService;

        public ServiceManager(IRepositoryManager repositoryManager,IMapper mapper,ILoggerManager logger)
        {
            _DepartmentService = new Lazy<IDepartmentService>(() => new DepartmentService(repositoryManager, mapper,logger));
            _DeviceService = new Lazy<IDeviceService>(() => new DeviceService(repositoryManager, mapper, logger));
            _MaintaninanceService = new Lazy<IMaintaninanceService>(() => new MaintaninanceService(repositoryManager, mapper,logger));
            _OfficeService = new Lazy<IOfficeService>(() => new OfficeService(repositoryManager, mapper,logger));
            _FailureService = new Lazy<IFailureService>(() => new FailureService(repositoryManager, mapper,logger));
            _GateService = new Lazy<IGateService>(() => new GateService(repositoryManager, mapper,logger));
            _RegionService = new Lazy<IRegionService>(() => new RegionService(repositoryManager, mapper,logger));
            _StuffService = new Lazy<IStuffService>(() => new StuffService(repositoryManager, mapper,logger));
            _SpecializationService = new Lazy<ISpecializationService>(() => new SpecializationService(repositoryManager, mapper,logger));
        }


        public IDeviceService DeviceService => _DeviceService.Value;
        public IDepartmentService DepartmentService => _DepartmentService.Value;
        public IMaintaninanceService MaintaninanceService => _MaintaninanceService.Value;
        public IOfficeService OfficeService => _OfficeService.Value;
        public IFailureService FailureService => _FailureService.Value;
        public IGateService GateService => _GateService.Value;
        public IRegionService RegionService => _RegionService.Value;
        public IStuffService StuffService => _StuffService.Value;
        public ISpecializationService SpecializationService => _SpecializationService.Value;
    }
}
