using AutoMapper;
using Minenetred.web.Models;
using Redmine.library.Models;

namespace Minenetred.web.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Project, ProjectDto>();

            CreateMap<Activity, ActivityDto>();

            CreateMap<Issue, IssueDto>();

            CreateMap<TimeEntry, Minenetred.web.Models.TimeEntryDto>();
        }
    }
}