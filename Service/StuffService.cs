using AutoMapper;
using Contracts.Base;
using Core.RepositoryContracts;
using Service.DTOs;
using Service.Services;
using Core.Entities;
using AutoMapper.QueryableExtensions;
using Core.Exceptions;
namespace Service
{
    public class StuffService:IStuffService
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;
        private readonly ILoggerManager logger;

        public StuffService(IRepositoryManager repository,IMapper mapper, Contracts.Base.ILoggerManager logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }

		public async Task<NameWithIdentifierDto> CreateStuff(string name)
		{
			var newStuff = new Stuff { Name = name };
		    await	   repository.StuffRepo.CreateStuff(newStuff);
		     await	repository.SaveAsync();
			return mapper.Map<NameWithIdentifierDto>(newStuff);
		}

		public async Task DeleteStuff(int id, bool trackchanges)
		{
			var stuff = GetObjectAndCheckExistance(id, trackchanges);
			repository.StuffRepo.DeleteStuff(stuff);
			await repository.SaveAsync();
		}

		public IQueryable<NameWithIdentifierDto> GetAllStuff(bool trackchanges)
		=> repository.StuffRepo.GetAllStuff(trackchanges).ProjectTo<NameWithIdentifierDto>(mapper.ConfigurationProvider);

		public NameWithIdentifierDto? GetFromStuffById(int id, bool trackchanges)
		{
			var stuff = GetObjectAndCheckExistance(id, trackchanges);
			return mapper.Map<NameWithIdentifierDto>(stuff);
		}
		private Stuff GetObjectAndCheckExistance(int id, bool trackchanges)
		{
			var stuff = repository.StuffRepo.GetFromStuffById(id, trackchanges);
			if (stuff == null)
			{
				throw new StuffNotFoundException(id);
			}
			return stuff;
		}
	}
}
