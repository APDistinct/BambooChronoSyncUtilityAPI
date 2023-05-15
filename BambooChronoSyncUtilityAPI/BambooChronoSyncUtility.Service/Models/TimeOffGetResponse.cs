using BambooChronoSyncUtility.Application.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BambooChronoSyncUtility.Service.Models
{
    public static class TimeOffGetResponseExtension
    {
        public static TimeOffModel Convert(this TimeOffGetResponse offResponse)
        {
            TimeOffModel offModel = new TimeOffModel
            {
                UserId = offResponse.EmployeeId
            };
            int coeff = 1;
            int type = offResponse.Type.Id;
            if(offResponse.Amount.Unit == "days") coeff = 8;
            foreach(var d in offResponse.Dates)
            {
                var td = new TimeDictionary() { Date = d.Key, Type = type };
                offModel.Time.Add(td, d.Value * coeff);
            }
            return offModel;
        }
    }

    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy), ItemNullValueHandling = NullValueHandling.Ignore)]

    public class TimeOffGetResponse
    {
        public class RequestActions
        {
            public bool View { get; set; }

            public bool Edit { get; set; }
            public bool Cancel { get; set; }
            public bool Approve { get; set; }
            public bool Deny { get; set; }
            public bool Bypass { get; set; }
        }
        public class RequestAmount
        {
            public string Unit { get; set; }
            public double Amount { get; set; }
        }
        public class RequestType
        {
            public int Id { get; set; }
            [JsonProperty]
            public string? Name { get; set; }
            [JsonProperty]
            public string Icon { get; set; }
        }
        public class RequestStatus
        {
            public string LastChanged { get; set; }
            public int LastChangedByUserId { get; set; }
            public string Status { get; set; }
        }

        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        public string? Name { get; set; }
        [JsonProperty]
        public DateOnly /*DateTime*/ Start { get; set; }
        [JsonProperty]
        public DateOnly /*DateTime*/ End { get; set; }
        [JsonProperty]
        public DateTime Created { get; set; }
        [JsonProperty]
        public int EmployeeId { get; set; } = 0;
        [JsonProperty]
        //public IEnumerable< string?>? Notes { get; set; }
        public RequestStatus Status { get; set; }
        public RequestType Type { get; set; }
        public RequestAmount Amount { get; set; }
        public RequestActions Actions { get; set; }
        public Dictionary</*string*/DateOnly, double> Dates { get; set; }

        

        //public List<EmployeesTimeOff> Employees { get; set; }

        //{
        //"id": "8668",
        //"employeeId": "941",
        //"status": {
        //    "lastChanged": "2023-04-10",
        //    "lastChangedByUserId": "3233",
        //    "status": "requested"
        //},
        //"name": "Andrei Dokuchaev",
        //"start": "2023-04-03",
        //"end": "2023-04-04",
        //"created": "2023-04-10",
        //"type": {
        //    "id": "3",
        //    "name": "Day Off/ Unpaid Vacation",
        //    "icon": "happy-calendar"
        //},
        //"amount": {
        //    "unit": "hours",
        //    "amount": "12"
        //},
        //"actions": {
        //    "view": true,
        //    "edit": true,
        //    "cancel": true,
        //    "approve": true,
        //    "deny": true,
        //    "bypass": true
        //},
        //"dates": {
        //    "2023-04-03": "8",
        //    "2023-04-04": "4"
        //},
        //"notes": {}
        //}
    }
}
