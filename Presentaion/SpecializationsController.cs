using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace Presentaion
{
    [ApiController]
    [Route("api/Specializations")]
    [Authorize]
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
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateSpecialization([FromBody] string specializationName)
        {
            var specialization = await service.SpecializationService.CreateSpecialization(specializationName);
            return CreatedAtAction(nameof(GetSpecialization), new { SpecializationId = specialization.Id }, specialization);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{SpecializationId}")]
        public async Task<IActionResult> Delete(int SpecializationId)
        {
            await service.SpecializationService.DeleteSpecialization(SpecializationId);
            return NoContent();
        }
    }
}
