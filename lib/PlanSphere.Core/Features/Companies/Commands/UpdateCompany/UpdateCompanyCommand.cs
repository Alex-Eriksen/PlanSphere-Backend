using Domain.Entities;
using MediatR;
using PlanSphere.Core.Features.Companies.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanSphere.Core.Features.Companies.Commands.UpdateCompany;
public record UpdateCompanyCommand(CompanyUpdateRequest Request) : IRequest
{
    public ulong Id { get; set; }
}