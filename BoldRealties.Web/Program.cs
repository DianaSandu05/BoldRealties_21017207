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

builder.Services.AddScoped<StaticDetails>();
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;
});




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


/*// Add authentication services
builder.Services.AddAuthentication(options =>
    {
        options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = "BoldSign";
    })
    .AddCookie()
            .AddOAuth("BoldSign", options =>
            {
                string domain = "https://account.boldsign.com";

                // Configure the Auth0 Client ID and Client Secret
                options.ClientId = "0a6aa257-9810-4305-a6e0-4bbfebff3e7f";
                options.ClientSecret = "831bc481-718a-4c0e-9860-419c91f7809a";
                options.CallbackPath = new PathString("/redirect");
                options.AuthorizationEndpoint = domain + "/connect/authorize";
                options.TokenEndpoint = domain + "/connect/token";
                options.UserInformationEndpoint = domain + "/connect/userinfo";
                // Configure the scope
                options.Scope.Clear();
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("offline_access");
                options.Scope.Add("BoldSign.Documents.All");
                options.Scope.Add("BoldSign.Templates.All");
                options.SaveTokens = true;
                options.UsePkce = true;
                options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "sub");
                options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                options.ClaimActions.MapJsonKey("access_token", "access_token");
                options.ClaimActions.MapJsonKey("refresh_token", "refresh_token");
                options.ClaimActions.MapJsonKey("expires_in", "expires_in");
                options.Events = new OAuthEvents
                {
                    OnCreatingTicket = async context =>
                    {
                        var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);

                        var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
                        response.EnsureSuccessStatusCode();
                        var user = JObject.Parse(await response.Content.ReadAsStringAsync());

                        user.Add("access_token", context.AccessToken);
                        user.Add("refresh_token", context.RefreshToken);
                        user.Add("expires_in", DateTime.Now.AddMinutes(1).ToString());

                        using (JsonDocument payload = JsonDocument.Parse(user.ToString()))
                        {
                            context.RunClaimActions(payload.RootElement);
                        }
                    },
                    OnRemoteFailure = context =>
                    {
                        context.HandleResponse();
                        context.Response.Redirect("/Home/Error?message=" + context.Failure.Message);
                        return Task.FromResult(0);
                    },
                    OnAccessDenied = context =>
                    {
                        return Task.FromResult(0);
                    }
                };
            });*/

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