using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Minenetred.web.Context;
using Minenetred.web.Infrastructure;
using Minenetred.web.Models;
using Minenetred.web.ViewModels;
using Redmine.library.Models;
using Redmine.library.Services;

namespace Minenetred.web.Controllers
{
    public class ActivityController : Controller
    {
        private readonly IActivityService _activityService;
        private readonly MinenetredContext _context;
        private readonly IEncryptionService _encryptionService;
        private readonly IMapper _mapper;
        public ActivityController(
            IActivityService activityService,
            MinenetredContext context,
            IEncryptionService encryptionService,
            IMapper mapper
            )
        {
            _encryptionService = encryptionService;
            _context = context;
            _activityService = activityService;
            _mapper = mapper;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }
        [Route("/Activities/{projectId}")]
        [HttpGet]
        public async Task<ActivityViewModel> IndexAsync(int projectId)
        {
            var userName = UserPrincipal.Current.EmailAddress;
            var encryptedKey = _context.Users.SingleOrDefault(u=> u.UserName == userName).RedmineKey;
            var decryptedKey = _encryptionService.Decrypt(encryptedKey);
            var retrievedData = await _activityService.GetActivityListResponseAsync(decryptedKey, projectId);
            var toRetun = _mapper.Map<ActivityListResponse, ActivityViewModel>(retrievedData);
            return toRetun;

        }
    }
}