using ExMoney.Services;
using Blazored.Modal;

var builder = WebApplication.CreateBuilder(args);

//Configuration
builder.Configuration["AuthBaseAddress"] = "http://localhost:8080";
builder.Configuration["BackendBaseAddress"] = "http://localhost:5050";

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddLogging();

//--register Add backend HttpClients

//-- register refit client
builder.RegisterBackendApi(typeof(IExMoneyUsersApi));
builder.RegisterBackendApi(typeof(IExMoneyCurrenciesApi));
builder.RegisterBackendApi(typeof(IExMoneyTransactionsApi));

//TODO: add auth httpClient
// builder.Services.AddTransient<AuthService>();

//add blazored modal
builder.Services.AddBlazoredModal();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
