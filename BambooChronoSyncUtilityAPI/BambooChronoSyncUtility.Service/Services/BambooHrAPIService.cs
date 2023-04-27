using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BambooChronoSyncUtility.Service.Models;
using Newtonsoft.Json;

namespace BambooChronoSyncUtility.Service.Services
{
    public interface IBambooHrAPIService
    {
        //Task<TimeOffGetResponse[]> GetEmployeesTimeOffRequestsHistoryFromBambooHRAPI1(int userId, DateOnly start, DateOnly end);
        Task<TimeOffGetResponse[]> GetEmployeesTimeOffRequestsHistoryFromBambooHRAPI(int userId, DateOnly start, DateOnly end);
    }
    public class BambooHrAPIService : IBambooHrAPIService
    {
        private readonly IHttpClientFactory _clientFactory = null!;
        //private const string GetEmployeesUrl = "reports/custom?format=json";
        private const string BamboohrHttpClientFactoryName = "BambooHR_API";
        private static string GetEmployeesTimeOffRequestsHistoryUrl(int userId, string start, string end)
    => $"time_off/requests/?employeeId={userId}&status=approved&start={start}&end={end}";

        public BambooHrAPIService(IHttpClientFactory clientFactory) => _clientFactory = clientFactory;
       
            

        public async Task<TimeOffGetResponse[]> GetEmployeesTimeOffRequestsHistoryFromBambooHRAPI1(int userId, DateOnly start, DateOnly end)
        {
           // var validCheckDate = GetValidDateTime(date);

            var request = new HttpRequestMessage(HttpMethod.Get,
                GetEmployeesTimeOffRequestsHistoryUrl(userId, start: start.ToString("yyy-MM-dd"), end: end.ToString("yyy-MM-dd")));

            //var client = _clientFactory.CreateClient(BamboohrHttpClientFactoryName);


            using HttpClient client = _clientFactory.CreateClient(BamboohrHttpClientFactoryName);

            try
            {
                // Make HTTP GET request
                // Parse JSON response deserialize into Time_offRequest types
                TimeOffGetResponse[]? todos = await client.GetFromJsonAsync<TimeOffGetResponse[]>(                
                    GetEmployeesTimeOffRequestsHistoryUrl(userId, start: start.ToString("yyy-MM-dd"), end: end.ToString("yyy-MM-dd"))
                    /*,new JsonSerializerOptions(JsonSerializerDefaults.Web)*/);

                return todos ?? Array.Empty<TimeOffGetResponse>();
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error getting something fun to say: {Error}", ex);
            }

            return Array.Empty<TimeOffGetResponse>();
        }
        public async Task<TimeOffGetResponse[]> GetEmployeesTimeOffRequestsHistoryFromBambooHRAPI(int userId, DateOnly start, DateOnly end)
        {
            //var request = new HttpRequestMessage(HttpMethod.Get,
            //    GetEmployeesTimeOffRequestsHistoryUrl(userId, start.ToString("yyy-MM-dd"), end.ToString("yyy-MM-dd")));

            using HttpClient client = _clientFactory.CreateClient(BamboohrHttpClientFactoryName);
            var str = GetEmployeesTimeOffRequestsHistoryUrl(userId, start.ToString("yyy-MM-dd"), end.ToString("yyy-MM-dd"));
            
            var response = await client.GetAsync(str);
            //var response = await client.SendAsync(request);
            bool ret = response.IsSuccessStatusCode;
            //if (!response.IsSuccessStatusCode) return null;

            var history = await response.Content.ReadAsStringAsync();

            var jsonArray = JArray.Parse(history);
            TimeOffGetResponse[]? todos = JsonConvert.DeserializeObject<TimeOffGetResponse[]>(history);
            
            return todos ?? Array.Empty<TimeOffGetResponse>();
        }
            //        private void Synchronize(DateOnly start, DateOnly end, IEnumerable<int> ids)
    }
}
