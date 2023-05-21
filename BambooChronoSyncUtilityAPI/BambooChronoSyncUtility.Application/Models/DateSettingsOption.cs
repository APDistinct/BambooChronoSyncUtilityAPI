using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BambooChronoSyncUtility.Application.Models
{
    public class DateSettingsOption
    {
        public int PeriodLengthType {  get; set; }  // Тип периода запроса данных
        public string PeriodType { get; set; } = string.Empty;  // Период запуска синхронизации
        public int DayOfPeriodStart { get; set; }  //  День запуска в периоде
        public string TimeOfDayStart { get; set; } = string.Empty;  //  Время в дне
        public /*DateOnly*/ string DateBegin { get; set; } = string.Empty;  //  Дата начала периода опроса
        public string DateEnd { get; set; } = string.Empty;  //  Дата окончания периода опроса

        private TimeOnly tTimeOfDayStart { get; set; }
        private DateOnly dDateBegin { get; set; }
        public DateOnly dDateEnd { get; set; }
        private bool changed = false;
        public DateOnly PeriodStart => GetStart(); // DateBegin; 
        public DateOnly PeriodEnd => GetEnd(); // DateEnd;
        //public DateSettingsOption()
        //{
            //GetPeriod();
        //}
        private DateOnly GetStart()
        {
            if (!changed) { GetPeriod(); }
            return dDateBegin;
        }
        private DateOnly GetEnd()
        {
            if (!changed) { GetPeriod(); }
            return dDateEnd;
        }
        private void GetPeriod()
        {
            // TODO: make variants from config settings
            DateTime date = DateTime.UtcNow;
            
            switch (PeriodLengthType)
            {
                case 1:
                    dDateBegin = (new DateOnly(date.Year, date.Month, date.Day)).GetMonday();
                    dDateEnd = dDateBegin.AddDays(13);
                    break;
                case 2:
                    dDateBegin = new DateOnly(date.Year, date.Month, 1);
                    int allDayMonth = DateTime.DaysInMonth(date.Year, date.Month);
                    dDateEnd = new DateOnly(date.Year, date.Month, allDayMonth);
                    break;
                case 3:
                    DateOnly.TryParse(DateBegin, out DateOnly db);
                    dDateBegin = db;
                    DateOnly.TryParse(DateEnd, out DateOnly de);
                    dDateEnd = de;
                    break;
            }
            changed = true;
        }
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
    }
    
}
