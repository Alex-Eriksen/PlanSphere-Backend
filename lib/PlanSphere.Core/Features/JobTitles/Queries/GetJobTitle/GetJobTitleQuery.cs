using MediatR;
using PlanSphere.Core.Features.JobTitles.DTOs;

namespace PlanSphere.Core.Features.JobTitles.Queries.GetJobTitle;

public record GetJobTitleQuery(ulong Id) : IRequest<JobTitleDTO>;