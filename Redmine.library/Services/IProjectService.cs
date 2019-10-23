using Redmine.library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Redmine.library.Services
{
    public interface IProjectService
    {
        Task<List<Project>> GetProjectsAsync(string authKey);
    }
}