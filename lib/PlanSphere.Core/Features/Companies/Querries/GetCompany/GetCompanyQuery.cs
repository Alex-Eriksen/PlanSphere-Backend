﻿using MediatR;
using PlanSphere.Core.Features.Companies.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanSphere.Core.Features.Companies.Qurries.GetCompany;

public record GetCompanyQuery (ulong Id) : IRequest<CompanyDTO>;