using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contracts.Base;
using Core.Entities;
using Core.Exceptions;
using Core.RepositoryContracts;
using Service.DTOs;
using Service.Services;
using System.Threading.Tasks;


namespace Service
{
    public class OfficeService : IOfficeService
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;
        private readonly ILoggerManager logger;

        public OfficeService(IRepositoryManager repository, IMapper mapper, Contracts.Base.ILoggerManager logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<OfficeDto> CreateNewOffice(int regionId, int gateId, int deptId, string officeName, bool trackchanges)
        {
             await CheckParentExistance(regionId, gateId, deptId, trackchanges);
            if (repository.OfficeRepo.CheckExistance(officeName.Trim(),deptId))
                throw new OfficeAlreadyRegistered(officeName);
            var office = new Office() { Name = officeName };
            await repository.OfficeRepo.CreateNewOffice(deptId, office);
            await repository.SaveAsync();
            return mapper.Map<OfficeDto>(office);
        }

        public async Task DeleteOffice(int regionId, int gateId, int deptId, int officeId, bool trackchanges)
        {
            await CheckParentExistance(regionId, gateId, deptId, trackchanges);
            var office=  await GetObjectAndCheckExistance(deptId, officeId, trackchanges);
          var ifOfficeHasDevices =(await repository.DeviceRepo.GetAllRegisteredDevicesInSpecificOffice(officeId, trackchanges: false)).Count() > 0;
            if (ifOfficeHasDevices)
                throw new CannotDeleteParentObjectThatHasChildrenException(office.Name);
            repository.OfficeRepo.DeleteOffice(office);
		   await repository.SaveAsync();
		}

        public async Task<IEnumerable<OfficeDto>> GetAll(int regionId, int gateId, int deptId, bool trackchanges)
        {
            await CheckParentExistance(regionId, gateId, deptId, trackchanges);
            var result = await repository.OfficeRepo.GetAll(deptId, trackchanges);
            return mapper.Map<IEnumerable<OfficeDto>>(result);
        }

        public async Task<OfficeDto> GetOfficeBasedOnId(int regionId, int gateId, int deptId, int officeId, bool trackchanges)
        {
             await CheckParentExistance(regionId, gateId, deptId, trackchanges);
            var office = await repository.OfficeRepo.GetOfficeBasedOnId(deptId, officeId, trackchanges);
            if (office == null)
                throw new OfficeNotFoundException(officeId);

            return mapper.Map<OfficeDto>(office);
        }

        private async Task<(Region region, Gate gate, Department dept)> CheckParentExistance(int regionId, int gateId, int deptId, bool trackchanges)
        {
            var region =await repository.RegionRepo.GetRegionBasedOnId(regionId, trackchanges);
            if (region == null)
                throw new RegionNotFoundException(regionId);

            var gate =await repository.GateRepo.GetSpecificGate(regionId, gateId, trackchanges);
            if (gate == null)
                throw new GateNotFoundException(gateId);

            var dept = await repository.DepartmentRepo.GetDeptBasedOnId(gateId, deptId, trackchanges);

            if (dept == null)
                throw new DepartmentNotFoundException(deptId);

            return (region, gate, dept);
        }
        private async Task<Office> GetObjectAndCheckExistance(int deptId, int officeId, bool trackchanges)
        {
            var office = await repository.OfficeRepo.GetOfficeBasedOnId(deptId, officeId, trackchanges);
            if (office == null)
                throw new OfficeNotFoundException(officeId);
            return office;
        }
    } 
}
