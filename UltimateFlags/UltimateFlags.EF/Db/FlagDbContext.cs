using Microsoft.EntityFrameworkCore;

namespace UltimateFlags.EF.Db;

internal class FlagDbContext : DbContext
{
    public FlagDbContext(DbContextOptions<FlagDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}
