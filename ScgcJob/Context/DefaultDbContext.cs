using Microsoft.EntityFrameworkCore;

namespace ScgcJob.Context;

public class DefaultDbContext : DbContext
{
    public DefaultDbContext(DbContextOptions options) : base(options)
    {
    }
}