using Redmine.library.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Redmine.library.Services
{
    public interface ITimeEntryService
    {
        Task<List<TimeEntry>> GetTimeEntriesAsync(
            string authKey,
            int userId,
            int projectId = 0,
            string fromDate = null,
            string toDate = null);

        Task<HttpStatusCode> AddTimeEntryAsync(TimeEntryDtoContainer entry, string authKey);
    }
}