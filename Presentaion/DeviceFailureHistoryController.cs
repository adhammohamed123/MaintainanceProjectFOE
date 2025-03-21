using Service.Services;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Microsoft.AspNetCore.JsonPatch;
using Core.Features;
using System.Text.Json;

namespace Presentaion
{
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
			var created = await _service.MaintaninanceService.CreateAsync(dto);
			return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
		}

		[HttpPatch("{id}")]
		public async Task<IActionResult> Update(int id ,[FromBody] JsonPatchDocument<DeviceFailureHistoryDto> dto)
		{
			var result= _service.MaintaninanceService.GetDeviceFailureHistoryByIdForPartialUpdate(id, true);
			dto.ApplyTo(result.dto);
			await _service.MaintaninanceService.SavePartialUpdate(result.dto, result.entity);
			return NoContent();
		}

	}
}
