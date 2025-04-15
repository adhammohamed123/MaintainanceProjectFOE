using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Services;

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
        public IActionResult GetAlls()
        {
            var data = service.RegionService.GetAllRegisteredRegion(trackchanges: false).ToList();
            return Ok(data);
        }

        [HttpGet("{regionId}", Name ="GetRegionBasedOnId")]
        public IActionResult GetRegion(int regionId)
        {
           var data=  service.RegionService.GetRegionByID(regionId, trackchanges: false);
            return Ok(data);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] string name)
        {
           var newRegion=  await service.RegionService.CreateNewRegionAsync(name);
            return CreatedAtAction(nameof(GetRegion), new { regionId = newRegion.Id }, newRegion);
            //return CreatedAtRoute("GetRegionBasedOnId", new { regionId = newRegion.Id }, newRegion);
        }
        [Authorize(Roles= "Admin")]
        [HttpDelete("{regionId}")]
		public async Task<IActionResult> Delete(int regionId)
		{
			await service.RegionService.DeleteRegionAsync(regionId);
			return NoContent();
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
