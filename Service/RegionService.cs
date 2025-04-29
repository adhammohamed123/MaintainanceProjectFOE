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
    public class RegionService:IRegionService
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;
        private readonly ILoggerManager logger;

        public RegionService(IRepositoryManager repository,IMapper mapper, Contracts.Base.ILoggerManager logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<RegionDto> CreateNewRegionAsync(string name)
        {
           if(repository.RegionRepo.ChackExistance(name.Trim()))
            {
                throw new RegionAlreadyRegistered(name);
            }
            var region = new Region() { Name = name };
            await repository.RegionRepo.CreateNewRegionAsync(region);
            await repository.SaveAsync();
            return mapper.Map<RegionDto>(region);
        }

		public async Task DeleteRegionAsync(int id)
		{
		   var region = await GetObjectAndCheckExistance(id, trackchanges: true);
            var ifRegionHasGates =(await repository.GateRepo.GetAllGates(region.Id, trackchanges: false)).Count()>0;
            if (ifRegionHasGates)
            {
                throw new CannotDeleteParentObjectThatHasChildrenException(region.Name);
            }
            repository.RegionRepo.DeleteRegion(region);
			await repository.SaveAsync();
		}

        public async Task<IEnumerable<RegionDto>> GetAllRegisteredRegion(bool trackchanges)
        {
            var result = await repository.RegionRepo.GetAllRegisteredRegion(trackchanges);
            return mapper.Map<IEnumerable<RegionDto>>(result);
        }
        public async Task<RegionDto> GetRegionByID(int id, bool trackchanges)
        {
            var region =await GetObjectAndCheckExistance(id,trackchanges);
            var regionDto = mapper.Map<RegionDto>(region);
            return regionDto;
        }

		public async Task<(Region region, RegionDto regionDto)> GetRegionForPartialUpdate(int regionId, bool trackchanges)
		{
			var region =await GetObjectAndCheckExistance(regionId, trackchanges);
			var regionDto = mapper.Map<RegionDto>(region);
			return (region, regionDto);
		}

		public async Task SavePatchChanges(Region region, RegionDto regionDto)
		{
			mapper.Map(regionDto, region);
			await repository.SaveAsync();
		}

		public async Task UpdateRegion(int regionId, RegionDto regionDto)
		{
			var region = await GetObjectAndCheckExistance(regionId, trackchanges: true);
			mapper.Map(regionDto, region);
            await  repository.SaveAsync();
		}

		private async Task<Region> GetObjectAndCheckExistance(int id,bool trackchanges)
        {
            var region =await repository.RegionRepo.GetRegionBasedOnId(id, trackchanges);
            if (region == null)
                throw new RegionNotFoundException(id);
            return region;
        }
    }
}
