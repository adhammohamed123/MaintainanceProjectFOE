using Service.Services;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using System.Threading.Tasks;

namespace Presentaion
{
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
        [HttpGet("{id}",Name ="GetGateBasedOnRegionId")]
        public IActionResult GetOne(int regionId, int id)
        {
           var data=  service.GateService.GetSpecificGate(regionId, id, false);
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewGate(int regionId,[FromBody]string gateName)
        {
            var gate = await service.GateService.CreateNewGateInRegion(regionId, gateName,false);
            return CreatedAtRoute("GetGateBasedOnRegionId", new {regionId,id=gate.Id}, gate);
        }
    }

}
