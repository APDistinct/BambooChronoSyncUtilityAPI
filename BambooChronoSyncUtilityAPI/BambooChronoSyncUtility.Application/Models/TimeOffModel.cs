﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BambooChronoSyncUtility.Application.Models
{
    public static class TimeOffModelExtension
    {
        public static TimeOffModel Add(this TimeOffModel offModel, TimeOffModel addModel)
        {
            if (offModel.UserId == addModel.UserId)
            {
                foreach (var t in addModel.Time)
                {
                    bool ret = offModel.Time.TryGetValue(t.Key, out double val);
                    offModel.Time[t.Key] = t.Value + val;
                }
            }
            return offModel;
        }
    }
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy), ItemNullValueHandling = NullValueHandling.Ignore)]
    public struct TimeDictionary
    {
        public DateOnly Date { get; set; }
        public int Type { get; set; }
    }
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy), ItemNullValueHandling = NullValueHandling.Ignore)]
    public class TimeOffModel
    {
        //public ICollection<TimeOffTimeModel> Time { get; set; } = new List<TimeOffTimeModel>();
        [JsonProperty]
        public Dictionary<TimeDictionary, double> Time { get; set; } = new Dictionary<TimeDictionary, double>();
        public int UserId { get; set; }
    }
}
