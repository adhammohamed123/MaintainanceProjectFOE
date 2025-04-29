using AutoMapper;
using Contracts.Base;
using Core.Entities;
using Core.Entities.Enums;
using Core.Exceptions;
using Core.Features;
using Core.RepositoryContracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Service.DTOs;
using Service.DTOs.MaintainanceDtos;
using Service.Services;
using System.Threading.Tasks;

namespace Service
{
	public class MaintaninanceService : IMaintaninanceService
	{
		private readonly IRepositoryManager repository;
		private readonly IMapper mapper;
		private readonly ILoggerManager logger;

		public MaintaninanceService(IRepositoryManager repository, IMapper mapper, Contracts.Base.ILoggerManager logger)
		{
			this.repository = repository;
			this.mapper = mapper;
			this.logger = logger;
		}

		// receive device for maintainance and create a new maintainance record
		public async Task<DeviceFailureHistoryDto> CreateAsync(DeviceFailureHistoryForCreationDto dto, string userId)
		{
			var entity = mapper.Map<DeviceFailureHistory>(dto);
			entity.CreatedByUserId = userId;
			//entity.State = Core.Entities.Enums.MaintainStatus.WorkingOnIt;
			var device =await repository.DeviceRepo.GetById(entity.DeviceId, true);
			if (device == null)
				throw new DeviceNotFoundException(entity.DeviceId);
			if (device.DeviceStatus == Core.Entities.Enums.DeviceStatus.InMaintain)
				throw new WeCannotOpenANewMaintainanceOperationWhileAnotehrOneIsNotEndedForTheSameDevice(entity.Id, device.Id);

			device.DeviceStatus = Core.Entities.Enums.DeviceStatus.InMaintain;
			foreach (var failureId in dto.FailureIds)
			{
				//TODO Optimize this query to get all failures at once
				var failure =await repository.FailureRepo.GetById(failureId, false);
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
			entity = await repository.MaintaninanceRepo.GetDeviceFailureHistoryById(entity.Id, false);
			var result = mapper.Map<DeviceFailureHistoryDto>(entity);
			result.MAC = device.MAC;
			result.DomainIDIfExists = device.DomainIDIfExists;

            return result;

			//var entity = mapper.Map<DeviceFailureHistory>(dto);
			//entity.CreatedByUserId = userId;
			//entity.ReceiverID=userId;
			//         await repository.MaintaninanceRepo.RegisterNew(entity,dto.FailureIds);
			//await repository.SaveAsync();
			//return mapper.Map<DeviceFailureHistoryDto>(entity);
		}

		public async Task<(IEnumerable<DeviceFailureHistoryDto> maintainRecords, MetaData metaData)> GetAllAsync(MaintainanceRequestParameters maintainanceRequestParameters, bool trackchanges)
		{
			var entitiesWithMeta =  repository.MaintaninanceRepo.GetDeviceFailureHistories(maintainanceRequestParameters, trackchanges);
			var entities = mapper.Map<IEnumerable<DeviceFailureHistoryDto>>(entitiesWithMeta);
			return (maintainRecords: entities, metaData: entitiesWithMeta.metaData);
		}

		public async Task<DeviceFailureHistoryDto?> GetByIdAsync(int id)
		{
			var entity = await repository.MaintaninanceRepo.GetDeviceFailureHistoryById(id, false);
			if (entity == null)
				throw new DeviceFailureHistoryNotFoundException(id);
			return mapper.Map<DeviceFailureHistoryDto>(entity);

		}

		public async Task<IEnumerable<DeviceFailureHistoryDto>> GetDeviceFailureHistoriesByDeviceId(int deviceId, bool trackchanges)
		{
			var entities = await repository.MaintaninanceRepo.GetDeviceFailureHistoriesByDeviceId(deviceId, trackchanges);
			
			
			return mapper.Map<IEnumerable<DeviceFailureHistoryDto>>(entities);
		}
		
        public async Task MakeDeviceDone(int MaintainId, string userId)
		{
			var maintain = await repository.MaintaninanceRepo.GetDeviceFailureHistoryById(MaintainId, trackchanges: true);
			
			if (maintain == null)
				throw new DeviceFailureHistoryNotFoundException(MaintainId);
			maintain.LastModifiedUserId=userId;
            if (maintain.FailureMaintains.Any(m => m.FailureActionDone == FailureActionDone.NotSolved))
            {
                throw new CannotDeliverDeviceWithNotSolvedFailures(maintain.DeviceId);
            }
            if (maintain.State == Core.Entities.Enums.MaintainStatus.WorkingOnIt)
			{
                throw new CannotDeliverDeviceBecauseStateIsNotWorkingOn();
            }
			if(maintain.IsDelivered)
			{
                throw new CannotDeliverDeviceThatIsDelivered();
            }
			if(maintain.MaintainerId is null)
			{
                throw new CannotDeliverDeviceThatIsNotAssignedToAnyone();
            }


                maintain.IsDelivered = true;
			var d =  await repository.DeviceRepo.GetById(maintain.DeviceId, true);
			if (d == null)
			{
				throw new DeviceNotFoundException(maintain.DeviceId);
			}
			d.DeviceStatus = Core.Entities.Enums.DeviceStatus.WithOwner;

			await repository.SaveAsync();
		}
		public async Task UpdateMaintainanceRecord(DeviceFailureHistoryDto dto,string userId)
		{
			var entity = await repository.MaintaninanceRepo.GetDeviceFailureHistoryById(dto.Id, true);
			if (entity == null)
				throw new DeviceFailureHistoryNotFoundException(dto.Id);
			if(entity.IsDelivered)
            {
                throw new CannotUpdateMaintainanceRecordThatIsAlreadyDelivered();
            }
            bool isSolved =
			 dto.FailureMaintains
			 .Any(f => f.State == Core.Entities.Enums.FailureActionDone.Solved);
			if(entity.State != MaintainStatus.WorkingOnIt)
			{
                throw new CannotUpdateMaintainanceRecordWhileItIsDone(entity.Id);
            }
            mapper.Map(dto, entity);
			if (isSolved)
			{
				entity.State = Core.Entities.Enums.MaintainStatus.Done;
			}
			entity.LastModifiedUserId = userId;
            await repository.SaveAsync();
		}
		public async Task<(DeviceFailureHistoryDto dto, DeviceFailureHistory entity)> GetDeviceFailureHistoryByIdForPartialUpdate(int id, bool trackchanges)
		{
			var entity = await repository.MaintaninanceRepo.GetDeviceFailureHistoryById(id, trackchanges);
			if (entity == null)
				throw new DeviceFailureHistoryNotFoundException(id);
			if(entity.IsDelivered)
			{
				throw new CannotUpdateMaintainanceRecordThatIsAlreadyDelivered();
			}
			var dto = mapper.Map<DeviceFailureHistoryDto>(entity);
			return (dto, entity);
		}

		public async Task SavePartialUpdate(DeviceFailureHistoryDto dto, DeviceFailureHistory entity,string userId)
		{
			mapper.Map(dto, entity);
			entity.LastModifiedUserId = userId;

            if (entity.State == MaintainStatus.Done|| entity.State == MaintainStatus.Canceled)
			{
                if (entity.FailureMaintains.Any(m => m.FailureActionDone == FailureActionDone.NotSolved))
				{
					throw new CannotMakeMaintenanceStateDoneOrCancelledWhileDeviceHasNotSolvedFailures(entity.DeviceId);
				}
			}
            await repository.SaveAsync();
		}

      
        public async Task ChangeFailureStatus(int MaintainId, int FailureId, FailureActionDone status)
        {
            var maintain =  await repository.FailureMaintainRepo.GetFailureMaintain(MaintainId, FailureId, true);
            if (maintain == null) throw new FailureMaintainNotFoundException(MaintainId, FailureId);
            maintain.FailureActionDone = status;
            await repository.SaveAsync();
        }

        public async Task DeleteMaintain(int MaintainId, string userId)
        {
            var maintain = await repository.MaintaninanceRepo.GetDeviceFailureHistoryById(MaintainId, true);
            if (maintain == null)
                throw new DeviceFailureHistoryNotFoundException(MaintainId);
            if (!maintain.IsDelivered)
				throw new CannotDeleteMaintainanceRecordThatIsNotDelivered(MaintainId);
			repository.MaintaninanceRepo.DeleteMaintainance(maintain,userId);
		 	await repository.SaveAsync();
        }
    }
}
