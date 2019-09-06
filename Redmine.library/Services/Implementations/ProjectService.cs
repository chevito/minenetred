using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Redmine.library.Models;
using Redmine.library.Services;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Redmine.library.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private HttpClient _client;

        public ProjectService(HttpClient client)
        {
            _client = client;
        }


        public async Task<ProjectListResponse> GetProjectsAsync(string authKey)
        {
  
            try
            {
                var toReturn = "";
                var requestUri = Constants.projects + Constants.json+ "?key=" + authKey;
                HttpResponseMessage response = await _client.GetAsync(requestUri);
                if (response.IsSuccessStatusCode)
                {
                    toReturn = await response.Content.ReadAsStringAsync();
                    ProjectListResponse projectListResponse = JsonConvert.DeserializeObject<ProjectListResponse>(toReturn);
                    return projectListResponse;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message); 
            }
            

           


        }

    }
}
