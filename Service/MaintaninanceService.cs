using AutoMapper;
using Contracts.Base;
using Core.Entities;
using Core.Exceptions;
using Core.Features;
using Core.RepositoryContracts;
using Service.DTOs;
using Service.Services;
using System.Threading.Tasks;

namespace Service
{
    public class MaintaninanceService:IMaintaninanceService
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;
        private readonly ILoggerManager logger;

        public MaintaninanceService(IRepositoryManager repository,IMapper mapper, Contracts.Base.ILoggerManager logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }

		public async Task<DeviceFailureHistoryDto> CreateAsync(DeviceFailureHistoryForCreationDto dto)
		{
			var entity = mapper.Map<DeviceFailureHistory>(dto);
			entity.CreatedByUserId = "UserID";
			await repository.MaintaninanceRepo.RegisterNew(entity,dto.FailureIds);
			await repository.SaveAsync();
			return mapper.Map<DeviceFailureHistoryDto>(entity);
		}

		public (IEnumerable<DeviceFailureHistoryDto> maintainRecords, MetaData metaData) GetAllAsync( MaintainanceRequestParameters maintainanceRequestParameters, bool trackchanges)
		{
			var entitiesWithMeta = repository.MaintaninanceRepo.GetDeviceFailureHistories(maintainanceRequestParameters,trackchanges);
			var entities = mapper.Map<IEnumerable<DeviceFailureHistoryDto>>(entitiesWithMeta);
			return (maintainRecord:entities,metaData:entitiesWithMeta.metaData);
		}

		public DeviceFailureHistoryDto? GetByIdAsync(int id)
		{
			var entity = repository.MaintaninanceRepo.GetDeviceFailureHistoryById(id, false);
			if (entity == null)
				throw new DeviceFailureHistoryNotFoundException(id);
			return mapper.Map<DeviceFailureHistoryDto>(entity);

		}

		public IEnumerable<DeviceFailureHistoryDto> GetDeviceFailureHistoriesByDeviceId(int deviceId, bool trackchanges)
		{
			var entities = repository.MaintaninanceRepo.GetDeviceFailureHistoriesByDeviceId(deviceId, trackchanges).ToList();
			return mapper.Map<IEnumerable<DeviceFailureHistoryDto>>(entities);
		}

		public (DeviceFailureHistoryDto dto, DeviceFailureHistory entity) GetDeviceFailureHistoryByIdForPartialUpdate(int id, bool trackchanges)
		{
			var entity=repository.MaintaninanceRepo.GetDeviceFailureHistoryById(id, trackchanges);
			if (entity == null)
				throw new DeviceFailureHistoryNotFoundException(id);
			var dto= mapper.Map<DeviceFailureHistoryDto>(entity);
			return (dto, entity);
		}

		public async Task SavePartialUpdate(DeviceFailureHistoryDto dto, DeviceFailureHistory entity)
		{
			mapper.Map(dto,entity);
			await repository.SaveAsync();
		}
	}
}
