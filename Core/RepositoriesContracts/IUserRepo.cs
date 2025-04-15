using Core.Entities;

namespace Core.RepositoryContracts
{
    public interface IUserRepo
    {
	    IQueryable<User> GetAllUser(bool trackchanges);
	    User? GetFromUserById(string id, bool trackchanges);
        Task CreateUser(User User);  
        void DeleteUser(User User);
        void associateUserWithSpecialization(User user, Specialization specialization);
    }

}
