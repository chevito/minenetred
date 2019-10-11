using AutoMapper;
using Minenetred.web.Context;
using Minenetred.web.Infrastructure;
using Minenetred.web.Models;
using Minenetred.web.ViewModels;
using Newtonsoft.Json;
using Redmine.library.Models;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Minenetred.web.Services.Implementations
{
    public class TimeEntryService : ITimeEntryService
    {
        private readonly MinenetredContext _context;
        private readonly Redmine.library.Services.ITimeEntryService _timeEntryService;
        private readonly IMapper _mapper;
        private readonly IUsersManagementService _usersManagementService;
        private readonly IProjectService _projectService;

        public TimeEntryService(
            MinenetredContext context,
            Redmine.library.Services.ITimeEntryService timeEntryService,
            IMapper mapper,
            IUsersManagementService usersManagementService,
            IProjectService projectService
            )
        {
            _context = context;
            _timeEntryService = timeEntryService;
            _mapper = mapper;
            _usersManagementService = usersManagementService;
            _projectService = projectService;
        }

        public async Task<float> GetTimeEntryHoursPerDay(int projectId, string date, string user)
        {
            var key = _usersManagementService.GetUserKey(user);
            var redmineId = _context.Users.SingleOrDefault(u=>u.UserName == user).RedmineId;
            var response = await _timeEntryService.GetTimeEntriesAsync(key, redmineId, projectId, date, date);
            var shapedList = _mapper.Map<TimeEntryListResponse, TimeEntryViewModel>(response);
            float totalHours = 0;
            foreach (var entry in shapedList.TimeEntries)
            {
                totalHours += entry.Hours;
            }
            return totalHours;
        }

        public async Task<HttpStatusCode> AddTimeEntryAsync(TimeEntryFormDto entry)
        {
            var entryToMap = new TimeEntryFormContainer()
            {
                TimeEntry = entry,
            };
            var timeEntry = _mapper.Map<TimeEntryFormContainer, TimeEntryDtoContainer>(entryToMap);
            var key = _usersManagementService.GetUserKey(UserPrincipal.Current.EmailAddress);
            return  await _timeEntryService.AddTimeEntryAsync(timeEntry, key);
        }

        private string ConvertDateToStringFormat(DateTime date)
        {
            var day = date.Day.ToString();
            if (date.Day < 10)
            {
                day = "0" + day;
            }
             return (date.Year.ToString() +
                "-" +
                date.Month.ToString()+
                "-"+
                day
                );
        }

        private async Task<List<DateTime>> GetFutureTimeEntriesDates(DateTime today, string apiKey)
        {
            var toReturn = new List<DateTime>();
            today = today.AddDays(1);
            var projects = await _projectService.GetOpenProjectsAsync(apiKey);
            var redimeId = _usersManagementService.getRedmineId(apiKey);
            var formatedDate = ConvertDateToStringFormat(today);
            foreach (var project in projects.Projects)
            {
                var time = await _timeEntryService.GetTimeEntriesAsync(apiKey, redimeId, project.Id, formatedDate);
                if (time.TimeEntries.Count > 0)
                {
                    foreach (var entry in time.TimeEntries)
                    {
                        toReturn.Add(entry.SpentOn);
                    }
                }
            }
            return toReturn;
        }
        private DateTime GetFirstAndLastPeriod(DateTime today, bool isFirstPeriod)
        {
            var periodDay = new DateTime();
            if (today.Day <= 15)
            {
                periodDay = isFirstPeriod? new DateTime(today.Year, today.Month, 1) : new DateTime(today.Year, today.Month, 15);
            }
            else
            {
                periodDay = isFirstPeriod? new DateTime(today.Year, today.Month, 16) : new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));
            }
            var toReturn = periodDay;
            return toReturn;
        }
        public async Task<Dictionary<String, int>> GetUnloggedDaysAsync(int UserId, string authKey)
        {
            var toReturn = new Dictionary<String, int>();
            var today = DateTime.Today;
            var firstPeriodDay = GetFirstAndLastPeriod(today, true);
            var lastPeriodDay = GetFirstAndLastPeriod(today, false);
            var futureTimeEntries = await GetFutureTimeEntriesDates(today, authKey);
            var referenceDate = new DateTime();
            if (futureTimeEntries.Count > 0)
            {
                foreach (var date in futureTimeEntries)
                {
                    if (date < lastPeriodDay)
                    {
                        referenceDate = date;
                        break;
                    }
                }
            }
            else
            {
                referenceDate = today;
            }
            double numberOfDays = (referenceDate - firstPeriodDay).TotalDays;
            for (int i = 0; i < numberOfDays; i++)
            {
                var dateToValidate = firstPeriodDay.AddDays(i);
                float hoursPerDay = 0;
                var entries = await _timeEntryService.GetTimeEntriesAsync(
                    authKey,
                    UserId,
                    fromDate: ConvertDateToStringFormat(dateToValidate),
                    toDate: ConvertDateToStringFormat(dateToValidate));
                foreach (var entry in entries.TimeEntries)
                {
                    if (entry.Activity.Name.Equals("Vacation/PTO/Holiday"))
                    {
                        hoursPerDay = 8;
                        continue;
                    }
                    hoursPerDay += entry.Hours;
                }
                if ((dateToValidate.DayOfWeek.ToString().Equals("Saturday") || dateToValidate.DayOfWeek.ToString().Equals("Sunday")))
                {
                    if (hoursPerDay != 0)
                        toReturn.Add(ConvertDateToStringFormat(dateToValidate), 1);
                }
                else
                {
                    if (hoursPerDay < 8)
                        toReturn.Add(ConvertDateToStringFormat(dateToValidate), 0);
                }
            }
            return toReturn;
        }
    }
}
