using Core.Entities;

namespace Core.RepositoryContracts
{
    public interface IUserRepo
    {
	    Task<IEnumerable<User>> GetAllUser(bool trackchanges);
	    Task<User?> GetFromUserById(string id, bool trackchanges);
        Task CreateUser(User User);  
        void DeleteUser(User User);
        void associateUserWithSpecialization(User user, Specialization specialization);
    }

}
