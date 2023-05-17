using BambooChronoSyncUtility.Application;
using BambooChronoSyncUtility.Service.Models;
using BambooChronoSyncUtility.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BambooChronoSyncUtilityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SynchronizerController : AbstractController
    {
        private readonly ISynchronizer _synchronizer;
        public SynchronizerController(ISynchronizer synchronizer, ILogger<SynchronizerController> logger) : base(logger)
        {
            _synchronizer = synchronizer;
        }
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(int))]
        [HttpGet("synchronize")]
        public async Task<IActionResult> GetTimeOff()
        {
            var result = await _synchronizer.Synchronize();
            
            return Ok(result);
        }
    }
}
