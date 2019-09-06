using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redmine.library;
using Minenetred.web.Models;
using Redmine.library.Models;
using Minenetred.web.ViewModels;
using AutoMapper;
using System.Net.Http;
using Redmine.library.Services;

namespace Minenetred.web.Controllers
{
    
    public class ProjectsController : Controller
    {
        private IProjectService _service;
        private readonly IMapper _mapper;
        public ProjectsController(IMapper mapper, IProjectService service)
        {
            _mapper = mapper;
            _service = service;
            
        }

        [Route("Projects")]
        [HttpGet]
        public async Task<ActionResult<ProjectsViewModel>> ProjectsAsync()
        {
            var apiContent = await _service.GetProjectsAsync("6b57002a51deedcb04866c0775c6cb7ee35e8613");
            var projectsList = _mapper.Map<ProjectListResponse, ProjectsViewModel>(apiContent);
            var shapedList = new ProjectsViewModel()
            {
                Projects = new List<ProjectDto>(),
            };
            foreach (var project in projectsList.Projects)
            {
                if (project.status == 1)
                    shapedList.Projects.Add(project);

            }
            return shapedList;
        }
    }
}
