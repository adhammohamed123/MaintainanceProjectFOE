using Core.Entities.Enums;
using Core.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Presentaion.Attributes;
using Service.DTOs.MaintainanceDtos;
using Service.Services;
using System.Security.Claims;

namespace Presentaion
{
	[Authorize]
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
            var response = new
            {
                data = result.maintainRecords,
                pagination = result.metaData// Include pagination info in the response body
            };

            return Ok(response);
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
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Create([FromBody] DeviceFailureHistoryForCreationDto dto)
		{
			var user=User.FindFirstValue(ClaimTypes.NameIdentifier);
            var created = await _service.MaintaninanceService.CreateAsync(dto,user);
			
	    	return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
		}
		[HttpPut("MarkDeviceDone")]
		public async Task<IActionResult> MarkDeviceDone(int MaintainId)
		{
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _service.MaintaninanceService.MakeDeviceDone(MaintainId,user);
			return NoContent();
		}
		[HttpPut("ChangeFailureStatus")]
		public async Task<IActionResult> ChangeFailureStatus(int MaintainId,int FailureId, FailureActionDone status)
		{
            await _service.MaintaninanceService.ChangeFailureStatus(MaintainId,FailureId, status);
			return NoContent();
		}
		[HttpPut]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Update([FromBody] DeviceFailureHistoryDto dto)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			await _service.MaintaninanceService.UpdateMaintainanceRecord(dto,userId);
            return NoContent();
        }

        [HttpPatch("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Update(int id ,[FromBody] JsonPatchDocument<DeviceFailureHistoryDto> dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


            var result = _service.MaintaninanceService.GetDeviceFailureHistoryByIdForPartialUpdate(id, true);
			dto.ApplyTo(result.dto);
			await _service.MaintaninanceService.SavePartialUpdate(result.dto, result.entity,userId);
			return NoContent();
		}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int id)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _service.MaintaninanceService.DeleteMaintain(id, userId);
            return NoContent();
        }

    }
}
