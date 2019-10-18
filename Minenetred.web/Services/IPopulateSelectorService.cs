using Minenetred.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minenetred.web.Services
{
    public interface IPopulateSelectorService
    {
        Task<List<ActivityDto>> GetActivitiesInListAsync(int projectId, string userName);
        Task<List<IssueDto>> GetIssuesInListAsync(int projectId, string username);
    }
}
