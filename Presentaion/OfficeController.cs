using Service.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Presentaion
{
    [Authorize]
    [ApiController]
    [Route("api/Regions/{regionId}/Gates/{gateId}/Departments/{deptId}/Offices")]
    public class OfficeController:ControllerBase
    {
        private readonly IServiceManager service;

        public OfficeController(IServiceManager service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult GetAllOfficesBasedOnDepartment(int regionId, int gateId, int deptId)
        {
            var data = service.OfficeService.GetAll(regionId, gateId, deptId, false);
            return Ok(data);
        }
        [HttpGet("{officeId}", Name ="GetOffice")]
        public IActionResult GetOne(int regionId, int gateId, int deptId, int officeId)
        {
            var data = service.OfficeService.GetOfficeBasedOnId(regionId,gateId,deptId, officeId, false);
            return Ok(data);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(int regionId, int gateId, int deptId,string officeName)
        {
            var result = await service.OfficeService.CreateNewOffice(regionId, gateId, deptId, officeName, false);
            return CreatedAtRoute("GetOffice", new { regionId, gateId, deptId, officeId = result.Id }, result);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{officeId}")]
		public async Task<IActionResult> Delete(int regionId, int gateId, int deptId, int officeId)
		{
			await service.OfficeService.DeleteOffice(regionId, gateId, deptId, officeId, true);
			return NoContent();
		}
	}

}
