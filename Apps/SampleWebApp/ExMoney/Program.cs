using ExMoney.Services;
using Blazored.Modal;
using Microsoft.AspNetCore.Components.Authorization;
using ExMoney.Authenticator;
using IdentityModel.Client;
using IdentityModel.OidcClient.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddLogging();
builder.Services.AddHttpClient();

//-- register refit client
builder.Services.RegisterBackendApi(builder.Configuration, typeof(IExMoneyUsersApi));
builder.Services.RegisterBackendApi(builder.Configuration, typeof(IExMoneyCurrenciesApi));
builder.Services.RegisterBackendApi(builder.Configuration, typeof(IExMoneyTransactionsApi));
builder.Services.RegisterBackendApi(builder.Configuration, typeof(IExMoneyRatesApi));
builder.Services.RegisterBackendApi(builder.Configuration, typeof(IExMoneyWalletsApi));

builder.Services.AddMemoryCache();
builder.Services.AddAuthorizationCore();
builder.Services.AddAuthenticationCore();

//add blazored modal
builder.Services.AddBlazoredModal();
builder.Services.AddScoped<ExMoneyJsInterop>();

//add authentication state provider
builder.Services.AddScoped<AppAuthenticationStateProvider>();

builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
{
    return provider.GetRequiredService<AppAuthenticationStateProvider>();
});

//configure authOptions
builder.Services.Configure<IdpAuthenticationOptions>(o =>
{
    o.RealmOrDomain = "";
    o.ClientId = "exmoney-mobile-app";
    o.Secret = "";
    o.Scope = "openid profile phone kyc_verified email";
    o.ServerUrl = "http://localhost:8050";
});

//add keycloak Authenticator
builder.Services.AddSingleton<KeycloakAuthenticator>();

//add discovery document
builder.Services.AddSingleton<IDiscoveryCache>((sp) =>
{
    IHttpClientFactory factory = sp.GetRequiredService<IHttpClientFactory>();
    return new DiscoveryCache("http://localhost:8050", () => factory.CreateClient());
});


WebApplication app = builder.Build();

// Configure the HTTP request pipeline.

app.UseDeveloperExceptionPage();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
