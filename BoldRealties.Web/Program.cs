using BoldRealties.BLL;
using BoldRealties.DAL;
using BoldRealties.DAL.Repository;
using BoldRealties.DAL.Repository.IRepository;
using BoldRealties.Models;
using BoldRealties.Web;
using BoldRealties.Web.Controllers;
using BoldSign.Api;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Stripe;
using BoldSign;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using BoldRealties.Web.Areas.Identity.Pages.Account.Manage;
using BoldRealties.Models.Identity;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


builder.Services.AddSingleton<ApiClient>();


// Add services to the container.
/*ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;*/
// Add services to the container.
// ADD COMMENTS!!! -- I should explain what I did here!!!
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BoldRealties_dbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthentication().AddFacebook(options =>
{
    options.AppId = "716138383030029";
    options.AppSecret = "ab192beadd91d6f3b720289cb3437e35";
});
builder.Services.AddAuthentication().AddGoogle(options =>
    {
        options.ClientId = "580558820670-gnchmvgo2uklm8ver26l7at85didotlo.apps.googleusercontent.com";
        options.ClientSecret = "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";
});
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;
});

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;
});



//login and registration configs
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
/*builder.Services.AddTransient<IEmailSender,EmailSender>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);*/
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});
// This method gets called by the runtime. Use this method to add services to the container.


//cookies configs
builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(15);

    options.LoginPath = "/Identity/Account/Login";
     options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});


// below line is used for mapping stripe keys with the properties from the class StripeSettings
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
/*builder.Services.Configure<DSConfiguration>(builder.Configuration.GetSection("DocuSign"));*/
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddRazorPages();


builder.Services.AddMemoryCache();
//services.AddCaching();// Adds a default in-memory implementation of IDistributedCache
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


var app = builder.Build();
 
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
/*app.Use(async (context, next) =>
{
    await context.RequestServices.GetRequiredService<BoldTokenService>().SetTokenAsync().ConfigureAwait(false);
    await next();
});*/
app.UseHttpsRedirection();
app.UseSession();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
// Stripe.Net - the NuGet package was installed in order to configure
// we provide the ApiKey which is the secret key which is retrieved though the method GetSection and returned as a string value
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();