using Service.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Presentaion
{
    [ApiController]
    [Route("api/Regions/{regionId}/Gates/{gateId}/Departments/{deptId}/Offices")]
    public class OfficeController:ControllerBase
    {
        private readonly IServiceManager service;

        public OfficeController(IServiceManager service)
        {
            this.service = service;
        }



        [HttpGet()]
        public IActionResult GetAllOfficesBasedOnDepartment(int regionId, int gateId, int deptId)
        {
            var data = service.OfficeService.GetAll(regionId, gateId, deptId, false);
            return Ok(data);
        }
        [HttpGet("{id}",Name ="GetOffice")]
        public IActionResult GetOne(int regionId, int gateId, int deptId, int id)
        {
            var data = service.OfficeService.GetOfficeBasedOnId(regionId,gateId,deptId, id, false);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int regionId, int gateId, int deptId,string officeName)
        {
            var result = await service.OfficeService.CreateNewOffice(regionId, gateId, deptId, officeName, false);
            return CreatedAtRoute("GetOffice", new { regionId, gateId, deptId, id = result.Id }, result);
        }
    }

}
