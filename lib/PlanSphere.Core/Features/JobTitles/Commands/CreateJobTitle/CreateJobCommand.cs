using MediatR;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.JobTitles.Requests;

namespace PlanSphere.Core.Features.JobTitles.Commands.CreateJobTitle;

public record CreateJobTitleCommand(SourceLevel SourceLevel, ulong SourceLevelId, JobTitleRequest Request) : IRequest;