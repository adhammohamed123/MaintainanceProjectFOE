using Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentaion
{
	[ApiController]
	[Route("api/Stuffs")]
	public class StuffController : ControllerBase
	{
		protected readonly IServiceManager service;

		public StuffController(IServiceManager service)
		{
			this.service = service;
		}

		[HttpGet]
		public IActionResult GetAlls()
		{
			var data = service.StuffService.GetAllStuff(trackchanges: false).ToList();
			return Ok(data);
		}

		[HttpGet("{stuffId}", Name = "GetStuffBasedOnId")]
		public IActionResult GetStuff(int stuffId)
		{
			var data = service.StuffService.GetFromStuffById(stuffId, trackchanges: false);
			return Ok(data);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] string name)
		{
			var newStuff = await service.StuffService. CreateStuff(name);
			return CreatedAtRoute("GetStuffBasedOnId", new { id = newStuff.Id }, newStuff);
		}
		[HttpDelete("{stuffId}")]
		public async Task<IActionResult> Delete(int stuffId)
		{
			await service.StuffService.DeleteStuff(stuffId, trackchanges:true);
			return NoContent();
		}
	}
}
