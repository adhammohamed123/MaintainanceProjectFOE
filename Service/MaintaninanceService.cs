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

        // receive device for maintainance and create a new maintainance record
        public async Task<DeviceFailureHistoryDto> CreateAsync(DeviceFailureHistoryForCreationDto dto)
		{
			var entity = mapper.Map<DeviceFailureHistory>(dto);
			entity.CreatedByUserId = entity.ReceiverID ;
			//entity.State = Core.Entities.Enums.MaintainStatus.WorkingOnIt;
			var device = repository.DeviceRepo.GetById(entity.DeviceId, true);
			if (device == null)
				throw new DeviceNotFoundException(entity.DeviceId);
			if (device.DeviceStatus == Core.Entities.Enums.DeviceStatus.InMaintain)
                throw new WeCannotOpenANewMaintainanceOperationWhileAnotehrOneIsNotEndedForTheSameDevice(entity.Id,device.Id);

            device.DeviceStatus = Core.Entities.Enums.DeviceStatus.InMaintain;

			foreach (var failureId in dto.FailureIds)
			{
                //TODO Optimize this query to get all failures at once
                var failure = repository.FailureRepo.GetById(failureId, false);
                if (failure == null)
                    throw new FailureNotFoundException(failureId);

                entity.FailureMaintains.Add(new FailureMaintain
				{
					DeviceFailureHistoryId = entity.Id,
					FailureId = failureId,
				});
			}
		
			await repository.MaintaninanceRepo.RegisterNew(entity);
            await repository.SaveAsync();
			entity= repository.MaintaninanceRepo.GetDeviceFailureHistoryById(entity.Id, false);
            return mapper.Map<DeviceFailureHistoryDto>(entity);

            //var entity = mapper.Map<DeviceFailureHistory>(dto);
            //entity.CreatedByUserId = userId;
            //entity.ReceiverID=userId;
            //         await repository.MaintaninanceRepo.RegisterNew(entity,dto.FailureIds);
            //await repository.SaveAsync();
            //return mapper.Map<DeviceFailureHistoryDto>(entity);
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
			var entities = repository.MaintaninanceRepo.GetDeviceFailureHistoriesByDeviceId(deviceId, trackchanges);
			return mapper.Map<IEnumerable<DeviceFailureHistoryDto>>(entities);
		}

		public async Task MakeDeviceDone(int DeviceId)
		{
			var d = repository.DeviceRepo.GetById(DeviceId, true);
            if (d == null)
			{
                throw new DeviceNotFoundException(DeviceId);
            }
            d.DeviceStatus = Core.Entities.Enums.DeviceStatus.WithOwner;
			await repository.SaveAsync();
        }
		public async Task UpdateMaintainanceRecord(DeviceFailureHistoryDto dto)
		{
			var entity = repository.MaintaninanceRepo.GetDeviceFailureHistoryById(dto.Id, true);
            if (entity == null)
                throw new DeviceFailureHistoryNotFoundException(dto.Id);
			bool isSolved =
             dto.FailureMaintains
             .Any(f => f.State == Core.Entities.Enums.FailureActionDone.Solved);

            mapper.Map(dto, entity);
			if (isSolved)
			{
                entity.State = Core.Entities.Enums.MaintainStatus.Done;
            }
			await repository.SaveAsync();
        }
		public (DeviceFailureHistoryDto dto, DeviceFailureHistory entity) GetDeviceFailureHistoryByIdForPartialUpdate(int id, bool trackchanges)
		{
			var entity=repository.MaintaninanceRepo.GetDeviceFailureHistoryById(id, trackchanges);
			if (entity == null)
				throw new DeviceFailureHistoryNotFoundException(id);
			
            var dto= mapper.Map<DeviceFailureHistoryDto>(entity);
			return (dto, entity);
		}

		public async Task SavePartialUpdate(DeviceFailureHistoryDto dto, DeviceFailureHistory entity,string UserId)
		{
			mapper.Map(dto,entity);
			entity.MaintainerId = UserId;
            entity.LastModifiedUserId = UserId;
            await repository.SaveAsync();
		}
	}
}
