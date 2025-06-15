using Microsoft.EntityFrameworkCore;

public class SqlDbContext : DbContext
{
    public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
    {
    }

    protected SqlDbContext()
    {
    }
}