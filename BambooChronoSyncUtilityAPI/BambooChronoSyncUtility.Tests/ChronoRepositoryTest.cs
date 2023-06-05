using BambooChronoSyncUtility.DAL.EF.Model;
using BambooChronoSyncUtility.Service.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace BambooChronoSyncUtility.Tests
{
    public class ChronoRepositoryTest
    {
        private readonly IChronoRepository _repository;
        //private readonly ILogger<ChronoRepository> _logger = new FakeLogger<ChronoRepository>();
        private readonly Mock<ILogger<ChronoRepository>> _mockLogger = new Mock<ILogger<ChronoRepository>>();

        private readonly string connectionString = "Server=APD\\APDSERVER;User Id=txchrono;Password=Tx12cHrono34;Database=TimeTrack";
        private readonly ChronoContext context;
        
        public ChronoRepositoryTest()
        {
            context = new ChronoContext(GetOptions(connectionString ));
            _repository = new ChronoRepository(context, /*_logger*/_mockLogger.Object);
        }
        private static DbContextOptions<ChronoContext> GetOptions(string connectionString)
        {
            return new DbContextOptionsBuilder<ChronoContext>().UseSqlServer(connectionString).Options;
        }

        [Fact]
        public async Task GetTimeOffTest()
        {
            int id = 3;
            DateTime start = new DateTime(2008, 5, 1);
            DateTime end = new DateTime(2008, 5, 31);

            var ret = await _repository.GetTimeOff(id, start, end);
            Assert.NotNull(ret);

        }
        [Fact]
        public async Task GetStatusTest()
        {
            int id = 3;
            DateTime start = new DateTime(2009, 8, 1);
            DateTime end = new DateTime(2009, 8, 31);

            var ret = await _repository.GetStatus(id, start, end);
            Assert.NotNull(ret);

        }
    }
}
