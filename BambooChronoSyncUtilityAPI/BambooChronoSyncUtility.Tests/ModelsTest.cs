using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using BambooChronoSyncUtility.Service.Models;

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
            var request1 = JsonConvert.DeserializeObject<Time_offRequest[]>(json);
            Assert.NotNull(request1);
            Assert.NotEmpty(request1);
            Assert.Single(request1);
            var request2 = JsonConvert.DeserializeObject<IEnumerable< Time_offRequest>>(Read("time_offrequests2"))?.ToArray();
            Assert.NotNull(request2);
            Assert.NotEmpty(request2);
            Assert.Single(request2);
            Assert.Equal(request1[0].Name, request2[0].Name);
            Assert.Equal(request1[0].EmployeeId, request2[0].EmployeeId);
            Assert.NotEqual(request1[0].Status.Status, request2[0].Status.Status);
            foreach(var d in request1[0].Dates)
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
    }
}