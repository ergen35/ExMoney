using ExMoney.Backend.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var conStr = builder.Configuration.GetConnectionString("exmoney-db");

builder.Services.AddDbContext<BackendDbContext>(options => {
    options.UseMySql(conStr, ServerVersion.AutoDetect(conStr));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
