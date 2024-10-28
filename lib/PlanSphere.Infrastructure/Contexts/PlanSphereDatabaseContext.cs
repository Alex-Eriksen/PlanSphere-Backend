using Microsoft.EntityFrameworkCore;
using PlanSphere.Core.Interfaces.Database;

namespace PlanSphere.Infrastructure.Contexts;

public class PlanSphereDatabaseContext : DbContext, IPlanSphereDatabaseContext
{
    
}