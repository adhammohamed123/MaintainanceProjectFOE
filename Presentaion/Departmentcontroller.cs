using Service.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Presentaion
{
 //   [Authorize]
    [ApiController]
    [Route("api/Regions/{regionId}/Gates/{gateId}/Departments")]
    public class DepartmentController : ControllerBase
    {
        private readonly IServiceManager service;

        public DepartmentController(IServiceManager service)
        {
            this.service = service;
        }
      

        [HttpGet]
        public IActionResult GetAll(int regionId, int gateId)
        {
            var data = service.DepartmentService.GetAllDepartments(regionId, gateId, false).ToList();
            return Ok(data);
        }
        [HttpGet("{deptId}",Name ="GetDept")]
        public IActionResult GetDepartment(int regionId, int gateId,int deptId)
        {
            var data = service.DepartmentService.GetDept(regionId,gateId,deptId, trakchanages: false);
            return Ok(data);
        }
     //   [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(int regionId, int gateId, [FromBody] string deptName)
        {
            var result = await service.DepartmentService.CreateNewDepartment(regionId, gateId, deptName, trackchanges: false);
            
            return CreatedAtAction(nameof(GetDepartment), new { regionId, gateId, deptId = result.Id }, result);
            //return CreatedAtRoute("GetDept", new { regionId, gateId, deptId = result.Id }, result);
        }
      //  [Authorize(Roles = "Admin")]
        [HttpDelete("{deptId}")]
        public async Task<IActionResult> Delete(int regionId, int gateId, int deptId)
        {
           await  service.DepartmentService.DeleteDepartment(regionId,gateId,deptId,trakchanages: true);
            return NoContent();
        }

    }
}
