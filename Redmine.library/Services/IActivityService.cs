using Redmine.library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Redmine.library.Services
{
    public interface IActivityService
    {
        Task<List<Activity>> GetActivityListResponseAsync(string authKey, int projectId);
    }
}