using Service.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Core.Entities.ErrorModel;
using Core.Entities;
using Service.DTOs;
using Microsoft.AspNetCore.Http;

namespace Presentaion
{
    [Authorize]
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
            var resposne = new ResponseShape<DepartmentDto>(StatusCodes.Status200OK,"ok",default, data);
            return Ok(resposne);
        }
        [HttpGet("{deptId}",Name ="GetDept")]
        public IActionResult GetDepartment(int regionId, int gateId,int deptId)
        {
            var data = service.DepartmentService.GetDept(regionId,gateId,deptId, trakchanages: false);
            var response = new ResponseShape<DepartmentDto>(StatusCodes.Status200OK, "ok", default, new List<DepartmentDto> { data });
            return Ok(response);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(int regionId, int gateId, [FromBody] string deptName)
        {
            var result = await service.DepartmentService.CreateNewDepartment(regionId, gateId, deptName, trackchanges: false);
            var response = new ResponseShape<DepartmentDto>(StatusCodes.Status201Created, "تم اضافة الاداره بنجاح", default, new List<DepartmentDto>() { result });
            return CreatedAtAction(nameof(GetDepartment), new { regionId, gateId, deptId = result.Id }, response);
            //return CreatedAtRoute("GetDept", new { regionId, gateId, deptId = result.Id }, result);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{deptId}")]
        public async Task<IActionResult> Delete(int regionId, int gateId, int deptId)
        {
           await  service.DepartmentService.DeleteDepartment(regionId,gateId,deptId,trakchanages: true);
            var response = new ResponseShape<DepartmentDto>(StatusCodes.Status200OK, "تم حذف الاداره بنجاح",errors:default,data:null);
            return NoContent();
        }

    }
}
