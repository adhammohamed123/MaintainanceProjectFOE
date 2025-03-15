using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Entities;
using Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Repository.Repository;
using Service.DTOs;
using Service.Services;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Service
{
    public class DepartmentService: IDepartmentService
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;

        public DepartmentService(IRepositoryManager repository,IMapper mapper, Contracts.Base.ILoggerManager logger)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<DepartmentDto> CreateNewDepartment(int regionId, int gateId, string deptName,bool trackchanges)
        {
            CheckParentExistance(regionId,gateId, trackchanges);
            var dept = new Department() { Name = deptName};
            await repository.DepartmentRepo.CreateNewDept( gateId, dept);
            await repository.SaveAsync();
            return mapper.Map<DepartmentDto>(dept);
        }

        public IEnumerable<DepartmentDto> GetAllDepartments(int regionId ,int gateId, bool trakchanages)
        {
            CheckParentExistance( regionId,gateId, trakchanages);
            var depts=  repository.DepartmentRepo.GetAll( gateId, trakchanages);
            return mapper.Map<IEnumerable<DepartmentDto>>(depts);
        }

        public DepartmentDto GetDept(int regionId ,int gateId, int deptId, bool trakchanages)
        {
            CheckParentExistance(regionId ,gateId, trakchanages);
            var dept=  GetObjectAndCheckExistance(regionId,gateId, deptId, trakchanages);
            return mapper.Map<DepartmentDto>(dept);
        }


        private (Region region,Gate gate) CheckParentExistance(int regionId,int gateId,bool trackchanges)
        {
            var region = repository.RegionRepo.GetRegionBasedOnId(regionId, trackchanges);
            if(region==null)
                throw new RegionNotFoundException(regionId);
            var gate = repository.GateRepo.GetSpecificGate(regionId, gateId, trackchanges);
            if (gate == null)
                throw new GateNotFoundException(gateId);
            return (region:region,gate:gate);
        }
        private Department GetObjectAndCheckExistance(int regionId ,int gateId, int id, bool trackObject)
        {
            var deptExists = repository.DepartmentRepo.GetDeptBasedOnId( gateId, id, trackObject);

            if (deptExists == null)
                throw new DepartmentNotFoundException(id);
            return deptExists;
        }
    }
}
