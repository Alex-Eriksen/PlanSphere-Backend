using AutoMapper;
using Domain.Entities;
using Domain.Entities.EmbeddedEntities;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.JobTitles.DTOs;

namespace PlanSphere.Core.Features.JobTitles.Profiles;

public class JobTitleDTOProfile : Profile
{
    public JobTitleDTOProfile()
    {
        CreateMap<JobTitle, JobTitleDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.IsInheritanceActive, opt => opt.MapFrom<IsInheritanceActiveResolver>())
            .ForMember(dest => dest.SourceLevel, opt => opt.MapFrom(src => src.SourceLevel));
    }
    
    private class IsInheritanceActiveResolver : IValueResolver<JobTitle, JobTitleDTO, bool>
    {
        public bool Resolve(JobTitle source, JobTitleDTO destination, bool destMember, ResolutionContext context)
        {
            ulong? sourceLevelId = null;
            if (context.Items.TryGetValue("SourceLevelId", out var sourceLevelIdObj) && ulong.TryParse(sourceLevelIdObj?.ToString(), out var parsedSourceLevelId))
            {
                sourceLevelId = parsedSourceLevelId;
            }

            SourceLevel? sourceLevel = null;
            if (context.Items.TryGetValue("SourceLevel", out var sourceLevelObj) && sourceLevelObj is SourceLevel parsedSourceLevel)
            {
                sourceLevel = parsedSourceLevel;
            }
            
            if (source.OrganisationJobTitle != null)
            {
                if (sourceLevel == SourceLevel.Company)
                {
                    return sourceLevelId.HasValue &&
                           source.CompanyBlockedJobTitles.All(x => x.JobTitleId != source.Id && x.CompanyId != sourceLevelId.Value) &&
                           source.OrganisationJobTitle.IsInheritanceActive;
                }

                if (sourceLevel == SourceLevel.Department)
                {
                    return sourceLevelId.HasValue &&
                           source.DepartmentBlockedJobTitles.All(x => x.JobTitleId != source.Id && x.DepartmentId != sourceLevelId.Value) &&
                           source.OrganisationJobTitle.IsInheritanceActive;
                }
            }
            if (source.CompanyJobTitle != null)
            {
                if (sourceLevel == SourceLevel.Company)
                {
                    return sourceLevelId.HasValue &&
                           source.CompanyBlockedJobTitles.All(x => x.JobTitleId != source.Id && x.CompanyId != sourceLevelId.Value) &&
                           source.CompanyJobTitle.IsInheritanceActive;
                }

                if (sourceLevel == SourceLevel.Department)
                {
                    return sourceLevelId.HasValue &&
                           source.DepartmentBlockedJobTitles.All(x => x.JobTitleId != source.Id && x.DepartmentId != sourceLevelId.Value) &&
                           source.CompanyJobTitle.IsInheritanceActive;
                }
            }
            if (source.DepartmentJobTitle != null)
            {
                if (sourceLevel == SourceLevel.Company)
                {
                    return sourceLevelId.HasValue &&
                           source.CompanyBlockedJobTitles.All(x => x.JobTitleId != source.Id && x.CompanyId != sourceLevelId.Value) &&
                           source.DepartmentJobTitle.IsInheritanceActive;
                }

                if (sourceLevel == SourceLevel.Department)
                {
                    return sourceLevelId.HasValue &&
                           source.DepartmentBlockedJobTitles.All(x => x.JobTitleId != source.Id && x.DepartmentId != sourceLevelId.Value) &&
                           source.DepartmentJobTitle.IsInheritanceActive;
                }
            }
            if (source.TeamJobTitle != null)
            {
                return source.TeamJobTitle.IsInheritanceActive;
            }
            return false; 
        }
    }
}