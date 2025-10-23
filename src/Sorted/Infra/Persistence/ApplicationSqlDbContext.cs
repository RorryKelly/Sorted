using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Sorted.Domain;
using Sorted.Domain.ValueObject;

namespace Sorted.Infra.Persistence;

public class ApplicationSqlDbContext : DbContext
{

    public DbSet<Job> Jobs { get; set; }

    public DbSet<Invoice> Invoices { get; set; }

    public DbSet<Quote> Quotes { get; set; }

    public DbSet<Expense> Expenses { get; set; }

    public ApplicationSqlDbContext(DbContextOptions<ApplicationSqlDbContext> options)
        : base(options)
    {
        base.Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Job>().Property(u => u.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Invoice>().Property(u => u.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Quote>().Property(u => u.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Expense>().Property(u => u.Id).ValueGeneratedOnAdd();
    }
}