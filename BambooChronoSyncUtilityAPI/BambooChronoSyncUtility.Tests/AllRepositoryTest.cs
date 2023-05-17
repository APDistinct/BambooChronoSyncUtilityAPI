using BambooChronoSyncUtility.Application.Models;
using BambooChronoSyncUtility.DAL.EF.Model;
using BambooChronoSyncUtility.Service.Repositories;
using BambooChronoSyncUtility.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BambooChronoSyncUtility.Tests
{
    public class AllRepositoryTest
    {
        private readonly ExcelServiceOption _options = new();
        private readonly IChronoRepository _chronoRepository;

        private readonly string connectionString = "Server=APD\\APDSERVER;User Id=txchrono;Password=Tx12cHrono34;Database=TimeTrack";
        private readonly ChronoContext context;

        public AllRepositoryTest()
        {
            context = new ChronoContext(GetOptions(connectionString));
            _chronoRepository = new ChronoRepository(context);
        }
        private static DbContextOptions<ChronoContext> GetOptions(string connectionString)
        {
            return new DbContextOptionsBuilder<ChronoContext>().UseSqlServer(connectionString).Options;
        }

        [Fact]
        //  Переделать под виртуальную БД и с проверкой на корректность нахождения в ней.
        public async Task GetUserIdsTest()
        {
            var option = CreateOptions("Xlsx\\BambooHR_to_Chrono.xlsx"); // Options.Create(_options);
            //option.Value.IDColumnName = "ID";
            //option.Value.FileName = "Xlsx\\BambooHR_to_Chrono.xlsx";
            //option.Value.EmployeeColumnName = "Employee";

            var excelRepository = new ExcelRepository(option);
            var list = (await excelRepository.GetUserIds()).ToList() ;
            Assert.NotNull(list);
            Assert.True(list.Count() > 0);
            list.ForEach(x => Assert.True(!string.IsNullOrEmpty(x.UserIdBamboo) && int.TryParse(x.UserIdBamboo, out int _)));
            
            List<UserIdName> userIdNameList = new();
            foreach (var x in list)
            {
                userIdNameList.Add(new UserIdName() { UserIdBamboo = int.Parse(x.UserIdBamboo), UserName = x.UserName, UserIdChrono = -1 });
            }
            await _chronoRepository.GetChronoUserIds(userIdNameList);

            //
        }
        private IOptions<ExcelServiceOption> CreateOptions(string fileName)
        {
            var option = Options.Create(_options);
            option.Value.IDColumnName = "ID";
            option.Value.FileName = fileName;
            option.Value.EmployeeColumnName = "Employee";
            return option;
        }
    }
}
