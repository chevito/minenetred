using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
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

        [Route("/Entries/{projectId}/{date}")]
        public async Task<TimeEntryViewModel> GetTimeEntriesAsync(int projectId, string date)
        {
            var userEmail = UserPrincipal.Current.EmailAddress;
            var encryptedKey = _context.Users.SingleOrDefault(u=>u.UserName == userEmail).RedmineKey;
            var decryptedKey = _encrytionService.Decrypt(encryptedKey);
            var redmineId = _context.Users.SingleOrDefault(c=>c.UserName==UserPrincipal.Current.EmailAddress).RedmineId;
            var response = await _timeEntryService.GetTimeEntriesAsync(decryptedKey, redmineId, projectId, date);
            var toReturn = _mapper.Map<TimeEntryListResponse, TimeEntryViewModel>(response);
            return toReturn;
        }
    }
}