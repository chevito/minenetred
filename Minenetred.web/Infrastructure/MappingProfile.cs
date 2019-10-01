using AutoMapper;
using Minenetred.web.Models;
using Minenetred.web.ViewModels;
using Redmine.library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minenetred.web.Infrastructure
{
    public class MappingProfile :Profile 
    {
        public MappingProfile()
        {
            CreateMap<Project, ProjectDto>();
            CreateMap<ProjectListResponse, ProjectsViewModel>()
                .ForMember(dto => dto.Projects, opt => opt.MapFrom(src => src.Projects));

            CreateMap<Activity, ActivityDto>();
            CreateMap<ActivityListResponse, ActivityViewModel>()
                .ForMember(dto => dto.Activities, opt => opt.MapFrom(src => src.Time_Entry_Activities));

            CreateMap<Issue, IssueDto>();
            CreateMap<IssueListResponse, IssueViewModel>();
            
            CreateMap<TimeEntry, Minenetred.web.Models.TimeEntryDto >();
            CreateMap<TimeEntryListResponse, TimeEntryViewModel>()
                .ForMember(dto => dto.TimeEntries, opt => opt.MapFrom(src => src.Time_Entries));

            CreateMap<TimeEntryFormDto, Redmine.library.Models.TimeEntryDto>()
                .ForMember(dto => dto.issue_id, opt => opt.MapFrom(src => src.IssueId))
                .ForMember(dto => dto.spent_on, opt => opt.MapFrom(src => src.SpentOn))
                .ForMember(dto => dto.hours, opt => opt.MapFrom(src => src.Hours))
                .ForMember(dto => dto.activity_id, opt => opt.MapFrom(src => src.ActivityId))
                .ForMember(dto => dto.comments, opt => opt.MapFrom(src => src.Comments));
            CreateMap<TimeEntryFormContainer, TimeEntryDtoContainer>()
                .ForMember(dto => dto.time_entry, opt=> opt.MapFrom(src => src.TimeEntry));
        }
    }
}
