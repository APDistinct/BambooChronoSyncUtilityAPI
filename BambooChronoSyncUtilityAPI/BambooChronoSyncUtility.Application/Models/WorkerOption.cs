using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BambooChronoSyncUtility.Application.Models
{
    public interface IWorkerOption
    {
        void GetDate(out DateTime date, out TimeSpan period);
    }
    public class WorkerOption : IWorkerOption
    {
        private readonly string Day = "Day";
        private readonly string Week = "Week";
        private readonly string Month = "Month";
        public string PeriodType { get; set; } = string.Empty;  // Период запуска синхронизации
        public int DayOfPeriodStart { get; set; }  //  День запуска в периоде
        public TimeOnly TimeOfDayStart { get; set; }  //  Время в дне
        private readonly IConfiguration _config;
        public WorkerOption(IConfiguration config)
        {
            _config = config;
        }
        public void GetDate(out DateTime date, out TimeSpan period)
        {
            DateTime today = DateTime.Today;
            PeriodType = _config["DateSettings: PeriodType"] ?? "Day";
            var _ = int.TryParse(_config["DateSettings: DayOfPeriodStart"] ?? "1", out int day);
            DayOfPeriodStart = day;
            TimeOfDayStart = TimeOnly.Parse( _config["DateSettings: TimeOfDayStart"] ?? "10:00");
            //  Потом доработать под разне периоды. 
            //  В данный момент пока только "Day"
            date = new DateTime(today.Year, today.Month, today.Day, TimeOfDayStart.Hour, TimeOfDayStart.Minute, TimeOfDayStart.Second);
            DateTimeOffset dateOffset = date.ToUniversalTime();
            if(date > dateOffset) 
            {
                date = date.AddDays(1);
            }
            period = TimeSpan.FromDays(1);     
        }
    }
}
