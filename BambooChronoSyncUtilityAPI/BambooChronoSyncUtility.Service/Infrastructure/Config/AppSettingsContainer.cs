using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BambooChronoSyncUtility.Service.Infrastructure.Config
{
    public class AppSettingsContainer
    {
        public AppSettingsContainer() { }
        public static Dictionary<DateOnly, double> GetData(DateOnly start,  DateOnly end)
        {
            DateTime startDate = start.ToDateTime(TimeOnly.Parse("0:0:0"));
            var endDate = end.ToDateTime(TimeOnly.Parse("0:0:0"));

            // Генерируем последовательность дат в заданном диапазоне
            var dates = 
                //Enumerable.Range(0, (endDate - startDate).Days + 1)
                Enumerable.Range(0, (endDate.Subtract( startDate)).Days + 1)
                //Enumerable.Range(0, (end.Subtract(start)).Days + 1)
                .Select(n => start.AddDays(n));

            // Создаем словарь с нулевыми значениями
            var dict = dates.ToDictionary(date => date, _ => 0.0);

            // Выводим ключи и значения словаря
            //foreach (var item in dict)
            //{
            //    Console.WriteLine($"{item.Key}: {item.Value}");
            //}Array.Empty<Time_offRequest>()
            return dict ?? new Dictionary<DateOnly, double>();
        }
        public static string GetBasicAuthenticator(string username, string password)
        {
            string arg = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
            return $"Basic {arg}";
        }

    }
}
