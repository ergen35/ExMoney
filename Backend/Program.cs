using ExMoney.Backend.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);


//---- Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

builder.Services.AddEndpointsApiExplorer();

// builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(ExMoney.SharedLibs.Mappings.MapperConfiguration));

var conStr = builder.Configuration.GetConnectionString("exmoney-db");
builder.Services.AddDbContext<BackendDbContext>(options =>
{
    options.UseMySql(conStr, ServerVersion.AutoDetect(conStr));
});


builder.Services.AddHttpClient();


//TODO: add authentication & authorization 


//-- 
var app = builder.Build();

//--- Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

//TODO: add authentication & authorization 
// app.UseAuthorization();
// app.UseAuthentication();


app.MapControllers();

app.Run();
