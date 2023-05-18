using ExMoney.Services;
using Blazored.Modal;
using Microsoft.AspNetCore.Authentication.Cookies;
using AspNet.Security.OAuth.Keycloak;

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

//TODO: add auth httpClient
// builder.Services.AddTransient<AuthService>();

//add blazored modal
builder.Services.AddBlazoredModal();

//authentication

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddKeycloak(options => {

        //auth server base address
        options.BaseAddress = new Uri("http://localhost:8080");
        //kc version
        options.Version = new(20, 0, 3);
        options.Realm = "exmoney";
        options.ClientId = "exmoney-app";
        // options.ClientSecret = "";
        options.AccessType = KeycloakAuthenticationAccessType.Public;

        
    });


builder.Services.AddAuthorization(options => {

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthentication();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
