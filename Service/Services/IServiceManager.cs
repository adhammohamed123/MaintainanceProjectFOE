namespace Service.Services
{
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
