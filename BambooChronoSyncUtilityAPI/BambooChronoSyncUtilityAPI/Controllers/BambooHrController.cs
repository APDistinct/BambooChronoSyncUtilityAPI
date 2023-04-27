using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net;
using Swashbuckle.AspNetCore.Annotations;
using BambooChronoSyncUtility.Service.Services;
using BambooChronoSyncUtility.Service.Models;

namespace BambooChronoSyncUtilityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BambooHrController : AbstractController
    {
        private readonly IBambooHrService _service;
        public BambooHrController(IBambooHrService service, ILogger<BambooHrController> logger) : base(logger)
        {
            _service = service;
        }


        //[SwaggerResponse(StatusCodes.Status200OK, Type = typeof(UserBaseInfo))]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(TimeOffGetResponse))]
        //[HttpPost("gettimereport/userId={userId}&dateFrom={dateFrom}&dayCount={dayCount}")]
        [HttpPost("gettimeoff")]
        //public async Task<IActionResult> GetTimeReport([FromRoute] Guid userId, [FromRoute] DateTime dateFrom, [FromRoute] int dayCount)        
        public async Task<IActionResult> GetTimeOff([FromBody] TimeOffGetRequest getRequest)
        {
            var result = await _service.GetTimeOff(getRequest.StartDate, getRequest.EndDate, getRequest.IdList);
            //if (result.User == null)
            //{
            //    return Error(HttpStatusCode.NotFound, $"The user '{getRequest.UserId}' not found.");
            //}
           
            return Ok(result);
            //return Ok(new { TimeReport = result });
        }
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<TimeOffModel>))]
        //[HttpPost("gettimereport/userId={userId}&dateFrom={dateFrom}&dayCount={dayCount}")]
        [HttpPost("gettimeoffmodel")]
        //public async Task<IActionResult> GetTimeReport([FromRoute] Guid userId, [FromRoute] DateTime dateFrom, [FromRoute] int dayCount)        
        public async Task<IActionResult> GetTimeOffModel([FromBody] TimeOffGetRequest getRequest)
        {
            var result = await _service.Synchronize(getRequest.StartDate, getRequest.EndDate, getRequest.IdList);
            //if (result.User == null)
            //{
            //    return Error(HttpStatusCode.NotFound, $"The user '{getRequest.UserId}' not found.");
            //}

            return Ok(result);
            //return Ok(new { TimeReport = result });
        }
    }
}
