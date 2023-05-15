using BambooChronoSyncUtility.Application.Models;
using BambooChronoSyncUtility.Service.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeOffModel = BambooChronoSyncUtility.Application.Models.TimeOffModel;

namespace BambooChronoSyncUtility.Service.Services
{
    public class BambooHrService : IBambooHrService
    {
        private readonly ILogger<BambooHrServiceOld> _logger;
        private readonly IBambooHrAPIService _bambooService;
        public BambooHrService(IBambooHrAPIService bambooService, ILogger<BambooHrServiceOld> logger) =>
        (_bambooService, _logger) = (bambooService, logger);
        private readonly object locker = new object();

        public async Task<IEnumerable<TimeOffModel>> GetTimeOff(DateOnly start, DateOnly end, IEnumerable<int> ids)
        {
            //int locker = 1;
            //Dictionary<TimeDictionary, double> time  = new();
            var models = new List<TimeOffModel>();
            var tasks = new List<Task>();
            tasks.AddRange(ids.Select(async id =>
            {
                var model = new TimeOffModel() { UserId = id };
                var ll = await _bambooService.GetEmployeesTimeOffRequestsHistoryFromBambooHRAPI(id, start, end);
                foreach (var togr in ll)
                {
                    if (togr != null)
                    {
                        model.Add(togr.Convert());
                    }
                }
                lock (locker)
                {
                    models.Add(model);
                }
            }));
            await Task.WhenAll(tasks);
            return models;
        }
    }
}
