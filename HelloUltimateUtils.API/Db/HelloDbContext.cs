using Microsoft.EntityFrameworkCore;
using UltimateFlags.EF.Db;

namespace HelloUltimateUtils.API.Db;

public class HelloDbContext : FlagDbContext
{
    public HelloDbContext(DbContextOptions options) : base(options)
    {
    }
}
