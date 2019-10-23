using Minenetred.web.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Minenetred.web.Services
{
    public interface ITimeEntryService
    {
        Task<float> GetTimeEntryHoursPerDay(int projectId, string date, string user);

        Task<HttpStatusCode> AddTimeEntryAsync(TimeEntryFormDto entry);

        Task<Dictionary<String, int>> GetUnloggedDaysAsync(int UserId, string authKey, DateTime today);
    }
}