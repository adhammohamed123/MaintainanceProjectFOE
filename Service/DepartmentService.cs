using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Entities;
using Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Core.RepositoryContracts;
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
            await CheckParentExistance(regionId,gateId, trackchanges);
            if (repository.DepartmentRepo.ChackExistance(deptName.Trim(),gateId))
            {
                throw new DepartmentAlreadyRegistered(deptName);
            }
            var dept = new Department() { Name = deptName};
            await repository.DepartmentRepo.CreateNewDept( gateId, dept);
            await repository.SaveAsync();
            return mapper.Map<DepartmentDto>(dept);
        }

		public async Task DeleteDepartment(int regionId, int gateId, int deptId, bool trakchanages)
		{
			await CheckParentExistance(regionId, gateId, trakchanages);
           var dept= await  GetObjectAndCheckExistance(gateId, deptId,trakchanages);
            var ifDeptHasGates =  (await repository.GateRepo.GetAllGates(regionId, trakchanages)) .Count() > 0;
            if (ifDeptHasGates)
            {
                throw new CannotDeleteParentObjectThatHasChildrenException(dept.Name);
            }
            repository.DepartmentRepo.DeleteDepartment(dept);
            await repository.SaveAsync();
		}

		public async Task<IEnumerable<DepartmentDto>> GetAllDepartments(int regionId ,int gateId, bool trakchanages)
        {
            await CheckParentExistance( regionId,gateId, trakchanages);
            var depts=  await repository.DepartmentRepo.GetAll( gateId, trakchanages);
            return mapper.Map<IEnumerable<DepartmentDto>>(depts);
        }

        public async Task<DepartmentDto> GetDept(int regionId ,int gateId, int deptId, bool trakchanages)
        {
           await CheckParentExistance(regionId ,gateId, trakchanages);
            var dept=  await GetObjectAndCheckExistance(gateId, deptId, trakchanages);
            return mapper.Map<DepartmentDto>(dept);
        }


        private async Task<(Region region,Gate gate)> CheckParentExistance(int regionId,int gateId,bool trackchanges)
        {
            var region = await repository.RegionRepo.GetRegionBasedOnId(regionId, trackchanges);
            if(region==null)
                throw new RegionNotFoundException(regionId);
            var gate = await repository.GateRepo.GetSpecificGate(regionId, gateId, trackchanges);
            if (gate == null)
                throw new GateNotFoundException(gateId);
            return (region:region,gate:gate);
        }
        private async Task<Department> GetObjectAndCheckExistance(int gateId, int id, bool trackObject)
        {
            var deptExists = await repository.DepartmentRepo.GetDeptBasedOnId( gateId, id, trackObject);

            if (deptExists == null)
                throw new DepartmentNotFoundException(id);
            return deptExists;
        }
    }
}
