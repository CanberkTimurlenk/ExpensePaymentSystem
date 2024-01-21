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
            Username = "kalem",
            Firstname = "tukenmez",
            Lastname = "kalem",
            Email = "tukenmez.kalem@company.com",
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
            Username = "kirmizimasa",
            Firstname = "kırmızı",
            Lastname = "masa",
            Email = "masa@company.com",
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
            Username = "yonetici",
            Firstname = "sirket",
            Lastname = "yoneticisi",
            Email = "yonetici@admin.com",
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
            Name = "Is Gezisi",
            Description = "Is gezisindeki masraflar",
            InsertUserId = 3,
            InsertDate = new DateTime(2024, 1, 11),
        });

        modelBuilder.Entity<ExpenseCategory>().HasData(new ExpenseCategory
        {
            Id = 2,
            Name = "Kirtasiye Malzemeleri",
            Description = "Kirtasiye ihtiyaclarimiz",
            InsertUserId = 3,
            InsertDate = new DateTime(2024, 1, 01)
        });

        modelBuilder.Entity<ExpenseCategory>().HasData(new ExpenseCategory
        {
            Id = 3,
            Name = "Yemek",
            Description = "Yemege gidilmesi",
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
            EmployeeDescription = "Bursaya is gezisi icin gitmistim",
            Location = "Bursa",
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
            EmployeeDescription = "Fotokopi cektirmem gerekti",
            Location = "ofis",
            PaymentMethodId = 2,
            Status = ExpenseStatus.Approved,
            AdminDescription = "",
            InsertUserId = 2,
            InsertDate = new DateTime(2024, 1, 09),
        });

        modelBuilder.Entity<Expense>().HasData(new Expense
        {
            Id = 3,
            Amount = 200.75m,
            CategoryId = 3,
            CreatorEmployeeId = 2,
            Date = new DateTime(2024, 1, 03),
            EmployeeDescription = "Ekip olarak yemege gittik",
            Location = "pizzaci",
            PaymentMethodId = 3,
            Status = ExpenseStatus.Rejected,
            AdminDescription = "planlama yapilmadigindan reddedildi",
            InsertUserId = 1,
            InsertDate = new DateTime(2024, 1, 04),
            UpdateUserId = 3,
            UpdateDate = new DateTime(2024, 1, 05)            

        });

        modelBuilder.Entity<Document>().HasData(new Document
        {
            Id = 1,
            ExpenseId = 1,
            Name = "Is seyahati faturasi",
            Description = "fatura",
            Url = "ornekurl",
            InsertUserId = 1,
            InsertDate = new DateTime(2024, 1, 16)
        });

        modelBuilder.Entity<Document>().HasData(new Document
        {
            Id = 2,
            ExpenseId = 2,
            Name = "fatura",
            Description = "fatura ektedir",
            Url = "pdf.com",
            InsertUserId = 1,
            InsertDate = new DateTime(2024, 1, 09)
        });

        modelBuilder.Entity<Document>().HasData(new Document
        {
            Id = 3,
            ExpenseId = 3,
            Name = "Yemek ücretine ait fis",
            Description = "fisi ekte bulabilirsiniz",
            Url = "fis.com",
            InsertUserId = 2,
            InsertDate = new DateTime(2024, 1, 04)
        });

        modelBuilder.Entity<PaymentMethod>().HasData(new PaymentMethod
        {
            Id = 1,
            Name = "Credit Card",
            Description = "kredi karti ile yapilan odeme",
            InsertUserId = 3,
            InsertDate = new DateTime(2024, 1, 1)
        });

        modelBuilder.Entity<PaymentMethod>().HasData(new PaymentMethod
        {
            Id = 2,
            Name = "Bank Transfer",
            Description = "Banka transferi",
            InsertUserId = 3,
            InsertDate = new DateTime(2024, 1, 1)
        });

        modelBuilder.Entity<PaymentMethod>().HasData(new PaymentMethod
        {
            Id = 3,
            Name = "Cash",
            Description = "Nakit",
            InsertUserId = 3,
            InsertDate = new DateTime(2024, 1, 1)
        });

        modelBuilder.Entity<Payment>().HasData(new Payment
        {
            Id = 1,
            Amount = 100,
            Date = new DateTime(2024, 1, 16),
            ReceiverIban = "TR777777777777777777777777",            
            ReceiverName = "Tukenmez Kalem",
            PaymentMethodName = "Credit Card",
            EmployeeId = 1,
            PaymentMethodId = 1,
            ExpenseId = 1,
            IsCompleted = true,
        });
    }
}