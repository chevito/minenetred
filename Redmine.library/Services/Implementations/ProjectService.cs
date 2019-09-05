using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Redmine.library.Models;
using Redmine.library.Services;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Redmine.library
{
    // todo: rename to Project Service and IProjectService interface
    public class ProjectsService : IProjectService
    {
        private HttpClient _client { get; set; }
        private string _authKey { get; set; }

        public ProjectsService(HttpClient client, string authKey)
        {
            _client = client;
            _authKey = authKey; 
            //Please refer to https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-2.2

        }


        public async Task<ProjectsContent> GetProjectsAsync()
        {
  
            try
            {
                var toReturn = "";
                var requestUri = Constants.projects + Constants.json+ "?key=" +_authKey;
                HttpResponseMessage response = await _client.GetAsync(requestUri);
                if (response.IsSuccessStatusCode)
                {
                    toReturn = await response.Content.ReadAsStringAsync();
                    ProjectsContent projectsContent = JsonConvert.DeserializeObject<ProjectsContent>(toReturn);
                    return projectsContent;
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
