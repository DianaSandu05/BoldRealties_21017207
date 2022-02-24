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
                name: "Accounts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    balance_due = table.Column<float>(type: "real", nullable: false),
                    charged_date = table.Column<float>(type: "real", nullable: false),
                    isReceived = table.Column<bool>(type: "bit", nullable: false),
                    received_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
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
                    UserID = table.Column<int>(type: "int", nullable: false),
                    TenancyID = table.Column<int>(type: "int", nullable: false),
                    transfered_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposits", x => x.Id);
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
                    UserID = table.Column<int>(type: "int", nullable: false),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    crime_Rate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enquiries", x => x.ID);
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
                    diagnostics = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tenanciesID = table.Column<int>(type: "int", nullable: false),
                    userID = table.Column<int>(type: "int", nullable: false),
                    filePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    invoiceID = table.Column<int>(type: "int", nullable: false),
                    imagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jobs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_jobs_Invoices_invoiceID",
                        column: x => x.invoiceID,
                        principalTable: "Invoices",
                        principalColumn: "Id" );
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
                    UserID = table.Column<int>(type: "int", nullable: false),
                    currency = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment", x => x.ID);
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
                    TenancyID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    EnquiriesID = table.Column<int>(type: "int", nullable: false),
                    maxPrice = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertiesRS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PropertiesRS_Enquiries_EnquiriesID",
                        column: x => x.EnquiriesID,
                        principalTable: "Enquiries",
                        principalColumn: "ID" );
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    filePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    ApplicantID = table.Column<int>(type: "int", nullable: false),
                    InvoicesID = table.Column<int>(type: "int", nullable: false),
                    AccountsID = table.Column<int>(type: "int", nullable: false),
                    imagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_Accounts_AccountsID",
                        column: x => x.AccountsID,
                        principalTable: "Accounts",
                        principalColumn: "ID" );
                    table.ForeignKey(
                        name: "FK_Users_Enquiries_ApplicantID",
                        column: x => x.ApplicantID,
                        principalTable: "Enquiries",
                        principalColumn: "ID" );
                    table.ForeignKey(
                        name: "FK_Users_Invoices_InvoicesID",
                        column: x => x.InvoicesID,
                        principalTable: "Invoices",
                        principalColumn: "Id" );
                    table.ForeignKey(
                        name: "FK_Users_PropertiesRS_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "PropertiesRS",
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
                    billsIncluded = table.Column<bool>(type: "bit", nullable: false),
                    comission = table.Column<float>(type: "real", nullable: false),
                    imagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    managementType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    accountsID = table.Column<int>(type: "int", nullable: false),
                    DepositsID = table.Column<int>(type: "int", nullable: false),
                    filePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                        name: "FK_tenancies_Deposits_DepositsID",
                        column: x => x.DepositsID,
                        principalTable: "Deposits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tenancies_PropertiesRS_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "PropertiesRS",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_tenancies_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Viewings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    viewing_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    ApplicantID = table.Column<int>(type: "int", nullable: false),
                    isConfirmed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viewings", x => x.Id);
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
                    table.ForeignKey(
                        name: "FK_Viewings_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
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
                column: "UserID",
                unique: true);

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
                name: "IX_Enquiries_PropertyID",
                table: "Enquiries",
                column: "PropertyID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enquiries_UserID",
                table: "Enquiries",
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
                name: "IX_jobs_userID",
                table: "jobs",
                column: "userID");

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
                name: "IX_Users_AccountsID",
                table: "Users",
                column: "AccountsID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ApplicantID",
                table: "Users",
                column: "ApplicantID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_InvoicesID",
                table: "Users",
                column: "InvoicesID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PropertyID",
                table: "Users",
                column: "PropertyID");

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
                name: "FK_Accounts_Invoices_InvoiceID",
                table: "Accounts",
                column: "InvoiceID",
                principalTable: "Invoices",
                principalColumn: "Id");

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
                name: "FK_Accounts_Users_UserID",
                table: "Accounts",
                column: "UserID",
                principalTable: "Users",
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
                name: "FK_Deposits_Users_UserID",
                table: "Deposits",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Enquiries_PropertiesRS_PropertyID",
                table: "Enquiries",
                column: "PropertyID",
                principalTable: "PropertiesRS",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Enquiries_Users_UserID",
                table: "Enquiries",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID");

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
                name: "FK_jobs_Users_userID",
                table: "jobs",
                column: "userID",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_payment_tenancies_TenancyID",
                table: "payment",
                column: "TenancyID",
                principalTable: "tenancies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_payment_Users_UserID",
                table: "payment",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertiesRS_tenancies_TenancyID",
                table: "PropertiesRS",
                column: "TenancyID",
                principalTable: "tenancies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertiesRS_Users_UserID",
                table: "PropertiesRS",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Invoices_InvoiceID",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Invoices_InvoicesID",
                table: "Users");

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

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_UserID",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Enquiries_Users_UserID",
                table: "Enquiries");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertiesRS_Users_UserID",
                table: "PropertiesRS");

            migrationBuilder.DropForeignKey(
                name: "FK_Enquiries_PropertiesRS_PropertyID",
                table: "Enquiries");

            migrationBuilder.DropTable(
                name: "BBAReports");

            migrationBuilder.DropTable(
                name: "jobs");

            migrationBuilder.DropTable(
                name: "officeAddress");

            migrationBuilder.DropTable(
                name: "Viewings");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "payment");

            migrationBuilder.DropTable(
                name: "tenancies");

            migrationBuilder.DropTable(
                name: "Deposits");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "PropertiesRS");

            migrationBuilder.DropTable(
                name: "Enquiries");
        }
    }
}
