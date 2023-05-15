using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BambooChronoSyncUtility.Application.Models
{
    public interface IBambooHrService
    {
        //Task<IEnumerable<TimeOffGetResponse>> GetTimeOff(DateOnly start, DateOnly end, IEnumerable<int> ids);
        Task<IEnumerable<TimeOffModel>> GetTimeOff(DateOnly start, DateOnly end, IEnumerable<int> ids);
    }
}
