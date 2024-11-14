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
            ulong? sourceLevelId = TryGetSourceLevelId(context);
            
            SourceLevel? sourceLevel = TryGetSourceLevel(context);
            
            return EvaluateInheritance(source, sourceLevel, sourceLevelId);
        }
    
        private ulong? TryGetSourceLevelId(ResolutionContext context)
        {
            if (context.TryGetItems(out var items) && items.TryGetValue("SourceLevelId", out var sourceLevelIdObj) &&
                ulong.TryParse(sourceLevelIdObj?.ToString(), out var parsedSourceLevelId))
            {
                return parsedSourceLevelId;
            }
            return null;
        }
    
        private SourceLevel? TryGetSourceLevel(ResolutionContext context)
        {
            if (context.TryGetItems(out var items) && items.TryGetValue("SourceLevel", out var sourceLevelObj) &&
                sourceLevelObj is SourceLevel parsedSourceLevel)
            {
                return parsedSourceLevel;
            }
            return null;
        }
    
        private bool EvaluateInheritance(JobTitle source, SourceLevel? sourceLevel, ulong? sourceLevelId)
        {
            if (source.OrganisationJobTitle != null)
            {
                return CheckInheritance(source, source.OrganisationJobTitle.IsInheritanceActive, sourceLevel, sourceLevelId, source.CompanyBlockedJobTitles, source.DepartmentBlockedJobTitles);
            }
            
            if (source.CompanyJobTitle != null)
            {
                return CheckInheritance(source, source.CompanyJobTitle.IsInheritanceActive, sourceLevel, sourceLevelId, source.CompanyBlockedJobTitles, source.DepartmentBlockedJobTitles);
            }
            
            if (source.DepartmentJobTitle != null)
            {
                return CheckInheritance(source, source.DepartmentJobTitle.IsInheritanceActive, sourceLevel, sourceLevelId, source.CompanyBlockedJobTitles, source.DepartmentBlockedJobTitles);
            }
            
            if (source.TeamJobTitle != null)
            {
                return source.TeamJobTitle.IsInheritanceActive;
            }
            
            return false;
        }
    
        private bool CheckInheritance(
            JobTitle source,
            bool isInheritanceActive,
            SourceLevel? sourceLevel,
            ulong? sourceLevelId,
            List<CompanyBlockedJobTitle> companyBlockedJobTitles,
            List<DepartmentBlockedJobTitle> departmentBlockedJobTitles)
        {
            if (!sourceLevelId.HasValue || !sourceLevel.HasValue)
            {
                return false;
            }
    
            return sourceLevel switch
            {
                SourceLevel.Company => companyBlockedJobTitles.All(x => x.JobTitleId != source.Id && x.CompanyId != sourceLevelId.Value) && isInheritanceActive,
                SourceLevel.Department => departmentBlockedJobTitles.All(x => x.JobTitleId != source.Id && x.DepartmentId != sourceLevelId.Value) && isInheritanceActive,
                _ => isInheritanceActive
            };
        }
    }
}