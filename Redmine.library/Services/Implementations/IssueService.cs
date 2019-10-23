using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Redmine.library.Core;
using Redmine.library.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Redmine.library.Services.Implementations
{
    public class IssueService : IIssueService
    {
        private readonly HttpClient _client;

        public IssueService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<Issue>> GetIssuesAsync(string authKey, int assignedToId, int projectId)
        {
            if (string.IsNullOrEmpty(authKey))
                throw new ArgumentNullException(nameof(authKey));

            var toReturn = "";
            var requestUri = UriHelper.Issues(authKey, assignedToId, projectId);
            HttpResponseMessage response = await _client.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                toReturn = await response.Content.ReadAsStringAsync();
                var parsedObject = JObject.Parse(toReturn);
                var issuesArray = parsedObject["issues"].ToString();
                var issueResponse = JsonConvert.DeserializeObject<List<Issue>>(issuesArray);
                return issueResponse;
            }
            else
            {
                var errorMsg = await response.Content.ReadAsStringAsync();
                throw new Exception(errorMsg);
            }
        }
    }
}