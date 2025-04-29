using Service.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Service.DTOs.MaintainanceDtos;
using Core.Entities.ErrorModel;
using Service.DTOs;
using System.ComponentModel.DataAnnotations;

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
		public async Task<IActionResult> GetAlls()
		{
			var data =await service.FailureService.GetAllFailures(trackchanges: false);
			var response = new ResponseShape<NameWithIdentifierDto>(StatusCodes.Status200OK, "ok", default, data.ToList());
            return Ok(response);
		}

		[HttpGet("{failureId}", Name = "GetFailureBasedOnId")]
		public async Task<IActionResult> GetFailure(int failureId)
		{
			var data =await service.FailureService.GetById(failureId, trackchanges: false);
			var response = new ResponseShape<NameWithIdentifierDto>(StatusCodes.Status200OK, "ok", default, new List<NameWithIdentifierDto> { data });
            return Ok(response);
		}
		[Authorize(Roles = "Admin")]
        [HttpPost]
		public async Task<IActionResult> Create([FromBody][MaxLength(50, ErrorMessage = "اسم العطل يجب ان لا يتعدي ال 50 حرف")] string name)
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
