using FinalCase.Data.Entities;
using FinalCase.Data.Enums;
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
    public DbSet<PaymentMethod> PaymentMethods { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region seed
        modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
        {
            Id = 1,
            Username = "sampleUsername",
            Firstname = "John",
            Lastname = "Doe",
            Email = "b.doe@example.com",
            Password = "samplePassword",
            DateOfBirth = new DateTime(1990, 1, 1),
            LastActivityDate = DateTime.Now,
            Iban = "sampleIban",
            Role = "employee", // Assuming "User" is a valid role,
            IsActive = true,
            InsertUserId = 1,
            InsertDate = DateTime.Now,

        });

        modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
        {
            Id =2,
            Username = "abbb",
            Firstname = "ffffffffff",
            Lastname = "eeeeeeeeeeeeee",
            Email = "c.doe@example.com",
            Password = "samplePassword",
            DateOfBirth = new DateTime(1980, 1, 1),
            LastActivityDate = DateTime.Now,
            Iban = "sampleIban",
            Role = "employee", // Assuming "User" is a valid role,
            IsActive = true,
            InsertUserId = 2,
            InsertDate = DateTime.Now,
            

        });


        modelBuilder.Entity<Expense>().HasData(new Expense
        {
            Id = 1,
            Amount = 100,
            CategoryId = 1,
            CreatorEmployeeId = 1,
            Date = DateTime.Now,
            EmployeeDescription = "Employee Description",
            Location = "Location",
            PaymentMethodId = 1,
            Status = ExpenseStatus.Pending,
            AdminDescription = "Admin Description",
            InsertUserId = 1,
            InsertDate = DateTime.Now
        });

        modelBuilder.Entity<Expense>().HasData(new Expense
        {
            Id = 2,
            Amount = 100,
            CategoryId = 1,
            CreatorEmployeeId = 2,
            Date = DateTime.Now,
            EmployeeDescription = "Employee Description",
            Location = "Location",
            PaymentMethodId = 1,
            Status = ExpenseStatus.Pending,
            AdminDescription = "Admin Description",
            InsertUserId = 2,
            InsertDate = DateTime.Now
        });

        modelBuilder.Entity<Expense>().HasData(new Expense
        {
            Id = 3,
            Amount = 100,
            CategoryId = 1,
            CreatorEmployeeId = 1,
            Date = DateTime.Now,
            EmployeeDescription = "Employee Description",
            Location = "Location",
            PaymentMethodId = 1,
            Status = ExpenseStatus.Pending,
            AdminDescription = "Admin Description",
            InsertUserId = 1,
            InsertDate = DateTime.Now
        });

        modelBuilder.Entity<Document>().HasData(new Document
        {
            Id = 1,
            ExpenseId = 1,
            Name = "Name",
            Description = "Description",
            Url = "Url",
            InsertUserId = 1,
            InsertDate = DateTime.Now
        });

        modelBuilder.Entity<Document>().HasData(new Document
        {
            Id = 2,
            ExpenseId = 1,
            Name = "Name",
            Description = "Description",
            Url = "Url",
            InsertUserId = 2,
            InsertDate = DateTime.Now
        });

        modelBuilder.Entity<Document>().HasData(new Document
        {
            Id = 3,
            ExpenseId = 1,
            Name = "a",
            Description = "Description",
            Url = "Url",
            InsertUserId = 1,
            InsertDate = DateTime.Now
        });

        modelBuilder.Entity<ExpenseCategory>().HasData(new ExpenseCategory
        {
            Id = 1,
            Name = "b",
            Description = "Description",
            InsertUserId = 1,
            InsertDate = DateTime.Now
        });

        modelBuilder.Entity<ExpenseCategory>().HasData(new ExpenseCategory
        {
            Id = 2,
            Name = "c",
            Description = "Description",
            InsertUserId = 2,
            InsertDate = DateTime.Now
        });

        modelBuilder.Entity<ExpenseCategory>().HasData(new ExpenseCategory
        {
            Id = 3,
            Name = "d",
            Description = "Description",
            InsertUserId = 1,
            InsertDate = DateTime.Now
        });

        modelBuilder.Entity<PaymentMethod>().HasData(new PaymentMethod
        {
            Id = 1,
            Name = "b",
            Description = "Description",
            InsertUserId = 1,
            InsertDate = DateTime.Now
        });

        modelBuilder.Entity<PaymentMethod>().HasData(new PaymentMethod
        {
            Id = 2,
            Name = "a",
            Description = "Description",
            InsertUserId = 2,
            InsertDate = DateTime.Now
        });

        modelBuilder.Entity<PaymentMethod>().HasData(new PaymentMethod
        {
            Id = 3,
            Name = "x   ",
            Description = "Description",
            InsertUserId = 1,
            InsertDate = DateTime.Now
        });

        modelBuilder.Entity<Payment>().HasData(new Payment
        {
            Amount = 100,
            Date = DateTime.Now,
            Description = "Description",
            ReceiverIban = "ReceiverIban",
            ReceiverName = "ReceiverName",
            PaymentMethodName = "PaymentMethodName",
            EmployeeId = 1,
            PaymentMethodId = 1,
            ExpenseId = 1
        });

        modelBuilder.Entity<Payment>().HasData(new Payment
        {
            Amount = 200,
            Date = DateTime.Now,
            Description = "Description",
            ReceiverIban = "ReceiverIban",
            ReceiverName = "ReceiverName",
            PaymentMethodName = "PaymentMethodName",
            EmployeeId = 1,
            PaymentMethodId = 1,
            ExpenseId = 2
        });

        modelBuilder.Entity<Payment>().HasData(new Payment
        {
            Amount = 300,
            Date = DateTime.Now,
            Description = "Description",
            ReceiverIban = "ReceiverIban",
            ReceiverName = "ReceiverName",            
            PaymentMethodName = "PaymentMethodName",
            EmployeeId = 1,
            PaymentMethodId = 1,
            ExpenseId = 3
        });

        #endregion




        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FinalCaseDbContext).Assembly);


    }
}
