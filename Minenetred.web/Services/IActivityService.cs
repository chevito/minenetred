using Minenetred.web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Minenetred.web.Services
{
    public interface IActivityService
    {
        Task<List<ActivityDto>> GetActivitiesAsync(int projectId, string email);
    }
}