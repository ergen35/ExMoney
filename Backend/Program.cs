using ExMoney.Backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using MassTransit;
using MassTransit.GrpcTransport;
using ExMoney.Backend.EventHandlers;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders().AddSerilog();


//---- Add services to the container.
builder.Services.AddLogging(l => l.ClearProviders().AddSerilog());
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

builder.Services.AddEndpointsApiExplorer();

// builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(ExMoney.SharedLibs.Mappings.MapperConfiguration));

string conStr = builder.Configuration.GetConnectionString("exmoney-db");
builder.Services.AddDbContext<BackendDbContext>(options =>
{
    _ = options.UseMySql(conStr, ServerVersion.AutoDetect(conStr));
});

builder.Services.AddHttpClient();

builder.Services.AddMassTransit(config => {

    config.UsingGrpc((ctx, cfg) => {

        cfg.Host(h => {
            h.Host = "localhost";
            h.Port = 19678;
        });

        cfg.ConfigureEndpoints(ctx);
    });

    //register consummers
    config.AddConsumer<UserRegisteredHandler>();
});
// builder.Services.aDD

WebApplication app = builder.Build();

//Apply EF Core migrations

// MigrationsUpdater.ApplyPendingMigrations(app.Services);

//--- Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

//TODO: add authentication & authorization 
// app.UseAuthorization();
// app.UseAuthentication();

app.MapGet("/status", async (ctx) => {
    await ctx.Response.WriteAsJsonAsync("Status, Ok");
});

app.MapControllers();

app.Run();
