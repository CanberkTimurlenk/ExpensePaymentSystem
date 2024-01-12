using FinalCase.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinalCase.Data.Contexts;

public class FinalCaseDbContext(DbContextOptions<FinalCaseDbContext> options) : DbContext(options)
// Primary Constructor
{
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FinalCaseDbContext).Assembly);
    }
}
