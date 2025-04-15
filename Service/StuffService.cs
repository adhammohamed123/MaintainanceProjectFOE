using AutoMapper;
using Contracts.Base;
using Core.RepositoryContracts;
using Service.DTOs;
using Service.Services;
using Core.Entities;
using AutoMapper.QueryableExtensions;
using Core.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Service.DTOs.UserDtos;
namespace Service
{
    public class UserService:IUserService
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;
        private readonly ILoggerManager logger;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration configuration;
        private User? _user;
        public UserService(IRepositoryManager repository,IMapper mapper, Contracts.Base.ILoggerManager logger,UserManager<User> userManager,IConfiguration configuration)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
            this._userManager = userManager;
            this.configuration = configuration;
        }

		//public async Task<NameWithIdentifierDto> CreateUser(string name)
		//{
		//	var newUser = new User { Name = name };
		//    await	   repository.UserRepo.CreateUser(newUser);
		//     await	repository.SaveAsync();
		//	return mapper.Map<NameWithIdentifierDto>(newUser);
		//}

		public async Task DeleteUser(string id, bool trackchanges)
		{
			var User = GetObjectAndCheckExistance(id, trackchanges);
			repository.UserRepo.DeleteUser(User);
			await repository.SaveAsync();
		}

		public IQueryable<User> GetAllUser(bool trackchanges)
		=> repository.UserRepo.GetAllUser(trackchanges);
        public IQueryable<UserDto> GetAllUsersNamesAndIds(bool trackchanges)
        => repository.UserRepo.GetAllUser(trackchanges)
            .ProjectTo<UserDto>(mapper.ConfigurationProvider);

        public User? GetFromUserById(string id, bool trackchanges)
		{
			var User = GetObjectAndCheckExistance(id, trackchanges);
			return User;
		}

       
        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            var user = mapper.Map<User>(userForRegistration);
            user.Email = null;
            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (result.Succeeded)
                await _userManager.AddToRolesAsync(user, userForRegistration.Roles);
            return result;
        }
        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuth)
        {
            _user = await _userManager.FindByNameAsync(userForAuth.UserName);

            var result = (_user != null && await _userManager.CheckPasswordAsync(_user,userForAuth.Password));
            if (!result)
               logger.LogWarn($"{nameof(ValidateUser)}: Authentication failed. Wrong user name or password."); 
            return result;
        }

        public async Task<TokenDto> CreateToken(bool populateExp)
        {
            var signingCredentials = GetSigningCredentials();

            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            var refreshToken = GenerateRefreshToken();

            _user.RefreshToken = refreshToken;

            if (populateExp)
                _user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            await _userManager.UpdateAsync(_user);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return new TokenDto(accessToken, refreshToken);
        }

        public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
        {
            var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);
            var user = await _userManager.FindByNameAsync(principal.Identity.Name);
            if (user == null || user.RefreshToken != tokenDto.RefreshToken ||
            user.RefreshTokenExpiryTime <= DateTime.Now)
                throw new RefreshTokenBadRequest();
            _user = user;
            return await CreateToken(populateExp: false);
        }

        public async Task AssociateUserWithSpecialization(string userId, int specializationId)
        { 
            var user = GetObjectAndCheckExistance(userId, trackchanges: true);
            var specialization = repository.SpecializationRepo.GetSpecializationById(specializationId, trackchanges: true);
            if (specialization == null)
                throw new SpecializationNotFoundException(specializationId);
            repository.UserRepo.associateUserWithSpecialization(user, specialization);
            await repository.SaveAsync();
        }
        public async Task<IdentityResult> changePassword( ChangeUserPasswordDto changeUserPasswordDto)
        {
           var user = await _userManager.FindByIdAsync(changeUserPasswordDto.UserId);
            if (user == null)
                throw new UserNotFoundException(changeUserPasswordDto.UserId);

           var token=await  _userManager.GeneratePasswordResetTokenAsync(user);
            var operationResult =await _userManager.ResetPasswordAsync(user, token, changeUserPasswordDto.NewPassword);
          
            return operationResult;
        }

        #region Private Helper methods
        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"));
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user.UserName),
                new Claim(ClaimTypes.NameIdentifier, _user.Id)
            };

            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken
            (
            issuer: jwtSettings["validIssuer"],
            audience: jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
            signingCredentials: signingCredentials
            );
            return tokenOptions;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"))),
                ValidateLifetime = true,
                ValidIssuer = jwtSettings["validIssuer"],
                ValidAudience = jwtSettings["validAudience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }
            return principal;
        }
        private User GetObjectAndCheckExistance(string id, bool trackchanges)
        {
            var User = repository.UserRepo.GetFromUserById(id, trackchanges);
            if (User == null)
            {
                throw new UserNotFoundException(id);
            }
            return User;
        }

      

        #endregion



    }
}
