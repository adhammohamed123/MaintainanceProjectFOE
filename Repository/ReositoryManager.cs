using   Core.RepositoryContracts;
using System.Diagnostics;

namespace Repository
{


    public class RepositoryManager:IRepositoryManager
    {
        private readonly FoeMaintainContext context;


        private Lazy<IDeviceRepo> _DeviceRepo;
        private Lazy<IDepartmentRepo> _DepartmentRepo;
        private Lazy<IMaintaninanceRepo> _MaintaninanceRepo;
        private Lazy<IOfficeRepo> _OfficeRepo;
        private Lazy<IFailureRepo> _FailureRepo;
        private Lazy<IGateRepo> _GateRepo;
        private Lazy<IRegionRepo> _RegionRepo;
        private Lazy<IUserRepo> _UserRepo;
        private Lazy<ISpecializationRepo> _SpecializationRepo;
        private Lazy<IFailureMaintainRepo> _FailureMaintainRepo;
        public RepositoryManager(FoeMaintainContext context)
        {
            this.context = context;
            _DeviceRepo = new Lazy<IDeviceRepo>(() => new DeviceRepo(context));
            _DepartmentRepo = new Lazy<IDepartmentRepo>(() => new DepartmentRepo(context));
            _MaintaninanceRepo = new Lazy<IMaintaninanceRepo>(() => new MaintaninanceRepo(context));
            _OfficeRepo = new Lazy<IOfficeRepo>(() => new OfficeRepo(context));
            _FailureRepo = new Lazy<IFailureRepo>(() => new FailureRepo(context));
            _GateRepo = new Lazy<IGateRepo>(() => new GateRepo(context));
            _RegionRepo = new Lazy<IRegionRepo>(() => new RegionRepo(context));
            _UserRepo = new Lazy<IUserRepo>(() => new UserRepo(context));
            _SpecializationRepo = new Lazy<ISpecializationRepo>(() => new SpecializationRepo(context));
            _FailureMaintainRepo = new Lazy<IFailureMaintainRepo>(() => new FailureMaintainRepo(context));
        }

        public IDeviceRepo DeviceRepo => _DeviceRepo.Value;
        public IDepartmentRepo DepartmentRepo => _DepartmentRepo.Value;
        public IMaintaninanceRepo MaintaninanceRepo => _MaintaninanceRepo.Value;
        public IOfficeRepo OfficeRepo => _OfficeRepo.Value;
        public IFailureRepo FailureRepo => _FailureRepo.Value;
        public IGateRepo GateRepo => _GateRepo.Value;
        public IRegionRepo RegionRepo => _RegionRepo.Value;
        public IUserRepo UserRepo => _UserRepo.Value;
        public ISpecializationRepo SpecializationRepo => _SpecializationRepo.Value;

        public IFailureMaintainRepo FailureMaintainRepo =>_FailureMaintainRepo.Value;

        public async Task SaveAsync() => await context.SaveChangesAsync();
    }
}
