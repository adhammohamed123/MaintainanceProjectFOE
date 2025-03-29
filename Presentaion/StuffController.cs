using Service.Services;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Presentaion
{
   // [Authorize(Roles = "Admin")]
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
            return Ok(tokenDto);
        }
        [HttpDelete("{UserId}")]
		public async Task<IActionResult> Delete(string UserId)
		{
			await service.UserService.DeleteUser(UserId, trackchanges:true);
			return NoContent();
		}
	}



    [ApiController]
    [Route("api/Specializations")]
    public class SpecializationsController : ControllerBase
    {
        protected readonly IServiceManager service;
        public SpecializationsController(IServiceManager service)
        {
            this.service = service;
        }
        [HttpGet]
        public IActionResult GetAlls()
        {
            var data = service.SpecializationService.GetAllSpecializations(trackchanges: false).ToList();
            return Ok(data);
        }
        [HttpGet("{SpecializationId}")]
        public IActionResult GetSpecialization(int SpecializationId)
        {
            var data = service.SpecializationService.GetSpecializationById(SpecializationId, trackchanges: false);
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSpecialization([FromBody] string specializationName)
        {
            var specialization = await service.SpecializationService.CreateSpecialization(specializationName);
            return CreatedAtAction(nameof(GetSpecialization), new { SpecializationId = specialization.Id }, specialization);
        }
        [HttpDelete("{SpecializationId}")]
        public async Task<IActionResult> Delete(int SpecializationId)
        {
            await service.SpecializationService.DeleteSpecialization(SpecializationId);
            return NoContent();
        }
    }
}
