using Service.Services;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs;

namespace Presentaion
{
    [Route("api/Token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IServiceManager service;
        public TokenController(IServiceManager service)
        {
            this.service = service;
        }
        [HttpPost("refresh")]
       
        public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
        {
            var tokenDtoToReturn = await service.UserService.RefreshToken(tokenDto);
            return Ok(tokenDtoToReturn);
        }
    }
}
