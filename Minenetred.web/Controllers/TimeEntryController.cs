using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Minenetred.web.Context;
using Minenetred.web.Infrastructure;
using Redmine.library.Services;

namespace Minenetred.web.Controllers
{
    public class TimeEntryController : Controller
    {
        private readonly MinenetredContext _context;
        private readonly ITimeEntryService _timeEntryService;
        private readonly IEncryptionService _encrytionService;
        private readonly IMapper _mapper;

        public TimeEntryController(
            MinenetredContext context,
            ITimeEntryService timeEntryService,
            IEncryptionService encryptionService,
            IMapper mapper
            )
        {
            _context = context;
            _timeEntryService = timeEntryService;
            _encrytionService = encryptionService;
            _mapper = mapper;
        }

        [Route("/Entries/{userId}/")]
        public IActionResult GetTimeEntriesAsync(int userId, int user, int projectId, string date)
        {
            var userEmail = UserPrincipal.Current.EmailAddress;
            var encryptedKey = _context.Users.SingleOrDefault(u=>u.UserName == userEmail).RedmineKey;
            var decryptedKey = _encrytionService.Decrypt(encryptedKey);
            _timeEntryService.GetTimeEntriesAsync(decryptedKey, userId, projectId, date);
            
            return View();
        }
    }
}