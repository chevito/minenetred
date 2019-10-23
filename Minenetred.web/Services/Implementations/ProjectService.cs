using AutoMapper;
using Minenetred.web.Models;
using Redmine.library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Minenetred.web.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly Redmine.library.Services.IProjectService _projectService;
        private readonly IMapper _mapper;

        public ProjectService(
            Redmine.library.Services.IProjectService projectService,
            IMapper mapper
            )
        {
            _projectService = projectService;
            _mapper = mapper;
        }

        public async Task<List<ProjectDto>> GetOpenProjectsAsync(string apiKey)
        {
            var response = await _projectService.GetProjectsAsync(apiKey);
            var projectList = _mapper.Map<List<Project>, List<ProjectDto>>(response);
            var shapedList = new List<ProjectDto>();

            foreach (var project in projectList)
            {
                if (project.Status == 1)
                {
                    shapedList.Add(project);
                }
            }
            return shapedList;
        }
    }
}