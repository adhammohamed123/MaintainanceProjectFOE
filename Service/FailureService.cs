using AutoMapper;
using Contracts.Base;
using Core.RepositoryContracts;
using Service.DTOs;
using Service.Services;
using Core.Entities;
using AutoMapper.QueryableExtensions;
using Core.Exceptions;
using System.Threading.Tasks;

namespace Service
{
    public class FailureService:IFailureService
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;
        private readonly ILoggerManager logger;

        public FailureService(IRepositoryManager repository,IMapper mapper, Contracts.Base.ILoggerManager logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }

		public async Task<NameWithIdentifierDto> CreateFailure(string failureName)
		{
            if (repository.FailureRepo.CheckExistance(failureName.Trim()))
                throw new FailureAlreadyRegistered(failureName);
            var failure = new Failure() { Name = failureName };
		    await	repository.FailureRepo.CreateFailure(failure);
			await repository.SaveAsync();
			return mapper.Map<NameWithIdentifierDto>(failure);

		}

		public async Task DeleteFailure(int id)
		{
			var failure = await repository.FailureRepo.GetById(id, true);
			if (failure == null)
			{
				throw new FailureNotFoundException(id);
			}
			repository.FailureRepo.DeleteFailure(failure);
			await repository.SaveAsync();
		}

		public async Task<IEnumerable<NameWithIdentifierDto>> GetAllFailures(bool trackchanges)
		{
			var allFauils = await repository.FailureRepo.GetAllFailures(trackchanges);
			return mapper.Map<IEnumerable<NameWithIdentifierDto>>(allFauils);
        }

		public async Task<NameWithIdentifierDto> GetById(int id, bool trackchanges)
		{
			var faliure= await repository.FailureRepo.GetById(id, trackchanges);
			if (faliure == null)
			{
				throw new FailureNotFoundException( id);
			}
			return mapper.Map<NameWithIdentifierDto>(faliure);
		}
	}
}
