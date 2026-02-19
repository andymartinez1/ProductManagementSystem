using AspNet.Security.OAuth.GitHub;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Components;
using ProductManagementSystem.Components.Account;
using ProductManagementSystem.Data;
using ProductManagementSystem.Services.Email;
using ProductManagementSystem.Services.Products;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

var sqliteRelativePath = builder.Configuration["Serilog:SqlitePath"] ?? "Data/logs.db";
var sqliteFullPath = Path.Combine(builder.Environment.ContentRootPath, sqliteRelativePath);
Directory.CreateDirectory(Path.GetDirectoryName(sqliteFullPath)!);

var loggerConfig = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .MinimumLevel.Override("System", LogEventLevel.Warning)
    .Enrich.FromLogContext();

try
{
    // Requires the Serilog SQLite sink package (e.g., Serilog.Sinks.SQLite)
    loggerConfig = loggerConfig.WriteTo.SQLite(sqliteFullPath);
}
catch (Exception ex)
{
    // Safe fallback so the app still starts even if SQLite logging fails
    loggerConfig = loggerConfig
        .WriteTo.Console()
        .WriteTo.File(
            path: Path.Combine(builder.Environment.ContentRootPath, "Logs", "fallback-.log"),
            rollingInterval: RollingInterval.Day
        );

    // Create a temporary logger to record why SQLite failed
    Log.Logger = loggerConfig.CreateLogger();
    Log.Warning(
        ex,
        "SQLite logging sink failed to initialize. Falling back to console/file logging."
    );
}

Log.Logger = loggerConfig.CreateLogger();
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<
    AuthenticationStateProvider,
    IdentityRevalidatingAuthenticationStateProvider
>();

builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddGoogle(
        GoogleDefaults.AuthenticationScheme,
        options =>
        {
            options.ClientId =
                builder.Configuration["Authentication:Google:ClientId"]
                ?? throw new InvalidOperationException(
                    "Missing Authentication:Google:ClientId configuration."
                );

            options.ClientSecret =
                builder.Configuration["Authentication:Google:ClientSecret"]
                ?? throw new InvalidOperationException(
                    "Missing Authentication:Google:ClientSecret configuration."
                );
        }
    )
    .AddFacebook(
        FacebookDefaults.AuthenticationScheme,
        options =>
        {
            options.AppId =
                builder.Configuration["Authentication:Facebook:AppId"]
                ?? throw new InvalidOperationException(
                    "Missing Authentication:Facebook:AppId configuration."
                );

            options.AppSecret =
                builder.Configuration["Authentication:Facebook:AppSecret"]
                ?? throw new InvalidOperationException(
                    "Missing Authentication:Facebook:AppSecret configuration."
                );
        }
    )
    .AddGitHub(
        GitHubAuthenticationDefaults.AuthenticationScheme,
        options =>
        {
            options.ClientId =
                builder.Configuration["Authentication:GitHub:ClientId"]
                ?? throw new InvalidOperationException(
                    "Missing Authentication:GitHub:ClientId configuration."
                );

            options.ClientSecret =
                builder.Configuration["Authentication:GitHub:ClientSecret"]
                ?? throw new InvalidOperationException(
                    "Missing Authentication:GitHub:ClientSecret configuration."
                );

            // Optional but commonly useful: request the user's email address
            options.Scope.Add("user:email");
        }
    )
    .AddMicrosoftAccount(
        MicrosoftAccountDefaults.AuthenticationScheme,
        options =>
        {
            options.ClientId =
                builder.Configuration["Authentication:Microsoft:ClientId"]
                ?? throw new InvalidOperationException(
                    "Missing Authentication:Microsoft:ClientId configuration."
                );
            options.ClientSecret =
                builder.Configuration["Authentication:Microsoft:ClientSecret"]
                ?? throw new InvalidOperationException(
                    "Missing Authentication:Microsoft:ClientSecret configuration."
                );
        }
    )
    .AddTwitter(
        TwitterDefaults.AuthenticationScheme,
        options =>
        {
            options.ConsumerKey =
                builder.Configuration["Authentication:Twitter:ConsumerKey"]
                ?? throw new InvalidOperationException(
                    "Missing Authentication:Twitter:ConsumerKey configuration."
                );

            options.ConsumerSecret =
                builder.Configuration["Authentication:Twitter:ConsumerSecret"]
                ?? throw new InvalidOperationException(
                    "Missing Authentication:Twitter:ConsumerSecret configuration."
                );
        }
    )
    .AddIdentityCookies();

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, sql => sql.EnableRetryOnFailure())
);

builder.Services.AddQuickGridEntityFrameworkAdapter();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder
    .Services.AddIdentityCore<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IEmailSender<ApplicationUser>, IdentityEmailSender>();
builder.Services.AddBlazorBootstrap();

var app = builder.Build();

// Initialize and seed the database
using (var scope = app.Services.CreateAsyncScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await SeedData.InitializeDataAsync(services);
        app.Logger.LogInformation("Seeding database succeeded.");
    }
    catch (Exception e)
    {
        app.Logger.LogError(e, "An error occured while seeding the database.");
        throw;
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", true);
    app.UseHsts();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();

// Serve static files for interactive components
app.UseStaticFiles();
app.UseRouting();

// Authentication / Authorization middleware if Identity endpoints are used
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorPages();

// Identity Razor endpoints
app.MapAdditionalIdentityEndpoints();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

try
{
    app.Run();
}
finally
{
    Log.CloseAndFlush();
}
