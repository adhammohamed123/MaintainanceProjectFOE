using Service.Services;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Presentaion
{
    [Authorize]
    [ApiController]
    [Route("api/Regions/{regionId}/Gates")]
    public class GateController : ControllerBase
    {
        private readonly IServiceManager service;

        public GateController(IServiceManager service)
        {
            this.service = service;
        }

        [HttpGet()]
        public IActionResult GetAll(int regionId)
        {
            var data = service.GateService.GetAllGates(regionId, false).ToList();

            return Ok(data);
        }
        //[HttpGet("/api/gates")]
        //public IActionResult GetAll()
        //{
        //    var data = service.GateService.GetAllGatesInGeneral(false);

        //    return Ok(data);
        //}
        [HttpGet("{gateId}", Name ="GetGateBasedOnRegionId")]
        public IActionResult GetOne(int regionId, int gateId)
        {
           var data=  service.GateService.GetSpecificGate(regionId, gateId, false);
            return Ok(data);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateNewGate(int regionId,[FromBody]string gateName)
        {
            var gate = await service.GateService.CreateNewGateInRegion(regionId, gateName,false);

            return CreatedAtAction(nameof(GetOne), new { regionId, gateId = gate.Id }, gate);
            // return CreatedAtRoute("GetGateBasedOnRegionId", new {regionId, gateId = gate.Id}, gate);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{gateId}")]
		public async Task<IActionResult> DeleteGate(int regionId, int gateId)
		{
			await service.GateService.DeleteGateAsync(regionId, gateId);
			return NoContent();
		}
	}

}
