using BambooChronoSyncUtility.Application.Models;
using Microsoft.Extensions.Options;
using System;
using System.Net.Sockets;

namespace BambooChronoSyncUtility.Application
{
    public interface ISynchronizer
    {
        Task<int> Synchronize();
    }
    public class Synchronizer : ISynchronizer
    {
        private readonly IBambooHrService _bambooService;
        private readonly IChronoService _chronoService;
        private readonly DateSettingsOption _dateSettingsOption;
        private readonly object locker = new();
        private Dictionary<int, int> types = null!;
        //private 
        public Synchronizer(IBambooHrService hrService, IChronoService chronoService, IOptions<DateSettingsOption> dateSettingsOption)
        {
            (_bambooService, _chronoService) = (hrService, chronoService);
            _dateSettingsOption = dateSettingsOption.Value;
        }

        public async Task<int> Synchronize()
        {
            DateOnly start, end;
            // Get period
            start = _dateSettingsOption.PeriodStart;
            end = _dateSettingsOption.PeriodEnd;
            //GetPeriod(ref start, ref end, /*for test*/ 3);
            // Get list of id's and names
            List<UserIdName> userIdNameList = await GetEmploees();
            // Add Chrono's userId's
            await GetEmploeesChrono(userIdNameList);
            //  Id's for BambooHR
            var ids = userIdNameList.Select(u => u.UserIdBamboo).ToList();
            // Get data from BambooHR
            var bambooData = await _bambooService.GetTimeOff(start, end, ids);
            if (bambooData == null || !bambooData.Any())
            {
                return await Task.FromResult(0);
            }
            //  TimeOff types mapping from BambooHR to Chrono
            types = await MakeTypeDictionary();

            // Analyze and get for import into Chrono
            var models = new List<TimeOffModel>();

            //var tasks = new List<Task>();
            //tasks.AddRange(ids.Select(async id =>
            foreach (var id in ids)
            //ids.ForEach(async id =>
            {
                var model = new TimeOffModel() { UserId = userIdNameList.First(u => u.UserIdBamboo.Equals(id)).UserIdChrono };
                // Get data from Chrono
                // Info about time state
                var status = await _chronoService.GetStatus(id, start.ToDateTime(TimeOnly.Parse("0:0:0")), end.ToDateTime(TimeOnly.Parse("0:0:0")));

                // Info about work time
                var bTime = bambooData.Where(d => d.UserId == id).Select(x => x.Time).FirstOrDefault();

                if (bTime != null && bTime.Any())
                {
                    //  Change type from Bamboo to Chrono
                    var cTime = ChangeType(bTime);
                    Dictionary<TimeDictionary, double> time = new();
                    //var time = ll.Where(l => state[l.Key] == )
                    foreach (var togr in cTime)
                    {
                        if (togr.Value > 0)
                        {
                            if (status.ContainsKey(togr.Key))
                            {
                                if (Settings.StatesAll.Contains(status[togr.Key]))
                                {
                                    bool ret = time.TryGetValue(togr.Key, out double val);
                                    time[togr.Key] = togr.Value + val;
                                }
                            }
                        }
                    }
                    model.Time = time;
                    if (time.Any())
                    {
                        lock (locker)
                        {
                            models.Add(model);
                        }
                    }
                }
            } ;
            //await Task.WhenAll(tasks);
            
            // Save to Chrono
            await _chronoService.SaveDaysOff(models);
            //return await Task.FromResult(models.Count);
            return models.Count;
        }
        private async Task<List<UserIdName>> GetEmploees()
        {
            List<UserIdName> userIdNameList = new List<UserIdName>();
            var list = (await _chronoService.GetUserIds()).ToList();
            foreach( var x in list)
            {
                if(!IsInt(x.UserIdBamboo))
                {
                    // Error!                    
                    continue;
                }
                if(string.IsNullOrEmpty( x.UserName))
                {
                    // Error!
                    continue;
                }
                userIdNameList.Add(new UserIdName() { UserIdBamboo = int.Parse(x.UserIdBamboo), UserName = x.UserName, UserIdChrono = -1 }); 
            }
            return userIdNameList;
        }
        private async Task GetEmploeesChrono(List<UserIdName> userIdNameList)
        {
            await _chronoService.GetChronoUserIds(userIdNameList);
            userIdNameList.ForEach(u => 
            {
                if(u.UserIdChrono < 0)
                {
                    // Error!

                }
                ;
            });
            userIdNameList = userIdNameList.Where(u => u.UserIdChrono > 0).ToList();
        }
        private Dictionary<TimeDictionary, double> ChangeType(Dictionary<TimeDictionary, double> bTime)
        {
            Dictionary<TimeDictionary, double> cTime = new Dictionary<TimeDictionary, double>();
            foreach (var togr in bTime) 
            {
                var newKey = new TimeDictionary() {Type = types[togr.Key.Type], Date = togr.Key.Date };

                bool ret = cTime.TryGetValue(togr.Key, out double val);
                cTime[newKey] = togr.Value + val;
            }
            return cTime;
        }
        private async Task<Dictionary<int, int>> MakeTypeDictionary()
        {
            return  await _chronoService.GetTypes();
            
        }
        //private void GetPeriod(ref DateOnly start,  ref DateOnly end, int kind = 0)
        //{
        //    // TODO: make variants from config settings
        //    switch (kind)
        //    {
        //        case 1 : GetPeriod1(ref start, ref end);
        //            break;
        //        case 2:
        //            GetPeriod2(ref start, ref end);
        //            break;
        //        case 3:
        //            GetPeriod3(ref start, ref end);
        //            break;
        //    }
        //}
        //private void GetPeriod1(ref DateOnly start, ref DateOnly end)
        //{
        //    DateTime date = DateTime.UtcNow;
        //    start = (new DateOnly(date.Year, date.Month, date.Day)).GetMonday();
        //    end = start.AddDays(13);
        //}
        //private void GetPeriod2(ref DateOnly start, ref DateOnly end)
        //{
        //    // First time - only current month
        //    DateTime date = DateTime.UtcNow;
        //    start = new DateOnly(date.Year, date.Month, 1);
        //    int allDayMonth = DateTime.DaysInMonth(date.Year, date.Month);
        //    end = new DateOnly(date.Year, date.Month, allDayMonth);
        //}
        //private void GetPeriod3(ref DateOnly start, ref DateOnly end)
        //{
        //    // First time - only for test. Made it for getting from config
        //    DateTime date = DateTime.UtcNow;
        //    start = new DateOnly(2023, 4, 1);
        //    int allDayMonth = DateTime.DaysInMonth(date.Year, date.Month);
        //    end = new DateOnly(date.Year, date.Month, allDayMonth);
        //}
        private static bool IsInt(string? value)
        {
            return !string.IsNullOrEmpty(value) && int.TryParse(value, out int _);
        }

        private static bool IsNumeric(string? value)
        {
            return !string.IsNullOrEmpty(value) && decimal.TryParse(value, out decimal _);
        }
    }
}