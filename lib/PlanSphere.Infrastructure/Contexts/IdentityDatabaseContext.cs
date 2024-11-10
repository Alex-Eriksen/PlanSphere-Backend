using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PlanSphere.Infrastructure.Contexts;

public class IdentityDatabaseContext : IdentityDbContext<ApplicationUser>
{
    public IdentityDatabaseContext(DbContextOptions<IdentityDatabaseContext> options)
        : base(options)
    {
    }
}