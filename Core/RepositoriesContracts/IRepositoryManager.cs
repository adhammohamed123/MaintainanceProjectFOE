namespace Core.RepositoryContracts
{
	public interface IRepositoryManager
    {
        public IDeviceRepo DeviceRepo { get; }
        public IDepartmentRepo DepartmentRepo { get; }
        public IMaintaninanceRepo MaintaninanceRepo { get; }
        public IOfficeRepo OfficeRepo { get;}
        public IFailureRepo FailureRepo { get;}
        public IGateRepo  GateRepo { get;  }
        public IRegionRepo RegionRepo { get; }
        public IUserRepo UserRepo { get;}
        public ISpecializationRepo SpecializationRepo { get;}

        public Task SaveAsync();
    }

}
