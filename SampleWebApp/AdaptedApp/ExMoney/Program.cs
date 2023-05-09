using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ExMoney.Data;
using ExMoney.Services;

var builder = WebApplication.CreateBuilder(args);

//Configuration
builder.Configuration["AuthBaseAddress"] = "http://localhost:3000";

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddLogging();

builder.Services.AddTransient<AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
