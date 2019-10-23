using Minenetred.web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Minenetred.web.Services
{
    public interface IIssueService
    {
        Task<List<IssueDto>> GetIssuesAsync(int projectId, string email);
    }
}