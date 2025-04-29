using Service.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Service.DTOs;
using Core.Entities.ErrorModel;
using System.ComponentModel.DataAnnotations;

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
        public async Task<IActionResult> GetAllOfficesBasedOnDepartment(int regionId, int gateId, int deptId)
        {
            var data = await service.OfficeService.GetAll(regionId, gateId, deptId, false);
            var response = new ResponseShape<OfficeDto>(StatusCodes.Status200OK, "ok", default, data.ToList());
            return Ok(response);
        }
        [HttpGet("{officeId}", Name ="GetOffice")]
        public async Task<IActionResult> GetOne(int regionId, int gateId, int deptId, int officeId)
        {
            var data = await service.OfficeService.GetOfficeBasedOnId(regionId,gateId,deptId, officeId, false);
            var response = new ResponseShape<OfficeDto>(StatusCodes.Status200OK, "ok", default, new List<OfficeDto> { data });
            return Ok(response);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(int regionId, int gateId, int deptId, [MaxLength(50, ErrorMessage = "اسم المكتب يجب ان لا يتعدي ال 50 حرف")] string officeName)
        {
            var result = await service.OfficeService.CreateNewOffice(regionId, gateId, deptId, officeName, false);
           var response = new ResponseShape<OfficeDto>(StatusCodes.Status201Created, "تم اضافة المكتب بنجاح", default, new List<OfficeDto>() { result });
            return CreatedAtAction(nameof(GetOne), new { regionId, gateId, deptId, officeId = result.Id }, response);
            // return CreatedAtRoute("GetOffice", new { regionId, gateId, deptId, officeId = result.Id }, result);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{officeId}")]
		public async Task<IActionResult> Delete(int regionId, int gateId, int deptId, int officeId)
		{
			await service.OfficeService.DeleteOffice(regionId, gateId, deptId, officeId, true);
			var response = new ResponseShape<OfficeDto>(StatusCodes.Status200OK, "تم حذف المكتب بنجاح", errors: default, data: null);
            return Ok(response);
		}
	}

}
