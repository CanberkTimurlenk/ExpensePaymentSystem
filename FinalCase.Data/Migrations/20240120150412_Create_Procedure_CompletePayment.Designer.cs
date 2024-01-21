﻿// <auto-generated />
using System;
using FinalCase.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FinalCase.Data.Migrations
{
    [DbContext(typeof(FinalCaseDbContext))]
    [Migration("20240120150412_Create_Procedure_CompletePayment")]
    partial class Create_Procedure_CompletePayment
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FinalCase.Data.Entities.ApplicationUser", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<DateTime>("DateOfBirth")
                    .HasColumnType("datetime2");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar(50)");

                b.Property<string>("Firstname")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar(50)");

                b.Property<string>("Iban")
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime>("InsertDate")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("datetime2")
                    .HasDefaultValueSql("GETDATE()");

                b.Property<int>("InsertUserId")
                    .HasColumnType("int");

                b.Property<bool>("IsActive")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("bit")
                    .HasDefaultValue(true);

                b.Property<DateTime>("LastActivityDate")
                    .HasColumnType("datetime2");

                b.Property<string>("Lastname")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar(50)");

                b.Property<string>("Password")
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnType("nvarchar(200)");

                b.Property<string>("Role")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar(50)");

                b.Property<DateTime?>("UpdateDate")
                    .HasColumnType("datetime2");

                b.Property<int?>("UpdateUserId")
                    .HasColumnType("int");

                b.Property<string>("Username")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.HasIndex("Email")
                    .IsUnique();

                b.ToTable("ApplicationUsers");

                b.HasData(
                    new
                    {
                        Id = 1,
                        DateOfBirth = new DateTime(1985, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        Email = "tukenmez.kalem@company.com",
                        Firstname = "tukenmez",
                        Iban = "TR777777777777777777777777",
                        InsertDate = new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        InsertUserId = 3,
                        IsActive = true,
                        LastActivityDate = new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        Lastname = "kalem",
                        Password = "ba394499b56b89fe5bda1338fcca6a04",
                        Role = "employee",
                        Username = "kalem"
                    },
                    new
                    {
                        Id = 2,
                        DateOfBirth = new DateTime(1990, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        Email = "masa@company.com",
                        Firstname = "kırmızı",
                        Iban = "TR666666666666666666666666",
                        InsertDate = new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        InsertUserId = 3,
                        IsActive = true,
                        LastActivityDate = new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        Lastname = "masa",
                        Password = "14e1b600b1fd579f47433b88e8d85291",
                        Role = "employee",
                        Username = "kirmizimasa"
                    },
                    new
                    {
                        Id = 3,
                        DateOfBirth = new DateTime(1990, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        Email = "yonetici@admin.com",
                        Firstname = "sirket",
                        InsertDate = new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        InsertUserId = 3,
                        IsActive = true,
                        LastActivityDate = new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        Lastname = "yoneticisi",
                        Password = "71b6551474932fda956136e87886017c",
                        Role = "admin",
                        Username = "yonetici"
                    });
            });

            modelBuilder.Entity("FinalCase.Data.Entities.Document", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("Description")
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnType("nvarchar(250)");

                b.Property<int>("ExpenseId")
                    .HasColumnType("int");

                b.Property<DateTime>("InsertDate")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("datetime2")
                    .HasDefaultValueSql("GETDATE()");

                b.Property<int>("InsertUserId")
                    .HasColumnType("int");

                b.Property<bool>("IsActive")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("bit")
                    .HasDefaultValue(true);

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.Property<DateTime?>("UpdateDate")
                    .HasColumnType("datetime2");

                b.Property<int?>("UpdateUserId")
                    .HasColumnType("int");

                b.Property<string>("Url")
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnType("nvarchar(150)");

                b.HasKey("Id");

                b.HasIndex("ExpenseId");

                b.ToTable("Documents");

                b.HasData(
                    new
                    {
                        Id = 1,
                        Description = "fatura",
                        ExpenseId = 1,
                        InsertDate = new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        InsertUserId = 1,
                        IsActive = false,
                        Name = "Is seyahati faturasi",
                        Url = "ornekurl"
                    },
                    new
                    {
                        Id = 2,
                        Description = "fatura ektedir",
                        ExpenseId = 2,
                        InsertDate = new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        InsertUserId = 1,
                        IsActive = false,
                        Name = "fatura",
                        Url = "pdf.com"
                    },
                    new
                    {
                        Id = 3,
                        Description = "fisi ekte bulabilirsiniz",
                        ExpenseId = 3,
                        InsertDate = new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        InsertUserId = 2,
                        IsActive = false,
                        Name = "Yemek ücretine ait fis",
                        Url = "fis.com"
                    });
            });

            modelBuilder.Entity("FinalCase.Data.Entities.Expense", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("AdminDescription")
                    .HasMaxLength(150)
                    .HasColumnType("nvarchar(150)");

                b.Property<decimal>("Amount")
                    .HasColumnType("decimal(18,2)");

                b.Property<int>("CategoryId")
                    .HasColumnType("int");

                b.Property<int>("CreatorEmployeeId")
                    .HasColumnType("int");

                b.Property<DateTime>("Date")
                    .HasColumnType("datetime2");

                b.Property<string>("EmployeeDescription")
                    .HasMaxLength(150)
                    .HasColumnType("nvarchar(150)");

                b.Property<DateTime>("InsertDate")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("datetime2")
                    .HasDefaultValueSql("GETDATE()");

                b.Property<int>("InsertUserId")
                    .HasColumnType("int");

                b.Property<bool>("IsActive")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("bit")
                    .HasDefaultValue(true);

                b.Property<string>("Location")
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnType("nvarchar(150)");

                b.Property<int>("PaymentMethodId")
                    .HasColumnType("int");

                b.Property<int?>("ReviewerAdminId")
                    .HasColumnType("int");

                b.Property<int>("Status")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasDefaultValue(1);

                b.Property<DateTime?>("UpdateDate")
                    .HasColumnType("datetime2");

                b.Property<int?>("UpdateUserId")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.HasIndex("CategoryId");

                b.HasIndex("CreatorEmployeeId");

                b.HasIndex("PaymentMethodId");

                b.HasIndex("ReviewerAdminId");

                b.ToTable("Expenses");

                b.HasData(
                    new
                    {
                        Id = 1,
                        Amount = 120.50m,
                        CategoryId = 1,
                        CreatorEmployeeId = 1,
                        Date = new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        EmployeeDescription = "Bursaya is gezisi icin gitmistim",
                        InsertDate = new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        InsertUserId = 1,
                        IsActive = false,
                        Location = "Bursa",
                        PaymentMethodId = 1,
                        Status = 1
                    },
                    new
                    {
                        Id = 2,
                        AdminDescription = "",
                        Amount = 75.30m,
                        CategoryId = 2,
                        CreatorEmployeeId = 2,
                        Date = new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        EmployeeDescription = "Fotokopi cektirmem gerekti",
                        InsertDate = new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        InsertUserId = 2,
                        IsActive = false,
                        Location = "ofis",
                        PaymentMethodId = 2,
                        Status = 2
                    },
                    new
                    {
                        Id = 3,
                        AdminDescription = "planlama yapilmadigindan reddedildi",
                        Amount = 200.75m,
                        CategoryId = 3,
                        CreatorEmployeeId = 2,
                        Date = new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        EmployeeDescription = "Ekip olarak yemege gittik",
                        InsertDate = new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        InsertUserId = 1,
                        IsActive = false,
                        Location = "pizzaci",
                        PaymentMethodId = 3,
                        Status = 3,
                        UpdateDate = new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        UpdateUserId = 3
                    });
            });

            modelBuilder.Entity("FinalCase.Data.Entities.ExpenseCategory", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("Description")
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnType("nvarchar(150)");

                b.Property<DateTime>("InsertDate")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("datetime2")
                    .HasDefaultValueSql("GETDATE()");

                b.Property<int>("InsertUserId")
                    .HasColumnType("int");

                b.Property<bool>("IsActive")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("bit")
                    .HasDefaultValue(true);

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar(50)");

                b.Property<DateTime?>("UpdateDate")
                    .HasColumnType("datetime2");

                b.Property<int?>("UpdateUserId")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.HasIndex("Name")
                    .IsUnique();

                b.ToTable("ExpenseCategories");

                b.HasData(
                    new
                    {
                        Id = 1,
                        Description = "Is gezisindeki masraflar",
                        InsertDate = new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        InsertUserId = 3,
                        IsActive = false,
                        Name = "Is Gezisi"
                    },
                    new
                    {
                        Id = 2,
                        Description = "Kirtasiye ihtiyaclarimiz",
                        InsertDate = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        InsertUserId = 3,
                        IsActive = false,
                        Name = "Kirtasiye Malzemeleri"
                    },
                    new
                    {
                        Id = 3,
                        Description = "Yemege gidilmesi",
                        InsertDate = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        InsertUserId = 3,
                        IsActive = false,
                        Name = "Yemek"
                    });
            });

            modelBuilder.Entity("FinalCase.Data.Entities.Payment", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<decimal>("Amount")
                    .HasColumnType("decimal(18,2)");

                b.Property<DateTime>("Date")
                    .HasColumnType("datetime2");

                b.Property<int>("EmployeeId")
                    .HasColumnType("int");

                b.Property<int>("ExpenseId")
                    .HasColumnType("int");

                b.Property<bool>("IsCompleted")
                    .HasColumnType("bit");

                b.Property<int>("PaymentMethodId")
                    .HasColumnType("int");

                b.Property<string>("PaymentMethodName")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("ReceiverIban")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("ReceiverName")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.HasIndex("ExpenseId")
                    .IsUnique();

                b.HasIndex("PaymentMethodId");

                b.HasIndex("EmployeeId", "ExpenseId")
                    .IsUnique();

                b.ToTable("Payments");

                b.HasData(
                    new
                    {
                        Id = 1,
                        Amount = 100m,
                        Date = new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        EmployeeId = 1,
                        ExpenseId = 1,
                        IsCompleted = true,
                        PaymentMethodId = 1,
                        PaymentMethodName = "Credit Card",
                        ReceiverIban = "TR777777777777777777777777",
                        ReceiverName = "Tukenmez Kalem"
                    });
            });

            modelBuilder.Entity("FinalCase.Data.Entities.PaymentMethod", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("Description")
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnType("nvarchar(150)");

                b.Property<DateTime>("InsertDate")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("datetime2")
                    .HasDefaultValueSql("GETDATE()");

                b.Property<int>("InsertUserId")
                    .HasColumnType("int");

                b.Property<bool>("IsActive")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("bit")
                    .HasDefaultValue(true);

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar(50)");

                b.Property<DateTime?>("UpdateDate")
                    .HasColumnType("datetime2");

                b.Property<int?>("UpdateUserId")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.HasIndex("Name")
                    .IsUnique();

                b.ToTable("PaymentMethods");

                b.HasData(
                    new
                    {
                        Id = 1,
                        Description = "kredi karti ile yapilan odeme",
                        InsertDate = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        InsertUserId = 3,
                        IsActive = false,
                        Name = "Credit Card"
                    },
                    new
                    {
                        Id = 2,
                        Description = "Banka transferi",
                        InsertDate = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        InsertUserId = 3,
                        IsActive = false,
                        Name = "Bank Transfer"
                    },
                    new
                    {
                        Id = 3,
                        Description = "Nakit",
                        InsertDate = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        InsertUserId = 3,
                        IsActive = false,
                        Name = "Cash"
                    });
            });

            modelBuilder.Entity("FinalCase.Data.Entities.Document", b =>
            {
                b.HasOne("FinalCase.Data.Entities.Expense", "Expense")
                    .WithMany("Documents")
                    .HasForeignKey("ExpenseId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                b.Navigation("Expense");
            });

            modelBuilder.Entity("FinalCase.Data.Entities.Expense", b =>
            {
                b.HasOne("FinalCase.Data.Entities.ExpenseCategory", "Category")
                    .WithMany("Expenses")
                    .HasForeignKey("CategoryId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("FinalCase.Data.Entities.ApplicationUser", "CreatorEmployee")
                    .WithMany("CreatedExpenses")
                    .HasForeignKey("CreatorEmployeeId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("FinalCase.Data.Entities.PaymentMethod", "PaymentMethod")
                    .WithMany("Expenses")
                    .HasForeignKey("PaymentMethodId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("FinalCase.Data.Entities.ApplicationUser", "ReviewerAdmin")
                    .WithMany()
                    .HasForeignKey("ReviewerAdminId");

                b.Navigation("Category");

                b.Navigation("CreatorEmployee");

                b.Navigation("PaymentMethod");

                b.Navigation("ReviewerAdmin");
            });

            modelBuilder.Entity("FinalCase.Data.Entities.Payment", b =>
            {
                b.HasOne("FinalCase.Data.Entities.ApplicationUser", "Employee")
                    .WithMany("Payments")
                    .HasForeignKey("EmployeeId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                b.HasOne("FinalCase.Data.Entities.Expense", "Expense")
                    .WithOne("Payment")
                    .HasForeignKey("FinalCase.Data.Entities.Payment", "ExpenseId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                b.HasOne("FinalCase.Data.Entities.PaymentMethod", "PaymentMethod")
                    .WithMany("Payments")
                    .HasForeignKey("PaymentMethodId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                b.Navigation("Employee");

                b.Navigation("Expense");

                b.Navigation("PaymentMethod");
            });

            modelBuilder.Entity("FinalCase.Data.Entities.ApplicationUser", b =>
            {
                b.Navigation("CreatedExpenses");

                b.Navigation("Payments");
            });

            modelBuilder.Entity("FinalCase.Data.Entities.Expense", b =>
            {
                b.Navigation("Documents");

                b.Navigation("Payment");
            });

            modelBuilder.Entity("FinalCase.Data.Entities.ExpenseCategory", b =>
            {
                b.Navigation("Expenses");
            });

            modelBuilder.Entity("FinalCase.Data.Entities.PaymentMethod", b =>
            {
                b.Navigation("Expenses");

                b.Navigation("Payments");
            });
#pragma warning restore 612, 618
        }
    }
}
