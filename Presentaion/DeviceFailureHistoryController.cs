using Service.Services;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Microsoft.AspNetCore.JsonPatch;
using Core.Features;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.JsonPatch.Operations;

namespace Presentaion
{
	//[Authorize]
	[Route("api/maintenance")]
	[ApiController]
	public class DeviceFailureHistoryController : ControllerBase
	{
		private readonly IServiceManager _service;

		public DeviceFailureHistoryController(IServiceManager service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] MaintainanceRequestParameters maintainanceRequestParameters)
		{
			var result = _service.MaintaninanceService.GetAllAsync(maintainanceRequestParameters,trackchanges:false);
			Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.metaData));
			return Ok(result.maintainRecords);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var result = _service.MaintaninanceService.GetByIdAsync(id);
			if (result == null) return NotFound();
			return Ok(result);
		}

		[HttpGet("/api/device/{id}/maintainHistory")]
		public async Task<IActionResult> GetByDeviceId(int id)
		{
			var result = _service.MaintaninanceService.GetDeviceFailureHistoriesByDeviceId(id,false);
			if (result == null) return NotFound();
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] DeviceFailureHistoryForCreationDto dto)
		{
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "41DE9DCE-5A19-4C25-B336-8BA113BC9886";
            var created = await _service.MaintaninanceService.CreateAsync(dto,userId);
			
	    	return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
		}

		[HttpPatch("{id}")]
		public async Task<IActionResult> Update(int id ,[FromBody] JsonPatchDocument<DeviceFailureHistoryDto> dto)
        {
		 var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

			
            var result= _service.MaintaninanceService.GetDeviceFailureHistoryByIdForPartialUpdate(id, true);
			dto.ApplyTo(result.dto);
			await _service.MaintaninanceService.SavePartialUpdate(result.dto, result.entity,userId);
			return NoContent();
		}

	}
}
