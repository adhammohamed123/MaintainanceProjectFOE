using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Service.DTOs.UserDtos;
namespace Service.Services
{
    public interface IUserService
    {
		Task<IEnumerable<User>> GetAllUser(bool trackchanges);

        Task<IEnumerable<UserDto>>GetAllUsersNamesAndIds(bool trackchanges);
        Task<User?> GetFromUserById(string id, bool trackchanges);
		//Task<NameWithIdentifierDto> CreateUser(string name);
        Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration);
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<TokenDto> CreateToken(bool populateExp);
        Task<TokenDto> RefreshToken(TokenDto tokenDto);
        Task DeleteUser(string id,bool trackchanges);
        Task AssociateUserWithSpecialization(string userId, int specializationId);
        Task<IdentityResult> changePassword(ChangeUserPasswordDto changeUserPasswordDto);

    }
}
