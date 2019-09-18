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
using Minenetred.web.ViewModels;
using Redmine.library.Models;
using Redmine.library.Services;

namespace Minenetred.web.Controllers
{
    public class IssueController : Controller
    {
        private readonly MinenetredContext _context;
        private readonly IEncryptionService _encryptionService;
        private readonly IIssueService _issueService;
        private readonly IMapper _mapper;
        public IssueController(
            MinenetredContext context,
            IEncryptionService encryptionService,
            IIssueService issueService,
            IMapper mapper
            )
        {
            _context = context;
            _encryptionService = encryptionService;
            _issueService = issueService;
            _mapper = mapper;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }
        [HttpGet]
        [Route("/Issues/{projectId}")]
        public async Task<IssueViewModel> GetIssuesAsync(int projectId)
        {
            var userEmail = UserPrincipal.Current.EmailAddress;
            var user = _context.Users.SingleOrDefault(u => u.UserName == userEmail);
            var decryptedKey = _encryptionService.Decrypt(user.RedmineKey);
            var response = await  _issueService.GetIssuesAsync(decryptedKey, user.RedmineId, projectId);
            var toReturn = _mapper.Map<IssueListResponse, IssueViewModel>(response);
            return toReturn;
        }
    }
}