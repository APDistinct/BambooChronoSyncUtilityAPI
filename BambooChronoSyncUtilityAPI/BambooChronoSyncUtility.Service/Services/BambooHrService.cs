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
        Task<IEnumerable<TimeOffModel>> Synchronize(DateOnly start, DateOnly end, IEnumerable<int> ids);
    }
    public partial class BambooHrService : IBambooHrService
    {
        private readonly ILogger<BambooHrService> _logger;
        private readonly IBambooHrAPIService _bambooService;
        public BambooHrService(IBambooHrAPIService bambooService, ILogger<BambooHrService> logger) =>
        (_bambooService, _logger) = (bambooService, logger);
        private readonly object locker = new object();

        public async Task<IEnumerable<TimeOffGetResponse>> GetTimeOff(DateOnly start, DateOnly end, IEnumerable<int> ids)
        {
            List<TimeOffGetResponse> list = new List<TimeOffGetResponse>();
            foreach(var id in ids) 
            {
                var ll = await _bambooService.GetEmployeesTimeOffRequestsHistoryFromBambooHRAPI(id, start, end);
                list.AddRange(ll);
            }

            //ids.ToList().ForEach(async id => {
            //    var ll = await GetEmployeesTimeOffRequestsHistoryFromBambooHRAPI(id, start, end);
            //    list.AddRange( ll);
            //});
            return list; // await Task.FromResult(list);
        }

        public async Task<IEnumerable<TimeOffModel>> Synchronize(DateOnly start, DateOnly end, IEnumerable<int> ids)
        {
            //int locker = 1;
            Dictionary<TimeDictionary, double> time  = new Dictionary<TimeDictionary, double>();
            var models = new List< TimeOffModel>();
            var tasks = new List<Task>();
            tasks.AddRange(ids.Select(async id =>
            {
                var model = new TimeOffModel() { UserId = id};
                var ll = await _bambooService.GetEmployeesTimeOffRequestsHistoryFromBambooHRAPI(id, start, end);
                foreach(var togr in ll)
                {
                    if(togr != null)
                    {
                        model.Add(togr.Convert());
                    }
                }
                lock(locker) 
                {
                    models.Add(model);
                }
            }));
            await Task.WhenAll(tasks);
            return models;
        }
    }
}
