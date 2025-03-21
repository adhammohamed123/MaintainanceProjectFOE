using Service.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Presentaion
{
    [ApiController]
    [Route("api/Regions/{regionId}/Gates/{gateId}/Departments")]
    public class Departmentcontroller : ControllerBase
    {
        private readonly IServiceManager service;

        public Departmentcontroller(IServiceManager service)
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
        [HttpPost]
        public async Task<IActionResult> Create(int regionId, int gateId, [FromBody] string deptName)
        {
            var result = await service.DepartmentService.CreateNewDepartment(regionId, gateId, deptName, trackchanges: false);
            return CreatedAtRoute("GetDept", new { regionId, gateId, deptId = result.Id }, result);
        }
        [HttpDelete("{deptId}")]
        public async Task<IActionResult> Delete(int regionId, int gateId, int deptId)
        {
           await  service.DepartmentService.DeleteDepartment(regionId,gateId,deptId,trakchanages: true);
            return NoContent();
        }

    }
}
