using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Minenetred.web.Context;
using Minenetred.web.Infrastructure;
using Redmine.library.Models;/////////////////
using Redmine.library.Services;

namespace Minenetred.web.Controllers
{
    public class ActivityController : Controller
    {
        private readonly IActivityService _activityService;
        private readonly MinenetredContext _context;
        private readonly IEncryptionService _encryptionService;
        public ActivityController(
            IActivityService activityService,
            MinenetredContext context,
            IEncryptionService encryptionService
            )
        {
            _encryptionService = encryptionService;
            _context = context;
            _activityService = activityService;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }
        [Route("/Activities/{projectId}")]
        [HttpGet]
        public async Task<ActivityListResponse> IndexAsync(int projectId)
        {
            var userName = UserPrincipal.Current.EmailAddress;
            var encryptedKey = _context.Users.SingleOrDefault(u=> u.UserName == userName).RedmineKey;
            var decryptedKey = _encryptionService.Decrypt(encryptedKey);
            var listToReturn = await _activityService.GetActivityListResponseAsync(decryptedKey, projectId);
            return listToReturn;
            
        }
    }
}