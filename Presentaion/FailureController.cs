using Service.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Service.DTOs.MaintainanceDtos;
using Core.Entities.ErrorModel;
using Service.DTOs;

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
			var response = new ResponseShape<NameWithIdentifierDto>(StatusCodes.Status200OK, "ok", default, data);
            return Ok(response);
		}

		[HttpGet("{failureId}", Name = "GetFailureBasedOnId")]
		public IActionResult GetFailure(int failureId)
		{
			var data = service.FailureService.GetById(failureId, trackchanges: false);
			var response = new ResponseShape<NameWithIdentifierDto>(StatusCodes.Status200OK, "ok", default, new List<NameWithIdentifierDto> { data });
            return Ok(response);
		}
		[Authorize(Roles = "Admin")]
        [HttpPost]
		public async Task<IActionResult> Create([FromBody] string name)
		{
			var newFailure = await service.FailureService.CreateFailure(name);
			var response = new ResponseShape<NameWithIdentifierDto>(StatusCodes.Status201Created, "تم اضافة العطل بنجاح", default, new List<NameWithIdentifierDto>() { newFailure });
            
            return CreatedAtAction(nameof(GetFailure), new { failureId = newFailure.Id }, response);
            //return CreatedAtRoute("GetFailureBasedOnId", new { failureId = newFailure.Id }, newFailure);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{failureId}")]
		public async Task<IActionResult> Delete(int failureId)
		{
		 	await service.FailureService.DeleteFailure(failureId);
            var response = new ResponseShape<NameWithIdentifierDto>(StatusCodes.Status200OK, "تم حذف العطل بنجاح", errors: default, data: null);
            return Ok(response);
		}

	}
}
