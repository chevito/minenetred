using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Redmine.library.Core;
using Redmine.library.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Redmine.library.Services.Implementations
{
    public class ActivityService : IActivityService
    {
        private readonly HttpClient _client;

        public ActivityService(HttpClient client)
        {
            _client = client;
        }
        public async Task<ActivityListResponse> GetActivityListResponseAsync(string authKey, int projectId)
        {

            if (string.IsNullOrEmpty(authKey))
                throw new ArgumentNullException(nameof(authKey));

            var toReturn = "";
            var requestUri = UriHelper.Activities(projectId, authKey);
            HttpResponseMessage response = await _client.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                toReturn = await response.Content.ReadAsStringAsync();
                var activityListResponse = JsonConvert.DeserializeObject<ActivityListResponse>(toReturn, SerializerHelper.Settings);
                return activityListResponse;
            }
            else
            {
                var errorMsg = await response.Content.ReadAsStringAsync();
                throw new Exception(errorMsg);
            }
        }
    }
}
