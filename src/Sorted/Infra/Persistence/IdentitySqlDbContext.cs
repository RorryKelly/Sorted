using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Sorted.Infra.Persistence;

public class IdentitySqlDbContext : IdentityDbContext<User, UserRole, string>
{
    public IdentitySqlDbContext(DbContextOptions<IdentitySqlDbContext> options) : base(options)
    {
        base.Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().Property(u => u.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<UserRole>().Property(r => r.Id).ValueGeneratedOnAdd();
    }
}