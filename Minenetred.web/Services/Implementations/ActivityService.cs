using AutoMapper;
using Minenetred.web.Models;
using Redmine.library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Minenetred.web.Services.Implementations
{
    public class ActivityService : IActivityService
    {
        private readonly IMapper _mapper;
        private readonly IUsersManagementService _usersManagementService;
        private readonly Redmine.library.Services.IActivityService _activityService;

        public ActivityService(
            IMapper mapper,
            IUsersManagementService usersManagementService,
            Redmine.library.Services.IActivityService activityService
            )
        {
            _mapper = mapper;
            _usersManagementService = usersManagementService;
            _activityService = activityService;
        }

        public async Task<List<ActivityDto>> GetActivitiesAsync(int projectId, string email)
        {
            var userName = email;
            var decryptedKey = _usersManagementService.GetUserKey(userName);
            var retrievedData = await _activityService.GetActivityListResponseAsync(decryptedKey, projectId);
            var toRetun = _mapper.Map<List<Activity>, List<ActivityDto>>(retrievedData);
            return toRetun;
        }
    }
}