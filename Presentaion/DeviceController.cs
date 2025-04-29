using Service.Services;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Core.Entities;
using Azure;
using Core.Features;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Presentaion.Attributes;
using Service.DTOs.DeviceDtos;
using Microsoft.AspNetCore.Http;
using Core.Entities.ErrorModel;

namespace Presentaion
{

	[Authorize]
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
		public async Task<IActionResult> GetAll([FromQuery]DeviceRequestParameters  deviceRequestParameters)
		{
			var data = await service.DeviceService.GetAllDevices(deviceRequestParameters,trackchanges: false);
            var response = new
            {
				response=new ResponseShape<DeviceDto>(StatusCodes.Status200OK, "ok", default, data.devices.ToList()),
                // Include the devices in the response body
                // devices = data.devices,
                // Include pagination info in the response body
                // data = data.devices,
                pagination = data.metadata// Include pagination info in the response body
            };
			//HttpContext.Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(data.metadata));
            return Ok(response);
        }

		[HttpGet]
		public async Task<IActionResult> GetAlldevicesBasedOnOffice(int regionId, int gateId, int deptId, int officeId)
		{
			var data = await service.DeviceService.GetAllRegisteredDevices(regionId, gateId, deptId, officeId, false);
            var response = new ResponseShape<DeviceDto>(StatusCodes.Status200OK, "ok", default, data.ToList());
            return Ok(response);
		}
		[HttpGet("{deviceId}", Name = "Getdevice")]
		public async Task<IActionResult> GetOne(int regionId, int gateId, int deptId, int officeId, int deviceId)
		{
			var data = await service.DeviceService.GetById(regionId, gateId, deptId, officeId, deviceId, false);
			var response = new ResponseShape<DeviceDto>(StatusCodes.Status200OK, "ok", default, new List<DeviceDto> { data });
            return Ok(response);
		}

		[HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Create(int regionId, int gateId, int deptId, int officeId, [FromBody] DeviceForCreationDto deviceForCreationDto)
		{
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            // we should pass logged in user id here 
            var result = await service.DeviceService.CreateDevice(regionId, gateId, deptId, officeId, deviceForCreationDto, userId, false);
			var response = new ResponseShape<DeviceDto>(StatusCodes.Status201Created, "تم اضافة الجهاز بنجاح", default, new List<DeviceDto>() { result });
            return CreatedAtAction(nameof(GetOne), new { regionId, gateId, deptId, officeId, deviceId = result.Id }, response);
            //return CreatedAtRoute("Getdevice", new { regionId, gateId, deptId, deviceId = result.Id }, result);
        }
		[Authorize(Roles = "Admin")]
        [HttpDelete("{deviceId}")]
		public async Task<IActionResult> Delete(int regionId, int gateId, int deptId, int officeId, int deviceId)
		{
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            // we should pass logged in user id here 
            await service.DeviceService.DeleteDevice(regionId, gateId, deptId, officeId, deviceId, userId, true);
            var response = new ResponseShape<DeviceDto>(StatusCodes.Status200OK, "تم حذف الجهاز بنجاح", errors: default, data: null);
            return Ok(response);
		}

		[HttpPut]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateDevice(int regionId, int gateId, int deptId, int officeId, DeviceForUpdateDto deviceForUpdateDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            // we should pass logged in user id here 
            await service.DeviceService.UpdateDevice(regionId, gateId, deptId, officeId, deviceForUpdateDto, userId, true);
            var response = new ResponseShape<DeviceDto>(StatusCodes.Status200OK, "تم تحديث الجهاز بنجاح", errors: default, data: null);
            return Ok(response);
        }
    }
}
