using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PlanSphere.SystemApi.Controllers.Base;

[ApiController]
[Route("api/[controller]/[action]")]
public class ApiControllerBase(IMediator _mediator) : ControllerBase
{
    
}