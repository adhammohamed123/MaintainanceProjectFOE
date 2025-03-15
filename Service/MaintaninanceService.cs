using AutoMapper;
using Contracts.Base;
using Repository.Repository;
using Service.Services;


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
    }
}
