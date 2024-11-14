using MediatR;
using PlanSphere.Core.Features.JobTitles.DTOs;

namespace PlanSphere.Core.Features.JobTitles.Queries.LookUpJobTitles;

public record LookUpJobTitlesCommand(ulong UserId) : IRequest<List<JobTitleLookUpDTO>>;
