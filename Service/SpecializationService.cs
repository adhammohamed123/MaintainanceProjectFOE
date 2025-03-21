using AutoMapper;
using Contracts.Base;
using Core.RepositoryContracts;
using Service.Services;


namespace Service
{
    public class SpecializationService:ISpecializationService
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;
        private readonly ILoggerManager logger;

        public SpecializationService(IRepositoryManager repository,IMapper mapper, Contracts.Base.ILoggerManager logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }
    }
}
