using AutoMapper;
using Minenetred.web.Context;
using Minenetred.web.Models;
using Redmine.library.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minenetred.web.Services.Implementations
{
    public class IssueService : IIssueService
    {
        private readonly MinenetredContext _context;
        private readonly Redmine.library.Services.IIssueService _issueService;
        private readonly IMapper _mapper;
        private IUsersManagementService _usersManagementService;

        public IssueService(
            MinenetredContext context,
            Redmine.library.Services.IIssueService issueService,
            IMapper mapper,
            IUsersManagementService usersManagementService
            )
        {
            _context = context;
            _issueService = issueService;
            _mapper = mapper;
            _usersManagementService = usersManagementService;
        }

        public async Task<List<IssueDto>> GetIssuesAsync(int projectId, string email)
        {
            var userEmail = email;
            var user = _context.Users.SingleOrDefault(u => u.UserName == userEmail);
            var decryptedKey = _usersManagementService.GetUserKey(userEmail);
            var response = await _issueService.GetIssuesAsync(decryptedKey, user.RedmineId, projectId);
            var toReturn = _mapper.Map<List<Issue>, List<IssueDto>>(response);
            return toReturn;
        }
    }
}