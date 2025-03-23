using Service.Services;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Presentaion
{
    [Authorize(Roles = "Admin")]
    [ApiController]
	[Route("api/Users")]
	public class UserController : ControllerBase
	{
		protected readonly IServiceManager service;

		public UserController(IServiceManager service)
		{
			this.service = service;
		}

		[HttpGet]
        
		public IActionResult GetAlls()
		{
			var data = service.UserService.GetAllUser(trackchanges: false).ToList();
			return Ok(data);
		}

		[HttpGet("{UserId}", Name = "GetUserBasedOnId")]
		public IActionResult GetUser(string UserId)
		{
			var data = service.UserService.GetFromUserById(UserId, trackchanges: false);
			return Ok(data);
		}
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
                return BadRequest(ModelState);
            }
            return StatusCode(201);
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
            return Ok(tokenDto);
        }
        [HttpDelete("{UserId}")]
		public async Task<IActionResult> Delete(string UserId)
		{
			await service.UserService.DeleteUser(UserId, trackchanges:true);
			return NoContent();
		}
	}
}
