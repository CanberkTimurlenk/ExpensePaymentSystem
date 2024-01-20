using FinalCase.Data.Entities;
using FinalCase.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace FinalCase.Data.Extensions;

public static class ModelBuilderExtensions
{
    public static void AddSeedData(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
        {
            Id = 1,
            Username = "johndoe",
            Firstname = "John",
            Lastname = "Doe",
            Email = "john.doe@example.com",
            Password = "ba394499b56b89fe5bda1338fcca6a04", // merhaba
            DateOfBirth = new DateTime(1985, 5, 15),
            LastActivityDate = new DateTime(2024, 1, 20),
            Iban = "TR777777777777777777777777",
            Role = "employee",
            IsActive = true,
            InsertUserId = 3,
            InsertDate = new DateTime(2023, 1, 1)
        });

        modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
        {
            Id = 2,
            Username = "alicebrown",
            Firstname = "Alice",
            Lastname = "Brown",
            Email = "alice.brown@example.com",
            Password = "14e1b600b1fd579f47433b88e8d85291", // 123456
            DateOfBirth = new DateTime(1990, 3, 22),
            LastActivityDate = new DateTime(2024, 1, 20),
            Iban = "TR666666666666666666666666",
            Role = "employee",
            IsActive = true,
            InsertUserId = 3,
            InsertDate = new DateTime(2023, 1, 1)
        });

        modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
        {
            Id = 3,
            Username = "adminnn",
            Firstname = "admin",
            Lastname = "user",
            Email = "example@mail.com",
            Password = "71b6551474932fda956136e87886017c", // testpassword
            DateOfBirth = new DateTime(1990, 3, 22),
            LastActivityDate = new DateTime(2024, 1, 20),
            Role = "admin",
            IsActive = true,
            InsertUserId = 3,
            InsertDate = new DateTime(2023, 1, 1)
        });

        modelBuilder.Entity<ExpenseCategory>().HasData(new ExpenseCategory
        {
            Id = 1,
            Name = "Business Trip",
            Description = "Expenses related to business trips",
            InsertUserId = 3,
            InsertDate = new DateTime(2024, 1, 11),
        });

        modelBuilder.Entity<ExpenseCategory>().HasData(new ExpenseCategory
        {
            Id = 2,
            Name = "Office Supplies",
            Description = "Expenses for office supplies",
            InsertUserId = 3,
            InsertDate = new DateTime(2024, 1, 01)
        });

        modelBuilder.Entity<ExpenseCategory>().HasData(new ExpenseCategory
        {
            Id = 3,
            Name = "Team Events",
            Description = "Expenses for team events",
            InsertUserId = 3,
            InsertDate = new DateTime(2024, 1, 01)
        });

        modelBuilder.Entity<Expense>().HasData(new Expense
        {
            Id = 1,
            Amount = 120.50m,
            CategoryId = 1,
            CreatorEmployeeId = 1,
            Date = new DateTime(2024, 1, 15),
            EmployeeDescription = "Business trip to Germany",
            Location = "Frankfurt",
            PaymentMethodId = 1,
            Status = ExpenseStatus.Pending,
            AdminDescription = null,
            InsertUserId = 1,
            InsertDate = new DateTime(2024, 1, 16),
        });

        modelBuilder.Entity<Expense>().HasData(new Expense
        {
            Id = 2,
            Amount = 75.30m,
            CategoryId = 2,
            CreatorEmployeeId = 2,
            Date = new DateTime(2024, 1, 08),
            EmployeeDescription = "Purchase of office supplies",
            Location = "Office",
            PaymentMethodId = 2,
            Status = ExpenseStatus.Approved,
            AdminDescription = "Approved by the manager",
            InsertUserId = 2,
            InsertDate = new DateTime(2024, 1, 09),
        });

        modelBuilder.Entity<Expense>().HasData(new Expense
        {
            Id = 3,
            Amount = 200.75m,
            CategoryId = 3,
            CreatorEmployeeId = 1,
            Date = new DateTime(2024, 1, 03),
            EmployeeDescription = "Team dinner celebration",
            Location = "Local Restaurant",
            PaymentMethodId = 3,
            Status = ExpenseStatus.Rejected,
            AdminDescription = "Rejected due to policy violation",
            InsertUserId = 1,
            InsertDate = new DateTime(2024, 1, 04),
        });

        modelBuilder.Entity<Document>().HasData(new Document
        {
            Id = 1,
            ExpenseId = 1,
            Name = "Business Trip Receipt",
            Description = "Receipt for business trip expenses",
            Url = "/documents/business_trip_receipt.pdf",
            InsertUserId = 1,
            InsertDate = new DateTime(2024, 1, 16)
        });

        modelBuilder.Entity<Document>().HasData(new Document
        {
            Id = 2,
            ExpenseId = 2,
            Name = "Office Supplies Invoice",
            Description = "Invoice for office supplies",
            Url = "/documents/office_supplies_invoice.pdf",
            InsertUserId = 2,
            InsertDate = new DateTime(2024, 1, 09)
        });

        modelBuilder.Entity<Document>().HasData(new Document
        {
            Id = 3,
            ExpenseId = 3,
            Name = "Team Dinner Photos",
            Description = "Photos from the team dinner",
            Url = "/documents/team_dinner_photos.zip",
            InsertUserId = 1,
            InsertDate = new DateTime(2024, 1, 04)
        });

        modelBuilder.Entity<PaymentMethod>().HasData(new PaymentMethod
        {
            Id = 1,
            Name = "Credit Card",
            Description = "Payment via credit card",
            InsertUserId = 3,
            InsertDate = new DateTime(2024, 1, 1)
        });

        modelBuilder.Entity<PaymentMethod>().HasData(new PaymentMethod
        {
            Id = 2,
            Name = "Bank Transfer",
            Description = "Payment via bank transfer",
            InsertUserId = 3,
            InsertDate = new DateTime(2024, 1, 1)
        });

        modelBuilder.Entity<PaymentMethod>().HasData(new PaymentMethod
        {
            Id = 3,
            Name = "Cash",
            Description = "Payment in cash",
            InsertUserId = 3,
            InsertDate = new DateTime(2024, 1, 1)
        });

        modelBuilder.Entity<Payment>().HasData(new Payment
        {
            Amount = 100,
            Date = new DateTime(2024, 1, 16),
            Description = "Business trip expenses payment",
            ReceiverIban = "TR777777777777777777777777",
            ReceiverName = "John Doe",
            PaymentMethodName = "Credit Card",
            EmployeeId = 1,
            PaymentMethodId = 1,
            ExpenseId = 1
        });

        modelBuilder.Entity<Payment>().HasData(new Payment
        {
            Amount = 75.30m,
            Date = new DateTime(2024, 1, 09),
            Description = "Office supplies payment",
            ReceiverIban = "TR666666666666666666666666",
            ReceiverName = "Alice Brown",
            PaymentMethodName = "Bank Transfer",
            EmployeeId = 2,
            PaymentMethodId = 2,
            ExpenseId = 2
        });

        modelBuilder.Entity<Payment>().HasData(new Payment
        {
            Amount = 200.75m,
            Date = new DateTime(2024, 1, 04),
            Description = "Team dinner payment",
            ReceiverIban = "TR777777777777777777777777",
            ReceiverName = "John Doe",
            PaymentMethodName = "Cash",
            EmployeeId = 1,
            PaymentMethodId = 3,
            ExpenseId = 3
        });
    }
}