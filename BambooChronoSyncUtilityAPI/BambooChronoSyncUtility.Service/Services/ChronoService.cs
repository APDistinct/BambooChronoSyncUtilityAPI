using BambooChronoSyncUtility.Application.Models;
using BambooChronoSyncUtility.Service.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BambooChronoSyncUtility.Service.Services
{   
    public class ChronoService : IChronoService
    {
        private readonly IChronoRepository _chronoRepository;
        private readonly IExcelRepository _excelRepository;
        public ChronoService(IChronoRepository repository, IExcelRepository excelRepository) 
            => (_chronoRepository, _excelRepository) = (repository, excelRepository);

        //private readonly string[] States = { "Added", "Declined" };
        public static DateTime GetMonday(DateTime date)
        {
            //var ret = DateOnly.FromDateTime( date.AddDays(DayOfWeek.Monday - date.DayOfWeek ));
            var ret = date.AddDays(DayOfWeek.Monday - date.DayOfWeek);
            return ret;
        }
        public async Task<IEnumerable<UserIdBambooHR>> GetUserIds()
        {
            return await _excelRepository.GetUserIds();
        }
        public async Task GetChronoUserIds(IEnumerable<IUserIdChrono> idChronos)
        {
            await _chronoRepository.GetChronoUserIds(idChronos);
            return;
        }
        public async Task<Dictionary<TimeDictionary, double>> GetTimeOff(int Id, DateTime start, DateTime end)
        {
            return await _chronoRepository.GetTimeOff(Id, start, end);
        }

        public async Task<Dictionary<TimeDictionary, string>> GetStatus(int Id, DateTime start, DateTime end)
        {
            return await _chronoRepository.GetStatus(Id, start, end);
        }

        public async Task<Dictionary<int, int>> GetTypes()
        {
            // Сделать таблицу в БД. Добывать оттуда.
            var dic = new Dictionary<int, int>()
            { {1, -4 }, {2, -2 }, {11, -2 }, {3, -5 }, {19, -3 } };
            return await Task.FromResult(dic);
        }

        public async Task<int> SaveDaysOff(IEnumerable<TimeOffModel> timeOffModels)
        {
            int count = 0;
            foreach(var timeOffModel in timeOffModels)
            {
                count += await _chronoRepository.SaveDaysOff(timeOffModel);
            }
            return count;
        }
    }
}
