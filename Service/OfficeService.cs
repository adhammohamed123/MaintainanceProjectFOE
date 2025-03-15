using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contracts.Base;
using Core.Entities;
using Core.Exceptions;
using Repository.Repository;
using Service.DTOs;
using Service.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public async Task<OfficeDto> CreateNewOffice(int regionId, int gateId, int deptId, string officeName,bool trackchanges)
        {
            CheckParentExistance(regionId, gateId, deptId, trackchanges);
            var office = new Office() { Name = officeName };
            await repository.OfficeRepo.CreateNewOffice(deptId, office);
            await repository.SaveAsync();
            return mapper.Map<OfficeDto>(office);
        }

        public IEnumerable<OfficeDto> GetAll(int regionId, int gateId, int deptId, bool trackchanges)
        {
            CheckParentExistance(regionId, gateId, deptId, trackchanges);
            var result = repository.OfficeRepo.GetAll(deptId, trackchanges).ToList();
            return mapper.Map<IEnumerable<OfficeDto>>(result);
        }

        public OfficeDto GetOfficeBasedOnId(int regionId, int gateId, int deptId, int officeId, bool trackchanges)
        {
            CheckParentExistance(regionId, gateId, deptId, trackchanges);
            var office = repository.OfficeRepo.GetOfficeBasedOnId(deptId, officeId, trackchanges);
            if (office == null)
                throw new OfficeNotFoundException(officeId);

            return mapper.Map<OfficeDto>(office);
        }

        private (Region region,Gate gate,Department dept) CheckParentExistance( int regionId,int gateId,int deptId,bool trackchanges)
        {
            var region = repository.RegionRepo.GetRegionBasedOnId(regionId,trackchanges);
            if (region == null)
                throw new RegionNotFoundException(regionId);
          
            var gate = repository.GateRepo.GetSpecificGate(regionId,gateId, trackchanges);
            if (gate == null)
                throw new GateNotFoundException(gateId);

            var dept = repository.DepartmentRepo.GetDeptBasedOnId(gateId, deptId, trackchanges);
          
            if (dept==null)
             throw new DepartmentNotFoundException(deptId);
            
            return (region,gate,dept);
        }
    }
}
