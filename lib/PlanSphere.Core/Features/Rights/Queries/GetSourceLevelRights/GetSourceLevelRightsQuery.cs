using Domain.Entities.EmbeddedEntities;
using MediatR;
using PlanSphere.Core.Features.Rights.DTOs;
using PlanSphere.Core.Interfaces;

namespace PlanSphere.Core.Features.Rights.Queries.GetSourceLevelRights;

public record GetSourceLevelRightsQuery(ulong UserId, SourceLevel? SourceLevel, ulong? SourceLevelId) : IRequest<SourceLevelRightDTO>;