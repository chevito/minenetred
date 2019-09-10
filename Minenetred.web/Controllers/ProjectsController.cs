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
using Microsoft.AspNetCore.Authorization;
using Minenetred.web.Context;

namespace Minenetred.web.Controllers
{

    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;
        private readonly MinenetredContext _context;
        public ProjectsController(IMapper mapper, IProjectService service, MinenetredContext context)
        {
            _mapper = mapper;
            _projectService = service;
            _context = context;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }

        [Authorize]
        [Route("/")]
        [HttpGet]
        public async Task<ActionResult<ProjectsViewModel>> GetProjectsAsync()
        {
            var userName = HttpContext.User.Identity.Name;
            var user = _context.Users.SingleOrDefault(c => c.UserName == userName);
            var userKey = user.Key;
            if (userKey == null)
            {
                return RedirectToAction("AddKey");
            }

            var apiContent = await _projectService.GetProjectsAsync(userKey);
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
            return View(shapedList);
        }

        public IActionResult AddKey()
        {
            var userName = HttpContext.User.Identity.Name;
            var user = _context.Users.SingleOrDefault(c => c.UserName == userName);
            ViewBag.key = user.Key;
            return View();
        } 

        [HttpPost]
        public IActionResult AddKey(string key)
        {
            var userName = HttpContext.User.Identity.Name;
            var user = _context.Users.SingleOrDefault(c => c.UserName == userName);
            user.Key = key;
            _context.Users.Update(user);
            _context.SaveChanges();
            return RedirectToAction("GetProjectsAsync");
        }
    }
}
