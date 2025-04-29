using Core.Entities;
using Core.Entities.ErrorModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Services;
using System.ComponentModel.DataAnnotations;

namespace Presentaion
{
    [ApiController]
    [Route("api/Regions")]
    [Authorize]
    public class RegionController : ControllerBase
    {
        protected  readonly IServiceManager service;

        public RegionController(IServiceManager service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAlls()
        {
            var data = await service.RegionService.GetAllRegisteredRegion(trackchanges: false);
            var response = new ResponseShape<RegionDto>(StatusCode:StatusCodes.Status200OK, "ok", default, data.ToList());
            return Ok(response);
        }

        [HttpGet("{regionId}", Name ="GetRegionBasedOnId")]
        public async Task<IActionResult> GetRegion(int regionId)
        {
           var data= await service.RegionService.GetRegionByID(regionId, trackchanges: false);
            var response = new ResponseShape<RegionDto>(StatusCodes.Status200OK, "ok", default, new List<RegionDto> { data });
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody][MaxLength(50,ErrorMessage ="اسم القطاع يجب ان لا يتعدي ال 50 حرف")] string name)
        {
           var newRegion=  await service.RegionService.CreateNewRegionAsync(name);
            var response = new ResponseShape<RegionDto>(StatusCodes.Status201Created, "تم اضافة المنطقه بنجاح", default, new List<RegionDto>() { newRegion });
            return CreatedAtAction(nameof(GetRegion), new { regionId = newRegion.Id },response);
            //return CreatedAtRoute("GetRegionBasedOnId", new { regionId = newRegion.Id }, newRegion);
        }
        [Authorize(Roles= "Admin")]
        [HttpDelete("{regionId}")]
		public async Task<IActionResult> Delete(int regionId)
		{
			await service.RegionService.DeleteRegionAsync(regionId);
		    var response = new ResponseShape<RegionDto>(StatusCodes.Status200OK, "تم حذف المنطقه بنجاح", errors: default, data: null);
            return Ok(response);
		}
        //  [HttpPut]
        // public async Task<IActionResult> Update(int regionId, [FromBody] RegionDto regionDto)
        // {
        //	await service.RegionService.UpdateRegion(regionId, regionDto);
        //	return NoContent();
        //}
        //  [HttpPatch]
		//public async Task<IActionResult> Update(int regionId, [FromBody] JsonPatchDocument<RegionDto> regionDto)
		//{
		//	var result = service.RegionService.GetRegionForPartialUpdate(regionId, trackchanges: true);
		//	foreach (var op in regionDto.Operations)
		//	{
		//		if (op.OperationType==OperationType.Replace && !op.path.Equals("/name",StringComparison.OrdinalIgnoreCase))
		//		{
		//			return BadRequest("Only name Updating Available");
		//		}
		//	}
		//	regionDto.ApplyTo(result.regionDto);
		//	await service.RegionService.SavePatchChanges(result.region, result.regionDto);
		//	return NoContent();
		//}
        

	}
}
