﻿using Service.Services;
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
		public IActionResult GetAll([FromQuery]DeviceRequestParameters  deviceRequestParameters)
		{
			var data = service.DeviceService.GetAllDevices(deviceRequestParameters,trackchanges: false);
            var response = new
            {
                data = data.devices,
                pagination = data.metadata// Include pagination info in the response body
            };
			//HttpContext.Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(data.metadata));
            return Ok(response);
        }

		[HttpGet]
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
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Create(int regionId, int gateId, int deptId, int officeId, [FromBody] DeviceForCreationDto deviceForCreationDto)
		{
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            // we should pass logged in user id here 
            var result = await service.DeviceService.CreateDevice(regionId, gateId, deptId, officeId, deviceForCreationDto, userId, false);
			return CreatedAtAction(nameof(GetOne), new { regionId, gateId, deptId, officeId, deviceId = result.Id }, result);
            //return CreatedAtRoute("Getdevice", new { regionId, gateId, deptId, deviceId = result.Id }, result);
        }
		[Authorize(Roles = "Admin")]
        [HttpDelete("{deviceId}")]
		public async Task<IActionResult> Delete(int regionId, int gateId, int deptId, int officeId, int deviceId)
		{
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            // we should pass logged in user id here 
            await service.DeviceService.DeleteDevice(regionId, gateId, deptId, officeId, deviceId, userId, true);
			return NoContent();
		}

		[HttpPut]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateDevice(int regionId, int gateId, int deptId, int officeId, DeviceForUpdateDto deviceForUpdateDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            // we should pass logged in user id here 
            await service.DeviceService.UpdateDevice(regionId, gateId, deptId, officeId, deviceForUpdateDto, userId, true);
            return NoContent();
        }
    }
}
