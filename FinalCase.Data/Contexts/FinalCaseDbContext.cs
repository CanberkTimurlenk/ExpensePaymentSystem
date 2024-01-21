using FinalCase.Data.Entities;
using FinalCase.Data.Enums;
using FinalCase.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FinalCase.Data.Contexts;

public class FinalCaseDbContext(DbContextOptions<FinalCaseDbContext> options) : DbContext(options)
// Primary Constructor
{
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddSeedData();

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


    }
}
