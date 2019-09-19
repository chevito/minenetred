using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Minenetred.web.Context;
using Minenetred.web.Infrastructure;
using Minenetred.web.Services;
using Minenetred.web.ViewModels;
using Redmine.library.Models;

namespace Minenetred.web.Controllers
{
    public class IssueController : Controller
    {
        private readonly IIssueService _issueService;

        public IssueController(
            IIssueService issueService
            )
        {
            _issueService = issueService;
        }

        [HttpGet]
        [Route("/Issues/{projectId}")]
        public async Task<IssueViewModel> GetIssuesAsync(int projectId)
        {
            var toReturn = await _issueService.GetIssuesAsync(projectId);
            return toReturn;
        }
    }
}