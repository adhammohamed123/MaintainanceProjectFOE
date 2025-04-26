using Core.Entities;
using Core.Entities.ErrorModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentaion.Attributes;
using Service.DTOs.UserDtos;
using Service.Services;
using System.Threading.Tasks;

namespace Presentaion
{
    [Authorize]
    [ApiController]
	[Route("api/Users")]
    
	public class UserController : ControllerBase
	{
		protected readonly IServiceManager service;

		public UserController(IServiceManager service)
		{
			this.service = service;
		}

        [HttpGet("UsersNamesWithIds")]
        public async Task<IActionResult> Get() { 
        
         var data=  await service.UserService.GetAllUsersNamesAndIds(trackchanges: false).ToListAsync();
            var response=new ResponseShape<UserDto>(StatusCodes.Status200OK, "ok", default, data);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
		public async Task<IActionResult> GetAlls()
		{
			var data = await service.UserService.GetAllUser(trackchanges: false).ToListAsync();
            var response = new ResponseShape<User>(StatusCodes.Status200OK, "ok", default, data);
            return Ok(response);
            //return Ok(data);
		}

		[HttpGet("{UserId}", Name = "GetUserBasedOnId")]
		public IActionResult GetUser(string UserId)
		{
			var data = service.UserService.GetFromUserById(UserId, trackchanges: false);
            var response = new ResponseShape<User>(StatusCodes.Status200OK, "ok", default, new List<User> { data });
            return Ok(response);
		}
        [Authorize(Roles = "Admin")] 
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            var result = await service.UserService.RegisterUser(userForRegistration);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                var errors = ModelState
                   .SelectMany(kvp => kvp.Value.Errors.Select(e => new { Key = kvp.Key, ErrorMessage = e.ErrorMessage }))
                   .ToDictionary(x => x.Key, x => x.ErrorMessage);
                var response = new ResponseShape<User>(StatusCodes.Status400BadRequest, "ok", errors, null);
                return BadRequest(response);
            }
            return StatusCode(201);
        }


        [HttpPost("AssociateWithSpecialization")]
        public async Task<IActionResult> associateUserWithSpecialization([FromBody] UserSpecializationDto userSpecializationDto)
        {
            await service.UserService.AssociateUserWithSpecialization(userSpecializationDto.UserId, userSpecializationDto.SpecializationId);
            return Ok();
        }
        // [HttpPost]
        //public async Task<IActionResult> Create([FromBody] string name)
        //{
        //	var newUser = await service.UserService. CreateUser(name);
        //	return CreatedAtRoute("GetUserBasedOnId", new { id = newUser.Id }, newUser);
        //}
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            if (!await service.UserService.ValidateUser(user))
                return Unauthorized();
            var tokenDto = await service.UserService.CreateToken(populateExp: true);
            var response = new ResponseShape<TokenDto>(StatusCode: StatusCodes.Status200OK, "ok", default, new List<TokenDto>() { tokenDto });
            return Ok(response);
        }
        // change password for account and user account
        [Authorize(Roles ="Admin")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordDto userForChangePassword)
        {
            var result = await service.UserService.changePassword(userForChangePassword);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                var errors = ModelState
                         .SelectMany(kvp => kvp.Value.Errors.Select(e => new { Key = kvp.Key, ErrorMessage = e.ErrorMessage }))
                         .ToDictionary(x => x.Key, x => x.ErrorMessage);
                var response = new ResponseShape<User>(StatusCodes.Status400BadRequest, "ok", errors, null);
                return BadRequest(response);
            }
            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{UserId}")]
		public async Task<IActionResult> Delete(string UserId)
		{
			await service.UserService.DeleteUser(UserId, trackchanges:true);
            var response = new ResponseShape<User>(StatusCodes.Status200OK, "تم حذف المستخدم بنجاح", errors: default, data: null);
            return Ok(response);
           // return NoContent();
		}
	}
}
