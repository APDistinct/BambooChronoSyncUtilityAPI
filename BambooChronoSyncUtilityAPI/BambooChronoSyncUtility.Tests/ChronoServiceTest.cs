using BambooChronoSyncUtility.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BambooChronoSyncUtility.Tests
{
    public class ChronoServiceTest
    {
        [Fact]
        public void GetMondayTest()
        {
            ChronoService service = new ChronoService(null, null);
            var test1 = DateTime.Parse("2023-05-01");
            var ret = ChronoService.GetMonday(test1.AddDays(3));
            Assert.True(ret.DayOfWeek == DayOfWeek.Monday);
            Assert.Equal(test1, ret);
            
            ret = ChronoService.GetMonday(test1.AddDays(33));
            Assert.True(ret.DayOfWeek == DayOfWeek.Monday);
            Assert.NotEqual(test1, ret);
        }
    }
}
