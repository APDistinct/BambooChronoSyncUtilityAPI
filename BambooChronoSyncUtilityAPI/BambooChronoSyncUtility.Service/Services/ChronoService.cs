using BambooChronoSyncUtility.Service.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BambooChronoSyncUtility.Service.Services
{
    public interface IChronoService
    {

    }
    public class ChronoService : IChronoService
    {
        private readonly IChronoRepository _repository;
        public ChronoService(IChronoRepository repository) => _repository = repository;

        private readonly string[] States = { "Added", "Declined" };
        public static DateTime GetMonday(DateTime date)
        {
            //var ret = DateOnly.FromDateTime( date.AddDays(DayOfWeek.Monday - date.DayOfWeek ));
            var ret = date.AddDays(DayOfWeek.Monday - date.DayOfWeek);
            return ret;
        }
    }
}
