using Newtonsoft.Json;
using Redmine.library.Core;
using Redmine.library.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Redmine.library.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly HttpClient _client;
        public UserService(HttpClient client)
        {
            _client = client;
        }

        public async Task<UserResponse> GetCurrentUserAsync(string authKey)
        {
            if (string.IsNullOrEmpty(authKey))
                throw new ArgumentNullException(nameof(authKey));

            var toReturn = "";
            var requestUri = Constants.CurrentUser + Constants.Json + "?key=" + authKey;
            HttpResponseMessage response = await _client.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                toReturn = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<UserResponse>(toReturn);
                return user;
            }
            else
            {
                var errorMsj = await response.Content.ReadAsStringAsync();
                throw new Exception(errorMsj);
            }
        }
    }
}
