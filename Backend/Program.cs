using ExMoney.Backend.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


//---- Add services to the container.
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


//TODO: add authentication & authorization 

//-- 
WebApplication app = builder.Build();

//Apply EF Core migrations

MigrationsUpdater.ApplyPendingMigrations(app.Services);

//--- Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

//TODO: add authentication & authorization 
// app.UseAuthorization();
// app.UseAuthentication();

app.MapGet("/", (ctx) => "Status Ok");

app.MapControllers();

app.Run();
