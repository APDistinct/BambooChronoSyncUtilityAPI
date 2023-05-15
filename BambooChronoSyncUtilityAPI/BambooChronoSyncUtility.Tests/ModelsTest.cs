using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using BambooChronoSyncUtility.Service.Models;
using System.Reflection;
using System.Security.Cryptography;
using BambooChronoSyncUtility.Application.Models;

namespace BambooChronoSyncUtility.Tests
{
    public class ModelsTest
    {
        private string Read(string fn)
        {
            string json = System.IO.File.ReadAllText("Json\\" + fn + ".json");
            return json;
        }
        [Fact]
        public void Time_offRequestTest()
        {
            string json = Read("time_offrequests1"); //.ToLower();
            var job = JsonConvert.DeserializeObject<JArray>(json);
            var request1 = JsonConvert.DeserializeObject<TimeOffGetResponse[]>(json);
            Assert.NotNull(request1);
            Assert.NotEmpty(request1);
            Assert.Single(request1);
            var request2 = JsonConvert.DeserializeObject<IEnumerable<TimeOffGetResponse>>(Read("time_offrequests2"))?.ToArray();
            Assert.NotNull(request2);
            Assert.NotEmpty(request2);
            Assert.Single(request2);
            Assert.Equal(request1[0].Name, request2[0].Name);
            Assert.Equal(request1[0].EmployeeId, request2[0].EmployeeId);
            Assert.NotEqual(request1[0].Status.Status, request2[0].Status.Status);
            foreach (var d in request1[0].Dates)
            {
                Assert.Equal(d.Value, request2[0].Dates[d.Key]);
            }
            //var ret = guids.Select(x => x.ToUserId).ToList();
            //var gh = guids[0];
            ////Guid.TryParse(guids[0].ToUserId, out Guid gg);
            //Guid[] users = { Guid.NewGuid(), Guid.NewGuid() };
            //string s = JsonConvert.SerializeObject(users);
            //var usersret = JsonConvert.DeserializeObject<IEnumerable<Guid>>(s);

        }
        [Fact]
        public void Time_offRequest_ConvertTest()
        {
            string json = Read("time_offrequests1"); //.ToLower();
            var job = JsonConvert.DeserializeObject<JArray>(json);
            var request1 = JsonConvert.DeserializeObject<TimeOffGetResponse[]>(json);
            Assert.NotNull(request1);
            var offModel = request1[0].Convert();
            Assert.NotNull(offModel);
            Assert.Equal(offModel.Time.Count, request1[0].Dates.Count);

        }
        
        [Fact]
        public void TimeOffModelTest()
        {
            TimeOffModel offModel = new TimeOffModel();
            //offModel.Time = new List<Dictionary<TimeDictionary, double>>();
            //var listTimeOffModel = new List<TimeOffTimeModel>();
            //for (int i = 0; i < 3; i++)
            //{
            //    for (int j = 0; j < 3; j++)
            //    {
            //        var timeDictionary = new TimeDictionary() { Date = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(i), Type = j };
            //        listTimeOffModel.Add(new TimeOffTimeModel() { Key = timeDictionary, Value = j + i });
            //    }
            //}
            //Assert.NotEmpty(listTimeOffModel);
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    var timeDictionary = new TimeDictionary() { Date = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(i), Type = j };
                    //var dic = new Dictionary<TimeDictionary, double>
                    //{
                    //    { timeDictionary, 1.0 }
                    //};
                    offModel.Time.Add(timeDictionary, 1.0);
                }
            }
            var ttimeDictionary = new TimeDictionary() { Date = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(1), Type = 1 };
            bool ret = offModel.Time.TryGetValue(ttimeDictionary, out double val);
            Assert.True(ret);
            Assert.Equal(1, val);
            ttimeDictionary.Type = 3;
            ret = offModel.Time.TryGetValue(ttimeDictionary, out double val1);
            offModel.Time[ttimeDictionary] = 3 + val1;
            Assert.Equal(5, offModel.Time.Count);
            Assert.Equal(3, offModel.Time[ttimeDictionary]);
            offModel.Time[ttimeDictionary] += 3;
            Assert.Equal(6, offModel.Time[ttimeDictionary]);
        }
        [Fact]
        public void TimeOffModelAddTest()
        {
            string json = Read("time_offrequests3"); //.ToLower();
            var job = JsonConvert.DeserializeObject<JArray>(json);
            var request1 = JsonConvert.DeserializeObject<TimeOffGetResponse[]>(json);
            Assert.NotNull(request1);
            var offModels = request1.Select(x => x.Convert()).ToList();
            Assert.NotNull(offModels);
            var offModel = new TimeOffModel() { UserId = offModels[0].UserId };
            offModels.ForEach(model => offModel.Add(model));
            Assert.True(offModel.Time.Count > 0);
            //Assert.Equal(offModel.Time.Count, request1[0].Dates.Count);
        }
    }
}