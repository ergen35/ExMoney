using System.Reflection;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;
using IdP;
using IdP.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo
                                        .Console()
                                            .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders().AddSerilog();

//add services to the container
builder.Services.AddLogging(l => l.ClearProviders().AddSerilog());
builder.Services.AddMvc();
builder.Services.AddRazorPages();

var conStr = builder.Configuration.GetConnectionString("mysql-store");
var migrationAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

builder.Services.AddIdentityServer(options =>
{
    options.EmitScopesAsSpaceDelimitedStringInJwt = true;
})
.AddOperationalStore(options =>
{
    options.ConfigureDbContext = dbBuilder =>
    {
        dbBuilder.UseMySql(conStr, ServerVersion.AutoDetect(conStr), sqlOptions =>
        {
            sqlOptions.MigrationsAssembly(migrationAssembly);
        });
    };
})
.AddConfigurationStore(options =>
{
    options.ConfigureDbContext = dbBuilder =>
    {
        dbBuilder.UseMySql(conStr, ServerVersion.AutoDetect(conStr), sqlOptions =>
        {
            sqlOptions.MigrationsAssembly(migrationAssembly);
        });
    };
})
.AddTestUsers(IdpConfiguration.GetTestUsers()) //still Test Data
.AddDeveloperSigningCredential();


var app = builder.Build();

app.MapDefaultControllerRoute();
app.UseIdentityServer();

//Init IDS database
IdPInitializer.InitializeData(app.Services);

app.Run();

