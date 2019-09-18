using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Minenetred.web.Context;
using Minenetred.web.Infrastructure;
using Redmine.library.Models;
using Redmine.library.Services;

namespace Minenetred.web.Controllers
{
    public class IssueController : Controller
    {
        private readonly MinenetredContext _context;
        private readonly IEncryptionService _encryptionService;
        private readonly IIssueService _issueService;
        public IssueController(
            MinenetredContext context,
            IEncryptionService encryptionService,
            IIssueService issueService
            )
        {
            _context = context;
            _encryptionService = encryptionService;
            _issueService = issueService;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }
        [HttpGet]
        [Route("/issues/{userId}/{projectId}")]
        public Task<IssueListResponse> Index(int userId, int projectId)
        {
            var userEmail = UserPrincipal.Current.EmailAddress;
            var encryptedKey = _context.Users.SingleOrDefault(u => u.UserName == userEmail).RedmineKey;
            var decryptedKey = _encryptionService.Decrypt(encryptedKey);
            return _issueService.GetIssuesAsync(decryptedKey, userId, projectId);

        }
    }
}