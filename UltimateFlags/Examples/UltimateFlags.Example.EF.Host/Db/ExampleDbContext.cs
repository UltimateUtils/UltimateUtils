using Microsoft.EntityFrameworkCore;
using UltimateFlags.EF.Db;

namespace UltimateFlags.Example.EF.Host.Db;

public class ExampleDbContext : FlagDbContext
{
    public ExampleDbContext(DbContextOptions options) : base(options)
    {
    }
}
