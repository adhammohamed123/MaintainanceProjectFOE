using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contracts.Base;
using Repository.Repository;
using Service.DTOs;
using Service.Services;
using Core.Exceptions;
using Core.Entities;

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
            var region = ChackParentExistance(regionId, trackchanges);

            var gate = new Gate() { Name = gateName, RegionId = region.Id };
            await repository.GateRepo.CreateNewGate(gate);
            await repository.SaveAsync();

            return mapper.Map<GateDto>(gate);

        }

        public IQueryable<GateDto> GetAllGates(int regionId, bool trackchanges)
        => repository.GateRepo.GetAllGates(regionId,trackchanges).ProjectTo<GateDto>(mapper.ConfigurationProvider);

        //public IQueryable<GateDto> GetAllGatesInGeneral(bool trackchanges)
        // => repository.GateRepo.GetAllGatesInGeneral(trackchanges)
        //    .ProjectTo<GateDto>(mapper.ConfigurationProvider);
        

        public GateDto GetSpecificGate(int regionId, int gateId, bool trackchanges)
        {

            var gate = CheckObjectExistanceAndParent(regionId, gateId, trackchanges);
            var gatedto = mapper.Map<GateDto>(gate);
            return gatedto;
        }

        private Gate CheckObjectExistanceAndParent(int regionId,int gateId, bool trackchanges)
        {
            var region = ChackParentExistance(regionId, trackchanges);

            var gate = repository.GateRepo.GetSpecificGate(regionId, gateId, trackchanges);

            if (gate == null)
                throw new GateNotFoundException(gateId);
            return gate;
        }
        private Region ChackParentExistance(int regionId,bool trackchanges)
        {
            var IsRegionExists = repository.RegionRepo.GetRegionBasedOnId(regionId, trackchanges);
            if (IsRegionExists == null)
                throw new RegionNotFoundException(regionId);
            return IsRegionExists;
        }

        public Task<GateDto> CreateNewGateInRegion(int regionId, string gateName)
        {
            throw new NotImplementedException();
        }
    }
}
