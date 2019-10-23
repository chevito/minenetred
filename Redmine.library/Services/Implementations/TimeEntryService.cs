using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Redmine.library.Core;
using Redmine.library.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Redmine.library.Services.Implementations
{
    public class TimeEntryService : ITimeEntryService
    {
        private readonly HttpClient _client;

        public TimeEntryService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<TimeEntry>> GetTimeEntriesAsync(string authKey, int userId, int projectId = 0, string fromDate = null, string toDate = null)
        {
            if (string.IsNullOrEmpty(authKey))
                throw new ArgumentNullException(nameof(authKey));

            var toReturn = "";
            var requestUri = UriHelper.HandleTimeEntriesUri(authKey, userId, projectId, fromDate, toDate);
            HttpResponseMessage response = await _client.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                toReturn = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(toReturn);
                var timeEntries = jsonObject["time_entries"].ToString();
                var timeEntryListResponse = JsonConvert.DeserializeObject<List<TimeEntry>>(timeEntries, SerializerHelper.Settings);
                return timeEntryListResponse;
            }
            else
            {
                var errormsg = await response.Content.ReadAsStringAsync();
                throw new Exception(errormsg);
            }
        }

        public async Task<HttpStatusCode> AddTimeEntryAsync(string authKey, int issueId, string spentOn, double hours, int activityId, string comments)
        {
            if (string.IsNullOrEmpty(comments))
            {
                throw new ArgumentNullException(nameof(comments));
            }
            if (string.IsNullOrEmpty(authKey))
            {
                throw new ArgumentNullException(nameof(authKey));
            }
            var requestUri = Constants.TimeEntries +
                Constants.Json +
                "?key=" + authKey;
            var FormatedJson = TimeEntryPostHelper.GetTimeEntryJsonFormat(authKey, issueId, spentOn, hours, activityId, comments);
            var httpContent = new StringContent(FormatedJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(requestUri, httpContent);
            return response.StatusCode;
        }
    }
}