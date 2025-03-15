using Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentaion
{
    [Route("api/Devices")] 
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IServiceManager service;

        public DeviceController(IServiceManager service)
        {
            this.service = service;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var data = service.DeviceService.GetAllDevices(false).ToList();
            return Ok(data); 
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var data = service.DeviceService.GetDeviceByID(id, false);
            return Ok(data);
        }


    }
}
