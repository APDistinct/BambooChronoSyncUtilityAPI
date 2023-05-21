using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BambooChronoSyncUtility.Application.Models
{
    public static class Extensions
    {
        public static DateTime GetMonday(this DateTime date)
        {
            var ret = date.AddDays(DayOfWeek.Monday - date.DayOfWeek);
            return ret;
        }
        public static DateOnly GetMonday(this DateOnly date)
        {
            var ret = date.AddDays(DayOfWeek.Monday - date.DayOfWeek);
            return ret;
        }
    }
}
