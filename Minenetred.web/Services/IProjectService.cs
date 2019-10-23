using Minenetred.web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Minenetred.web.Services
{
    public interface IProjectService
    {
        Task<List<ProjectDto>> GetOpenProjectsAsync(string apiKey);
    }
}