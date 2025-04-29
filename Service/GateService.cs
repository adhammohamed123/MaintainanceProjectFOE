using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contracts.Base;
using Core.RepositoryContracts;
using Service.DTOs;
using Service.Services;
using Core.Exceptions;
using Core.Entities;
using System.Threading.Tasks;

namespace Service
{
    public class GateService:IGateService
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;
        private readonly ILoggerManager logger;

        public GateService(IRepositoryManager repository,IMapper mapper, Contracts.Base.ILoggerManager logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<GateDto> CreateNewGateInRegion(int regionId, string gateName,bool trackchanges)
        {
            var region = await ChackParentExistance(regionId, trackchanges);
            if (repository.GateRepo.ChackExistance(gateName.Trim(),regionId))
                throw new GateAlreadyRegistered(gateName);
            var gate = new Gate() { Name = gateName, RegionId = region.Id };
            await repository.GateRepo.CreateNewGate(gate);
            await repository.SaveAsync();

            return mapper.Map<GateDto>(gate);

        }

        public async Task<IEnumerable<GateDto>> GetAllGates(int regionId, bool trackchanges)
        {
              await  ChackParentExistance(regionId,  trackchanges);
            var allGates = await repository.GateRepo.GetAllGates(regionId, trackchanges);
             return mapper.Map<IEnumerable <GateDto>>(allGates);  
        }
        //public IQueryable<GateDto> GetAllGatesInGeneral(bool trackchanges)
        // => repository.GateRepo.GetAllGatesInGeneral(trackchanges)
        //    .ProjectTo<GateDto>(mapper.ConfigurationProvider);
        

        public async Task<GateDto> GetSpecificGate(int regionId, int gateId, bool trackchanges)
        {

            var gate = await CheckObjectExistanceAndParent(regionId, gateId, trackchanges);
            var gatedto = mapper.Map<GateDto>(gate);
            return gatedto;
        }

		public async Task DeleteGateAsync(int regionId, int gateId)
		{
			var gate = await CheckObjectExistanceAndParent(regionId, gateId, trackchanges: true);
            var ifGateHasDepartments = (await repository.DepartmentRepo.GetAll(gate.Id, trackchanges: false)) .Count() > 0;
            if (ifGateHasDepartments)
            {
                throw new CannotDeleteParentObjectThatHasChildrenException(gate.Name);
            }
            repository.GateRepo.DeleteGate(gate);
            await repository.SaveAsync();
		}



		private async Task<Gate> CheckObjectExistanceAndParent(int regionId,int gateId, bool trackchanges)
        {
            var region = await ChackParentExistance(regionId, trackchanges);

            var gate = await repository.GateRepo.GetSpecificGate(regionId, gateId, trackchanges);

            if (gate == null)
                throw new GateNotFoundException(gateId);
            return gate;
        }
        private async Task<Region> ChackParentExistance(int regionId,bool trackchanges)
        {
            var IsRegionExists = await repository.RegionRepo.GetRegionBasedOnId(regionId, trackchanges);
            if (IsRegionExists == null)
                throw new RegionNotFoundException(regionId);
            return IsRegionExists;
        }

	
	}
}
