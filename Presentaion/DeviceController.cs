using Service.Services;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Core.Entities;
using Azure;
using Core.Features;
using System.Text.Json;

namespace Presentaion
{


	[ApiController]
	[Route("api/Regions/{regionId}/Gates/{gateId}/Departments/{deptId}/offices/{officeId}/Devices")]
	public class DeviceController : ControllerBase
	{
		private readonly IServiceManager service;

		public DeviceController(IServiceManager service)
		{
			this.service = service;
		}

		[HttpGet("/api/Devices")]
		public IActionResult GetAll([FromQuery]DeviceRequestParameters  deviceRequestParameters)
		{
			var data = service.DeviceService.GetAllDevices(deviceRequestParameters,trackchanges: false);
			Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(data.metadata));
			return Ok(data.devices);
		}

		[HttpGet()]
		public IActionResult GetAlldevicesBasedOnOffice(int regionId, int gateId, int deptId, int officeId)
		{
			var data = service.DeviceService.GetAllRegisteredDevices(regionId, gateId, deptId, officeId, false);
			return Ok(data);
		}
		[HttpGet("{deviceId}", Name = "Getdevice")]
		public IActionResult GetOne(int regionId, int gateId, int deptId, int officeId, int deviceId)
		{
			var data = service.DeviceService.GetById(regionId, gateId, deptId, officeId, deviceId, false);
			return Ok(data);
		}

		[HttpPost]
		public async Task<IActionResult> Create(int regionId, int gateId, int deptId, int officeId, [FromBody] DeviceForCreationDto deviceForCreationDto)
		{

			// we should pass logged in user id here 
			var result = await service.DeviceService.CreateDevice(regionId, gateId, deptId, officeId, deviceForCreationDto, "UserID", false);
			return CreatedAtRoute("Getdevice", new { regionId, gateId, deptId, deviceId = result.Id }, result);
		}
		[HttpDelete("{deviceId}")]
		public async Task<IActionResult> Delete(int regionId, int gateId, int deptId, int officeId, int deviceId)
		{
			// we should pass logged in user id here 
			await service.DeviceService.DeleteDevice(regionId, gateId, deptId, officeId, deviceId, "userID", true);
			return NoContent();
		}
	}
}
