using Core.RepositoryContracts;
using Core.Entities;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepo : BaseRepository<User>, IUserRepo
    {
        public UserRepo(FoeMaintainContext context) : base(context)
        {
        }

        public void associateUserWithSpecialization(User user, Specialization specialization)
         =>  user.Specializations.Add(specialization);
        

        public async Task CreateUser(User User)
		=> await Create(User);

		public void DeleteUser(User User)
		=>SoftDelete(User);

		public IQueryable<User> GetAllUser(bool trackchanges)
		=>FindAll(trackchanges);

		public User? GetFromUserById(string id, bool trackchanges)
		=> FindByCondition(s => s.Id.Equals(id), trackchanges).SingleOrDefault();
	}
}
