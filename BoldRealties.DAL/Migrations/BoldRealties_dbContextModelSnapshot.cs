﻿// <auto-generated />
using System;
using BoldRealties.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BoldRealties.DAL.Migrations
{
    [DbContext(typeof(BoldRealties_dbContext))]
    partial class BoldRealties_dbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BoldRealties.Models.Accounts", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InvoiceID")
                        .HasColumnType("int");

                    b.Property<int>("PaymentID")
                        .HasColumnType("int");

                    b.Property<int>("TenancyID")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("balance_due")
                        .HasColumnType("real");

                    b.Property<float>("charged_date")
                        .HasColumnType("real");

                    b.Property<bool>("isReceived")
                        .HasColumnType("bit");

                    b.Property<DateTime>("received_Date")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("InvoiceID");

                    b.HasIndex("PaymentID");

                    b.HasIndex("TenancyID")
                        .IsUnique();

                    b.HasIndex("UserID");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("BoldRealties.Models.BA_Reports", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Outcome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Report_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("tenancyID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("tenancyID");

                    b.ToTable("BBAReports");
                });

            modelBuilder.Entity("BoldRealties.Models.Deposits", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TenancyID")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("amount")
                        .HasColumnType("int");

                    b.Property<DateTime>("due_date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("isProtected")
                        .HasColumnType("bit");

                    b.Property<bool>("isReceived")
                        .HasColumnType("bit");

                    b.Property<bool>("isReturned")
                        .HasColumnType("bit");

                    b.Property<bool>("isTransfered")
                        .HasColumnType("bit");

                    b.Property<DateTime>("protected_Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("receivedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("returned_Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("transfered_Date")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TenancyID")
                        .IsUnique();

                    b.HasIndex("UserID");

                    b.ToTable("Deposits");
                });

            modelBuilder.Entity("BoldRealties.Models.Enquiries", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("NameSurname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("applicantType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("crime_Rate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("maxPrice")
                        .HasColumnType("int");

                    b.Property<int>("minPrice")
                        .HasColumnType("int");

                    b.Property<int>("noBaths")
                        .HasColumnType("int");

                    b.Property<int>("noBeds")
                        .HasColumnType("int");

                    b.Property<int>("noPets")
                        .HasColumnType("int");

                    b.Property<bool>("pets")
                        .HasColumnType("bit");

                    b.Property<int>("phone_No")
                        .HasColumnType("int");

                    b.Property<string>("postcode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("specification_Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Enquiries");
                });

            modelBuilder.Entity("BoldRealties.Models.Invoices", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Due_Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("PropertyID")
                        .HasColumnType("int");

                    b.Property<int>("TenancyID")
                        .HasColumnType("int");

                    b.Property<string>("filePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("invoice_No")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PropertyID");

                    b.HasIndex("TenancyID");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("BoldRealties.Models.jobs", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PropertyID")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("diagnostics")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("filePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("invoiceID")
                        .HasColumnType("int");

                    b.Property<bool>("isCompleted")
                        .HasColumnType("bit");

                    b.Property<int>("tenanciesID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("PropertyID");

                    b.HasIndex("UserID");

                    b.HasIndex("invoiceID");

                    b.HasIndex("tenanciesID");

                    b.ToTable("jobs");
                });

            modelBuilder.Entity("BoldRealties.Models.officeAddress", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Fax")
                        .HasColumnType("int");

                    b.Property<string>("MapPathLarge")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MapPathSmall")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Phone1")
                        .HasColumnType("int");

                    b.Property<int>("Phone2")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("officeAddress");
                });

            modelBuilder.Entity("BoldRealties.Models.payment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("TenancyID")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("amount")
                        .HasColumnType("real");

                    b.Property<string>("currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("received_Date")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("TenancyID");

                    b.HasIndex("UserID");

                    b.ToTable("payment");
                });

            modelBuilder.Entity("BoldRealties.Models.PropertiesRS", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int?>("EnquiriesID")
                        .HasColumnType("int");

                    b.Property<int?>("TenancyID")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("balconiesNo")
                        .HasColumnType("int");

                    b.Property<float>("balconySpace")
                        .HasColumnType("real");

                    b.Property<int>("bathsNo")
                        .HasColumnType("int");

                    b.Property<int>("bedsNo")
                        .HasColumnType("int");

                    b.Property<bool>("billsIncluded")
                        .HasColumnType("bit");

                    b.Property<string>("estateDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("floorSpace")
                        .HasColumnType("real");

                    b.Property<int>("garagesNo")
                        .HasColumnType("int");

                    b.Property<string>("imagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("marketPrice")
                        .HasColumnType("real");

                    b.Property<float>("maxPrice")
                        .HasColumnType("real");

                    b.Property<float>("minPrice")
                        .HasColumnType("real");

                    b.Property<int>("parkingNo")
                        .HasColumnType("int");

                    b.Property<bool>("petsAllowed")
                        .HasColumnType("bit");

                    b.Property<string>("propertyAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("propertyType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("EnquiriesID");

                    b.HasIndex("TenancyID");

                    b.HasIndex("UserID");

                    b.ToTable("PropertiesRS");
                });

            modelBuilder.Entity("BoldRealties.Models.tenancies", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("DepositsID")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PropertyID")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("accountsID")
                        .HasColumnType("int");

                    b.Property<bool>("billsIncluded")
                        .HasColumnType("bit");

                    b.Property<float>("comission")
                        .HasColumnType("real");

                    b.Property<string>("filePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("managementType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("rentPrice")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("DepositsID");

                    b.HasIndex("PropertyID");

                    b.HasIndex("UserID");

                    b.HasIndex("accountsID");

                    b.ToTable("tenancies");
                });

            modelBuilder.Entity("BoldRealties.Models.Viewings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ApplicantID")
                        .HasColumnType("int");

                    b.Property<int>("PropertyID")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("isConfirmed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("viewing_Date")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantID");

                    b.HasIndex("PropertyID");

                    b.HasIndex("UserID");

                    b.ToTable("Viewings");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("BoldRealties.Models.Users", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<int?>("AccountsID")
                        .HasColumnType("int");

                    b.Property<int?>("ApplicantID")
                        .HasColumnType("int");

                    b.Property<int?>("InvoicesID")
                        .HasColumnType("int");

                    b.Property<int?>("PropertyID")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("filePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("AccountsID");

                    b.HasIndex("ApplicantID");

                    b.HasIndex("InvoicesID");

                    b.HasIndex("PropertyID");

                    b.HasDiscriminator().HasValue("Users");
                });

            modelBuilder.Entity("BoldRealties.Models.Accounts", b =>
                {
                    b.HasOne("BoldRealties.Models.Invoices", "Invoices")
                        .WithMany()
                        .HasForeignKey("InvoiceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BoldRealties.Models.payment", "payment")
                        .WithMany()
                        .HasForeignKey("PaymentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BoldRealties.Models.tenancies", "tenancies")
                        .WithOne()
                        .HasForeignKey("BoldRealties.Models.Accounts", "TenancyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "Users")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Invoices");

                    b.Navigation("Users");

                    b.Navigation("payment");

                    b.Navigation("tenancies");
                });

            modelBuilder.Entity("BoldRealties.Models.BA_Reports", b =>
                {
                    b.HasOne("BoldRealties.Models.tenancies", "tenancies")
                        .WithMany()
                        .HasForeignKey("tenancyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("tenancies");
                });

            modelBuilder.Entity("BoldRealties.Models.Deposits", b =>
                {
                    b.HasOne("BoldRealties.Models.tenancies", "tenancies")
                        .WithOne()
                        .HasForeignKey("BoldRealties.Models.Deposits", "TenancyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "Users")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");

                    b.Navigation("tenancies");
                });

            modelBuilder.Entity("BoldRealties.Models.Invoices", b =>
                {
                    b.HasOne("BoldRealties.Models.PropertiesRS", "PropertiesRS")
                        .WithMany()
                        .HasForeignKey("PropertyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BoldRealties.Models.tenancies", "tenancies")
                        .WithMany()
                        .HasForeignKey("TenancyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PropertiesRS");

                    b.Navigation("tenancies");
                });

            modelBuilder.Entity("BoldRealties.Models.jobs", b =>
                {
                    b.HasOne("BoldRealties.Models.PropertiesRS", "PropertiesRS")
                        .WithMany()
                        .HasForeignKey("PropertyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "Users")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BoldRealties.Models.Invoices", "Invoices")
                        .WithMany()
                        .HasForeignKey("invoiceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BoldRealties.Models.tenancies", "Tenancies")
                        .WithMany()
                        .HasForeignKey("tenanciesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Invoices");

                    b.Navigation("PropertiesRS");

                    b.Navigation("Tenancies");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("BoldRealties.Models.payment", b =>
                {
                    b.HasOne("BoldRealties.Models.tenancies", "tenancies")
                        .WithMany()
                        .HasForeignKey("TenancyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "Users")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");

                    b.Navigation("tenancies");
                });

            modelBuilder.Entity("BoldRealties.Models.PropertiesRS", b =>
                {
                    b.HasOne("BoldRealties.Models.Enquiries", "Enquiries")
                        .WithMany()
                        .HasForeignKey("EnquiriesID");

                    b.HasOne("BoldRealties.Models.tenancies", "tenancies")
                        .WithMany()
                        .HasForeignKey("TenancyID");

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "Users")
                        .WithMany()
                        .HasForeignKey("UserID");

                    b.Navigation("Enquiries");

                    b.Navigation("Users");

                    b.Navigation("tenancies");
                });

            modelBuilder.Entity("BoldRealties.Models.tenancies", b =>
                {
                    b.HasOne("BoldRealties.Models.Deposits", "Deposits")
                        .WithMany()
                        .HasForeignKey("DepositsID");

                    b.HasOne("BoldRealties.Models.PropertiesRS", "PropertiesRS")
                        .WithMany()
                        .HasForeignKey("PropertyID");

                    b.HasOne("BoldRealties.Models.Users", "Users")
                        .WithMany()
                        .HasForeignKey("UserID");

                    b.HasOne("BoldRealties.Models.Accounts", "Accounts")
                        .WithMany()
                        .HasForeignKey("accountsID");

                    b.Navigation("Accounts");

                    b.Navigation("Deposits");

                    b.Navigation("PropertiesRS");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("BoldRealties.Models.Viewings", b =>
                {
                    b.HasOne("BoldRealties.Models.Enquiries", "Enquiries")
                        .WithMany()
                        .HasForeignKey("ApplicantID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BoldRealties.Models.PropertiesRS", "PropertiesRS")
                        .WithMany()
                        .HasForeignKey("PropertyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "Users")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Enquiries");

                    b.Navigation("PropertiesRS");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BoldRealties.Models.Users", b =>
                {
                    b.HasOne("BoldRealties.Models.Accounts", "Accounts")
                        .WithMany()
                        .HasForeignKey("AccountsID");

                    b.HasOne("BoldRealties.Models.Enquiries", "Enquiries")
                        .WithMany()
                        .HasForeignKey("ApplicantID");

                    b.HasOne("BoldRealties.Models.Invoices", "Invoices")
                        .WithMany()
                        .HasForeignKey("InvoicesID");

                    b.HasOne("BoldRealties.Models.PropertiesRS", "PropertiesRS")
                        .WithMany()
                        .HasForeignKey("PropertyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Accounts");

                    b.Navigation("Enquiries");

                    b.Navigation("Invoices");

                    b.Navigation("PropertiesRS");
                });
#pragma warning restore 612, 618
        }
    }
}
