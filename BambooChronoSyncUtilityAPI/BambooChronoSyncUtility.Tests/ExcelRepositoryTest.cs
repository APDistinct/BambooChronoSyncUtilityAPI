using BambooChronoSyncUtility.Application.Models;
using BambooChronoSyncUtility.Service.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BambooChronoSyncUtility.Tests
{
    public class ExcelRepositoryTest
    {
        private readonly ExcelServiceOption _options = new();
        [Fact]
        public async Task GetUserIdsTest()
        {
            var option = Options.Create(_options);
            option.Value.IDColumnName = "ID";
            option.Value.FileName = "Xlsx\\BambooHR_to_Chrono.xlsx";
            option.Value.EmployeeColumnName = "Employee";
            
            var repository = new ExcelRepository(option);
            var ret = repository.GetUserIds();
            Assert.NotNull(ret);


            //
        }
    }
}
