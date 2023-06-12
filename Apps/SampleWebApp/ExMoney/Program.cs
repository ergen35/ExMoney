using ExMoney.Services;
using Blazored.Modal;
using Microsoft.AspNetCore.Components.Authorization;
using ExMoney.Authenticator;
using IdentityModel.Client;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddMemoryCache();
builder.Services.AddAuthenticationCore();

//add blazored modal
builder.Services.AddBlazoredModal();
builder.Services.AddScoped<ExMoneyJsInterop>();

builder.Services.AddHttpClient();

//add authentication state provider
builder.Services.AddScoped<AppAuthenticationStateProvider>();

builder.Services.AddScoped<AuthenticationStateProvider>(provider => {
    return provider.GetRequiredService<AppAuthenticationStateProvider>();
});

//configure authOptions
builder.Services.Configure<IdpAuthenticationOptions>(o =>
{
    o.ClientId = "mobile-maui-app";
    o.RealmOrDomain = "test-realm";
    o.Scope = "openid profile";
    o.Secret = "3cuhUdO0uG9khzO4xykYHYKTdBOqKwu0";
    o.ServerUrl = "http://localhost:8080";
});

//add keycloak Authenticator
builder.Services.AddSingleton<KeycloakAuthenticator>();

//add discovery document
builder.Services.AddSingleton<IDiscoveryCache>((sp) =>
{
    IHttpClientFactory factory = sp.GetRequiredService<IHttpClientFactory>();
    return new DiscoveryCache("http://localhost:8080/realms/test-realm", () => factory.CreateClient());
});


WebApplication app = builder.Build();

// Configure the HTTP request pipeline.

app.UseDeveloperExceptionPage();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
