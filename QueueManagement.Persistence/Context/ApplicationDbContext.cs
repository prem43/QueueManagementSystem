using Microsoft.EntityFrameworkCore;
using QueueManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace QueueManagement.Persistence.Context;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
    }


    public DbSet<Category> Categories => Set<Category>();

    public DbSet<SubCategory> SubCategories => Set<SubCategory>();

    public DbSet<Counter> Counters => Set<Counter>();

    public DbSet<Token> Tokens => Set<Token>();
    public DbSet<TokenTransfer> TokenTransfers => Set<TokenTransfer>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(
    typeof(ApplicationDbContext).Assembly);
    }
}