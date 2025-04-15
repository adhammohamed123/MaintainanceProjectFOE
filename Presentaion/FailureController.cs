using Service.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Presentaion
{
	[Authorize]
	[ApiController]
	[Route("api/Failures")]
	public class FailureController : ControllerBase
	{
		protected readonly IServiceManager service;

		public FailureController(IServiceManager service)
		{
			this.service = service;
		}

		[HttpGet]
		public IActionResult GetAlls()
		{
			var data = service.FailureService.GetAllFailures(trackchanges: false).ToList();
			return Ok(data);
		}

		[HttpGet("{failureId}", Name = "GetFailureBasedOnId")]
		public IActionResult GetFailure(int failureId)
		{
			var data = service.FailureService.GetById(failureId, trackchanges: false);
			return Ok(data);
		}
		[Authorize(Roles = "Admin")]
        [HttpPost]
		public async Task<IActionResult> Create([FromBody] string name)
		{
			var newFailure = await service.FailureService.CreateFailure(name);
			return CreatedAtAction(nameof(GetFailure), new { failureId = newFailure.Id }, newFailure);
            //return CreatedAtRoute("GetFailureBasedOnId", new { failureId = newFailure.Id }, newFailure);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{failureId}")]
		public async Task<IActionResult> Delete(int failureId)
		{
		 	await service.FailureService.DeleteFailure(failureId);
			return NoContent();
		}

	}
}
