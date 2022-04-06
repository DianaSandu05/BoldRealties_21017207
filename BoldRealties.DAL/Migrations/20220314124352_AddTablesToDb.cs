using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoldRealties.DAL.Migrations
{
    public partial class AddTablesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enquiries",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    applicantType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone_No = table.Column<int>(type: "int", nullable: false),
                    pets = table.Column<bool>(type: "bit", nullable: false),
                    noPets = table.Column<int>(type: "int", nullable: false),
                    noBeds = table.Column<int>(type: "int", nullable: false),
                    noBaths = table.Column<int>(type: "int", nullable: false),
                    specification_Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    postcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    minPrice = table.Column<int>(type: "int", nullable: false),
                    maxPrice = table.Column<int>(type: "int", nullable: false),
                    crime_Rate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enquiries", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "officeAddress",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone1 = table.Column<int>(type: "int", nullable: false),
                    Phone2 = table.Column<int>(type: "int", nullable: false),
                    Fax = table.Column<int>(type: "int", nullable: false),
                    MapPathLarge = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MapPathSmall = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_officeAddress", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");

                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    balance_due = table.Column<float>(type: "real", nullable: false),
                    charged_date = table.Column<float>(type: "real", nullable: false),
                    isReceived = table.Column<bool>(type: "bit", nullable: false),
                    received_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenancyID = table.Column<int>(type: "int", nullable: false),
                    PaymentID = table.Column<int>(type: "int", nullable: false),
                    InvoiceID = table.Column<int>(type: "int", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");

                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PropertyID = table.Column<int>(type: "int", nullable: true),
                    ApplicantID = table.Column<int>(type: "int", nullable: true),
                    InvoicesID = table.Column<int>(type: "int", nullable: true),
                    AccountsID = table.Column<int>(type: "int", nullable: true),
                    filePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Accounts_AccountsID",
                        column: x => x.AccountsID,
                        principalTable: "Accounts",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Enquiries_ApplicantID",
                        column: x => x.ApplicantID,
                        principalTable: "Enquiries",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");

                });

            migrationBuilder.CreateTable(
                name: "BBAReports",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Report_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tenancyID = table.Column<int>(type: "int", nullable: false),
                    Outcome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BBAReports", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Deposits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    amount = table.Column<int>(type: "int", nullable: false),
                    isReceived = table.Column<bool>(type: "bit", nullable: false),
                    receivedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    due_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isProtected = table.Column<bool>(type: "bit", nullable: false),
                    protected_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isReturned = table.Column<bool>(type: "bit", nullable: false),
                    returned_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isTransfered = table.Column<bool>(type: "bit", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenancyID = table.Column<int>(type: "int", nullable: false),
                    transfered_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deposits_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                       
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    invoice_No = table.Column<int>(type: "int", nullable: false),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    TenancyID = table.Column<int>(type: "int", nullable: false),
                    filePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Due_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "jobs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isCompleted = table.Column<bool>(type: "bit", nullable: false),
                    tenanciesID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    filePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    invoiceID = table.Column<int>(type: "int", nullable: false),
                    diagnostics = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jobs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_jobs_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id" );
                    table.ForeignKey(
                        name: "FK_jobs_Invoices_invoiceID",
                        column: x => x.invoiceID,
                        principalTable: "Invoices",
                        principalColumn: "Id"
                      );
                });

            migrationBuilder.CreateTable(
                name: "payment",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    amount = table.Column<float>(type: "real", nullable: false),
                    received_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenancyID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    currency = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_payment_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PropertiesRS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    floorSpace = table.Column<float>(type: "real", nullable: false),
                    balconiesNo = table.Column<int>(type: "int", nullable: false),
                    balconySpace = table.Column<float>(type: "real", nullable: false),
                    bathsNo = table.Column<int>(type: "int", nullable: false),
                    bedsNo = table.Column<int>(type: "int", nullable: false),
                    garagesNo = table.Column<int>(type: "int", nullable: false),
                    parkingNo = table.Column<int>(type: "int", nullable: false),
                    estateDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    propertyType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    billsIncluded = table.Column<bool>(type: "bit", nullable: false),
                    propertyAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    marketPrice = table.Column<float>(type: "real", nullable: false),
                    petsAllowed = table.Column<bool>(type: "bit", nullable: false),
                    minPrice = table.Column<float>(type: "real", nullable: false),
                    TenancyID = table.Column<int>(type: "int", nullable: true),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EnquiriesID = table.Column<int>(type: "int", nullable: true),
                    imagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    maxPrice = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertiesRS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PropertiesRS_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PropertiesRS_Enquiries_EnquiriesID",
                        column: x => x.EnquiriesID,
                        principalTable: "Enquiries",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "tenancies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    rentPrice = table.Column<float>(type: "real", nullable: false),
                    comission = table.Column<float>(type: "real", nullable: false),
                    PropertyID = table.Column<int>(type: "int", nullable: true),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    managementType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    accountsID = table.Column<int>(type: "int", nullable: true),
                    DepositsID = table.Column<int>(type: "int", nullable: true),
                    filePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    billsIncluded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tenancies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tenancies_Accounts_accountsID",
                        column: x => x.accountsID,
                        principalTable: "Accounts",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_tenancies_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tenancies_Deposits_DepositsID",
                        column: x => x.DepositsID,
                        principalTable: "Deposits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tenancies_PropertiesRS_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "PropertiesRS",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Viewings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    viewing_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    ApplicantID = table.Column<int>(type: "int", nullable: false),
                    isConfirmed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viewings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Viewings_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Viewings_Enquiries_ApplicantID",
                        column: x => x.ApplicantID,
                        principalTable: "Enquiries",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Viewings_PropertiesRS_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "PropertiesRS",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_InvoiceID",
                table: "Accounts",
                column: "InvoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_PaymentID",
                table: "Accounts",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_TenancyID",
                table: "Accounts",
                column: "TenancyID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserID",
                table: "Accounts",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AccountsID",
                table: "AspNetUsers",
                column: "AccountsID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ApplicantID",
                table: "AspNetUsers",
                column: "ApplicantID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_InvoicesID",
                table: "AspNetUsers",
                column: "InvoicesID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PropertyID",
                table: "AspNetUsers",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BBAReports_tenancyID",
                table: "BBAReports",
                column: "tenancyID");

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_TenancyID",
                table: "Deposits",
                column: "TenancyID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_UserID",
                table: "Deposits",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PropertyID",
                table: "Invoices",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_TenancyID",
                table: "Invoices",
                column: "TenancyID");

            migrationBuilder.CreateIndex(
                name: "IX_jobs_invoiceID",
                table: "jobs",
                column: "invoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_jobs_PropertyID",
                table: "jobs",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_jobs_tenanciesID",
                table: "jobs",
                column: "tenanciesID");

            migrationBuilder.CreateIndex(
                name: "IX_jobs_UserID",
                table: "jobs",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_payment_TenancyID",
                table: "payment",
                column: "TenancyID");

            migrationBuilder.CreateIndex(
                name: "IX_payment_UserID",
                table: "payment",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertiesRS_EnquiriesID",
                table: "PropertiesRS",
                column: "EnquiriesID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertiesRS_TenancyID",
                table: "PropertiesRS",
                column: "TenancyID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertiesRS_UserID",
                table: "PropertiesRS",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_tenancies_accountsID",
                table: "tenancies",
                column: "accountsID");

            migrationBuilder.CreateIndex(
                name: "IX_tenancies_DepositsID",
                table: "tenancies",
                column: "DepositsID");

            migrationBuilder.CreateIndex(
                name: "IX_tenancies_PropertyID",
                table: "tenancies",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_tenancies_UserID",
                table: "tenancies",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Viewings_ApplicantID",
                table: "Viewings",
                column: "ApplicantID");

            migrationBuilder.CreateIndex(
                name: "IX_Viewings_PropertyID",
                table: "Viewings",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_Viewings_UserID",
                table: "Viewings",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AspNetUsers_UserID",
                table: "Accounts",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Invoices_InvoiceID",
                table: "Accounts",
                column: "InvoiceID",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_payment_PaymentID",
                table: "Accounts",
                column: "PaymentID",
                principalTable: "payment",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_tenancies_TenancyID",
                table: "Accounts",
                column: "TenancyID",
                principalTable: "tenancies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Invoices_InvoicesID",
                table: "AspNetUsers",
                column: "InvoicesID",
                principalTable: "Invoices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_PropertiesRS_PropertyID",
                table: "AspNetUsers",
                column: "PropertyID",
                principalTable: "PropertiesRS",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BBAReports_tenancies_tenancyID",
                table: "BBAReports",
                column: "tenancyID",
                principalTable: "tenancies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deposits_tenancies_TenancyID",
                table: "Deposits",
                column: "TenancyID",
                principalTable: "tenancies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_PropertiesRS_PropertyID",
                table: "Invoices",
                column: "PropertyID",
                principalTable: "PropertiesRS",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_tenancies_TenancyID",
                table: "Invoices",
                column: "TenancyID",
                principalTable: "tenancies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_jobs_PropertiesRS_PropertyID",
                table: "jobs",
                column: "PropertyID",
                principalTable: "PropertiesRS",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_jobs_tenancies_tenanciesID",
                table: "jobs",
                column: "tenanciesID",
                principalTable: "tenancies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_payment_tenancies_TenancyID",
                table: "payment",
                column: "TenancyID",
                principalTable: "tenancies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertiesRS_tenancies_TenancyID",
                table: "PropertiesRS",
                column: "TenancyID",
                principalTable: "tenancies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AspNetUsers_UserID",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Deposits_AspNetUsers_UserID",
                table: "Deposits");

            migrationBuilder.DropForeignKey(
                name: "FK_payment_AspNetUsers_UserID",
                table: "payment");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertiesRS_AspNetUsers_UserID",
                table: "PropertiesRS");

            migrationBuilder.DropForeignKey(
                name: "FK_tenancies_AspNetUsers_UserID",
                table: "tenancies");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Invoices_InvoiceID",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_payment_PaymentID",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_tenancies_TenancyID",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Deposits_tenancies_TenancyID",
                table: "Deposits");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertiesRS_tenancies_TenancyID",
                table: "PropertiesRS");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BBAReports");

            migrationBuilder.DropTable(
                name: "jobs");

            migrationBuilder.DropTable(
                name: "officeAddress");

            migrationBuilder.DropTable(
                name: "Viewings");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "payment");

            migrationBuilder.DropTable(
                name: "tenancies");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Deposits");

            migrationBuilder.DropTable(
                name: "PropertiesRS");

            migrationBuilder.DropTable(
                name: "Enquiries");
        }
    }
}
