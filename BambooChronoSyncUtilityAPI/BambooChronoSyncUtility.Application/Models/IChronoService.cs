using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BambooChronoSyncUtility.Application.Models
{
    public interface IChronoService
    {
        Task<Dictionary<TimeDictionary, double>> GetTimeOff(int Id, DateTime start, DateTime end);
        Task<Dictionary<TimeDictionary, string>> GetStatus(int Id, DateTime start, DateTime end);
        Task<Dictionary<int, int>> GetTypes();
        Task<int> SaveDaysOff(IEnumerable<TimeOffModel> timeOffModels);
        Task<IEnumerable<UserIdBambooHR>> GetUserIds();
        Task GetChronoUserIds(IEnumerable<IUserIdChrono> idChronos);
    }
}
