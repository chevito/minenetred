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
    public class ProjectService : IProjectService
    {
        private readonly HttpClient _client;

        public ProjectService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<Project>> GetProjectsAsync(string authKey)
        {
            if (authKey == null || authKey.Equals(""))
                throw new ArgumentNullException(nameof(authKey));

            var toReturn = "";
            var requestUri = UriHelper.Projects(authKey);
            HttpResponseMessage response = await _client.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                toReturn = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(toReturn);
                var projects = jsonObject["projects"].ToString();
                var projectListResponse = JsonConvert.DeserializeObject<List<Project>>(projects, SerializerHelper.Settings);
                return projectListResponse;
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception(errorMessage);
            }
        }
    }
}