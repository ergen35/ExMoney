using ExMoney.Services;
using Blazored.Modal;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddLogging();

//--register Add backend HttpClients

//-- register refit client
builder.Services.RegisterBackendApi(builder.Configuration, typeof(IExMoneyUsersApi));
builder.Services.RegisterBackendApi(builder.Configuration, typeof(IExMoneyCurrenciesApi));
builder.Services.RegisterBackendApi(builder.Configuration, typeof(IExMoneyTransactionsApi));
builder.Services.RegisterBackendApi(builder.Configuration, typeof(IExMoneyRatesApi));

//TODO: add auth httpClient
// builder.Services.AddTransient<AuthService>();

builder.Services.AddMemoryCache();

//add blazored modal
builder.Services.AddBlazoredModal();
builder.Services.AddScoped<ExMoneyJsInterop>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseDeveloperExceptionPage();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
