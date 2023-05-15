using BambooChronoSyncUtility.Service.Models;
using BambooChronoSyncUtility.Service.Services;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BambooChronoSyncUtility.Tests
{
    public class BambooHrServiceTest
    {
        private List<TimeOffGetResponse> offGetResponses = new List<TimeOffGetResponse>();

        [Fact]
        public async Task SynchronizeTest()
        {
            // arrange
            var apiservice = new Mock<IBambooHrAPIService>();
            var ids = new List<int>() { 1, 2, 3, 4 };
            InitList(ids);
      
            DateOnly start = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(-3);
            DateOnly end = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(3);
            apiservice.Setup(z => z.GetEmployeesTimeOffRequestsHistoryFromBambooHRAPI(It.IsAny<int>(), It.IsAny<DateOnly>(), It.IsAny<DateOnly>()))                
            .ReturnsAsync<int, DateOnly, DateOnly, IBambooHrAPIService, TimeOffGetResponse[]>((userId, start, end) => GetWIList(userId, start, end));
            
            var service = new BambooHrServiceOld(apiservice.Object, null);

            // act
            var result = await service.Synchronize(start, end, ids);

            // assert
            Assert.NotNull(result);
        }
        private TimeOffGetResponse[] GetWIList(int userId, DateOnly start, DateOnly end)
        {
            var list = offGetResponses.Where(x => x.EmployeeId ==  userId).ToArray();
         
            return list;
        }
        private void InitList(IEnumerable< int> userIds)
        {
            offGetResponses.Clear();
            foreach( int userId in userIds) 
            {
                var timeOff = new TimeOffGetResponse() 
                { 
                    EmployeeId = userId,
                    Dates = new Dictionary<DateOnly, double>() ,
                    Amount = new TimeOffGetResponse.RequestAmount() { Unit = "hours", },
                    Type = new TimeOffGetResponse.RequestType() { Id = 1},


                };
                timeOff.Dates.Add(DateOnly.FromDateTime(DateTime.UtcNow).AddDays(-2), 3);
                offGetResponses.Add(timeOff);
            }
            
        }
    }
}
