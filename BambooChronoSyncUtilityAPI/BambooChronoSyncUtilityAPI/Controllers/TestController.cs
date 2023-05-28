using BambooChronoSyncUtility.Application.Models;
using BambooChronoSyncUtility.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BambooChronoSyncUtilityAPI.Controllers
{
    public class TestController : AbstractController
    {
        private readonly IChronoService _chronoService;
        public TestController(IChronoService chronoService,  ILogger<TestController> logger) : base(logger)
        {
            _chronoService = chronoService;
        }
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [HttpPost("getdbtest")]
        public async Task<IActionResult> GetTimeOff()
        {
            var result = await _chronoService.TestDB();
            //if (result.User == null)
            //{
            //    return Error(HttpStatusCode.NotFound, $"The user '{getRequest.UserId}' not found.");
            //}

            return Ok(result);
            //return Ok(new { TimeReport = result });
        }

    }
}
