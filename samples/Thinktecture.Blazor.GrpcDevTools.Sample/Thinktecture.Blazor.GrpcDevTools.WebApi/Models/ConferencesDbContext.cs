using Microsoft.EntityFrameworkCore;

namespace Thinktecture.Blazor.GrpcDevTools.WebApi.Models;

public class ConferencesDbContext : DbContext
{
    public ConferencesDbContext() { }
    public ConferencesDbContext(DbContextOptions<ConferencesDbContext> options)
        : base(options)
    {
    }
    public DbSet<Conference> Conferences { get; set; }
}