using MediatR;
using PlanSphere.Core.Features.Jobtitles.DTOs;

namespace PlanSphere.Core.Features.JobTitles.Queries.GetJobTitle;

public record GetJobTitleQuery(ulong Id) : IRequest<JobTitleDTO>;
