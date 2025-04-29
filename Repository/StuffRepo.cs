using Core.RepositoryContracts;
using Core.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

		public async Task<IEnumerable<User>> GetAllUser(bool trackchanges)
		=> await FindAll(trackchanges).ToListAsync();

		public async Task< User?> GetFromUserById(string id, bool trackchanges)
		=> await FindByCondition(s => s.Id.Equals(id), trackchanges).SingleOrDefaultAsync();
	}
}
