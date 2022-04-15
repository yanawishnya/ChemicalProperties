using ChemicalPropertiesApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using IdentityDbContext = Microsoft.AspNet.Identity.EntityFramework.IdentityDbContext;

namespace ChemicalPropertiesApp.Data;

public class AuthDbContext : IdentityDbContext<ChemicalPropertiesAppUser>
{
    private readonly DbContextOptions _options;
    
    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
    {
        _options = options;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}