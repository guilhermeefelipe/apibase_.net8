using ApiBase.Domain.Entities;
using ApiBase.Repositories.DbContexts.Base;
using Microsoft.EntityFrameworkCore;

namespace ApiBase.Repositories.DbContexts;

public class ApiBaseDbContext : DbContextBase
{
    public ApiBaseDbContext(DbContextOptions<ApiBaseDbContext> options)
        : base(options)
    {
    }

    protected ApiBaseDbContext()
    {
    }

    public DbSet<Product> Product { get; set; } = default!;
    public DbSet<Sector> Sector { get; set; } = default!;
}
