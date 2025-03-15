using Service.Services;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using System.Threading.Tasks;

namespace Presentaion
{
    [ApiController]
    [Route("api/Regions")]
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

        [HttpGet("{id}",Name ="GetRegionBasedOnId")]
        public IActionResult GetRegion(int id)
        {
           var data=  service.RegionService.GetRegionByID(id, trackchanges: false);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] string name)
        {
           var newRegion=  await service.RegionService.CreateNewRegionAsync(name);
            return CreatedAtRoute("GetRegionBasedOnId", new { id = newRegion.Id }, newRegion);
        }

    }
}
