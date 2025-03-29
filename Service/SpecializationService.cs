using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contracts.Base;
using Core.Entities;
using Core.Exceptions;
using Core.RepositoryContracts;
using Repository;
using Service.DTOs;
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

        public async Task<NameWithIdentifierDto> CreateSpecialization(string specializationName)
        {
            var specialization = new Specialization { Name = specializationName };
             await repository.SpecializationRepo.CreateSpecialization(specialization);
             await repository.SaveAsync();
             return mapper.Map<NameWithIdentifierDto>(specialization);
        }

        public async Task DeleteSpecialization(int id)
        {
            var specialization = GetObjectAndCheckExistance(id,true);
            repository.SpecializationRepo.DeleteSpecialization(specialization);
            await repository.SaveAsync();
        }

        private Specialization GetObjectAndCheckExistance(int id,bool trackChanges)
        {
            var specialization = repository.SpecializationRepo.GetSpecializationById(id, trackChanges);
            if (specialization == null)
            {
                throw new SpecializationNotFoundException(id);
            }
            return specialization;
        }

        public IQueryable<NameWithIdentifierDto> GetAllSpecializations(bool trackchanges)
        => repository.SpecializationRepo.GetAllSpecializations(trackchanges)
            .ProjectTo<NameWithIdentifierDto>(mapper.ConfigurationProvider);

        public NameWithIdentifierDto? GetSpecializationById(int id, bool trackchanges)
        {
            var specialization = GetObjectAndCheckExistance(id, trackchanges);
            return mapper.Map<NameWithIdentifierDto>(specialization);
        }
    }
}
