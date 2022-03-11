
using BoldRealties.DAL;
using BoldRealties.DAL.Repository;
using BoldRealties.DAL.Repository.IRepository;
using BoldRealties.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// ADD COMMENTS!!! -- I should explain what I did here!!!
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BoldRealties_dbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>

{

    options.SignIn.RequireConfirmedAccount = false;

    options.User.RequireUniqueEmail = true;

    options.Lockout.AllowedForNewUsers = true;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);

    options.Lockout.MaxFailedAccessAttempts = 3;



})

    .AddDefaultTokenProviders()

    .AddEntityFrameworkStores<BoldRealties_dbContext>();
builder.Services.AddSingleton<IEmailSender, EmailSender>();
// below line is used for mapping stripe keys with the properties from the class StripeSettings
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddRazorPages();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
// Stripe.Net - the NuGet package was installed in order to configure
// we provide the ApiKey which is the secret key which is retrieved though the method GetSection and returned as a string value
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();
app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
