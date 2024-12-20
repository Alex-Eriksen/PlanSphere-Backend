﻿using Domain.Entities;

namespace PlanSphere.Core.Interfaces.Repositories;

public interface ICountryRepository : IRepository<Country>
{
    Task<List<Country>> GetCountryLookUpsAsync(CancellationToken cancellationToken);
}