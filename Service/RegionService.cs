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
            var region = new Region() { Name = name };
            await repository.RegionRepo.CreateNewRegionAsync(region);
            await repository.SaveAsync();
            return mapper.Map<RegionDto>(region);
        }

		public async Task DeleteRegionAsync(int id)
		{
		  var region = GetObjectAndCheckExistance(id, trackchanges: true);
			repository.RegionRepo.DeleteRegion(region);
			await repository.SaveAsync();
		}

		public IQueryable<RegionDto> GetAllRegisteredRegion(bool trackchanges)
        => repository.RegionRepo.GetAllRegisteredRegion(trackchanges)
            .ProjectTo<RegionDto>(mapper.ConfigurationProvider);

        public RegionDto GetRegionByID(int id, bool trackchanges)
        {
            var region = GetObjectAndCheckExistance(id,trackchanges);
            var regionDto = mapper.Map<RegionDto>(region);
            return regionDto;
        }

		public (Region region, RegionDto regionDto) GetRegionForPartialUpdate(int regionId, bool trackchanges)
		{
			var region = GetObjectAndCheckExistance(regionId, trackchanges);
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
			var region = GetObjectAndCheckExistance(regionId, trackchanges: true);
			mapper.Map(regionDto, region);
            await  repository.SaveAsync();
		}

		private Region GetObjectAndCheckExistance(int id,bool trackchanges)
        {
            var region = repository.RegionRepo.GetRegionBasedOnId(id, trackchanges);
            if (region == null)
                throw new RegionNotFoundException(id);
            return region;
        }
    }
}
