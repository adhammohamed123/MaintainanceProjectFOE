using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contracts.Base;
using Repository.Repository;
using Service.DTOs;
using Service.Services;
using Core.Exceptions;
using Microsoft.EntityFrameworkCore;
namespace Service
{
    public class DeviceService : IDeviceService
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;
        private readonly ILoggerManager logger;

        public DeviceService(IRepositoryManager repository, IMapper mapper,ILoggerManager logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public IQueryable<DeviceDto> GetAllDevices(bool trackchanges)
        {
            var DevicesAsEntities = repository.DeviceRepo.GetAllRegisteredDevices(trackchanges);
            return DevicesAsEntities.ProjectTo<DeviceDto>(mapper.ConfigurationProvider);
        }

        public DeviceDto GetDeviceByID(int id, bool trackchanges)
        {
            var device = repository.DeviceRepo.GetById(id, trackchanges);
            if (device == null)
                throw new DeviceNotFoundException(id);
            return mapper.Map<DeviceDto>(device);
        }
    }
}
