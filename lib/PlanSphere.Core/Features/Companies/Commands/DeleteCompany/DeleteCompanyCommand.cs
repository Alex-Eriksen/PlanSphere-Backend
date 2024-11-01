using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanSphere.Core.Features.Companies.Commands.DeleteCompany
{
    public record DeleteCompanyCommand (ulong Id) : IRequest;
    

}