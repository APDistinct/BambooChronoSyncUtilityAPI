using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using BambooChronoSyncUtility.Service.Models;

namespace BambooChronoSyncUtility.Service.Services
{
    public interface IBambooHrService
    {
        Task<IEnumerable<TimeOffGetResponse>> GetTimeOff(DateOnly start, DateOnly end, IEnumerable<int> ids);
    }
    public partial class BambooHrService : IBambooHrService
    {
        private readonly ILogger<BambooHrService> _logger;
        public BambooHrService(IHttpClientFactory clientFactory, ILogger<BambooHrService> logger) =>
        (_clientFactory, _logger) = (clientFactory, logger);

        public async Task<IEnumerable<TimeOffGetResponse>> GetTimeOff(DateOnly start, DateOnly end, IEnumerable<int> ids)
        {
            List<TimeOffGetResponse> list = new List<TimeOffGetResponse>();
            foreach(var id in ids) 
            {
                var ll = await GetEmployeesTimeOffRequestsHistoryFromBambooHRAPI(id, start, end);
                list.AddRange(ll);
            }

            //ids.ToList().ForEach(async id => {
            //    var ll = await GetEmployeesTimeOffRequestsHistoryFromBambooHRAPI(id, start, end);
            //    list.AddRange( ll);
            //});
            return list; // await Task.FromResult(list);
        }
        //public async Task Synchronize(DateOnly start, DateOnly end, IEnumerable<int> ids)
        //{

        //}
    }
}
