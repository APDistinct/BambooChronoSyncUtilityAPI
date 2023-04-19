using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BambooChronoSyncUtility.Service.Models
{    
    public interface ITimeOffGetRequest
    {
        public IEnumerable<int> IdList { get; set; } 
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
    public class TimeOffGetRequest
    {
        public IEnumerable<int> IdList { get; set; } = new List<int>();
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
