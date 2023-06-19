using Blazored.Modal;
using ExMoney.Authenticator;
using ExMoney.Services;
using IdentityModel.Client;
using IdentityModel.OidcClient.Infrastructure;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;

namespace ExMoney
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            MauiAppBuilder builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            //-- Services

            builder.Services.AddMauiBlazorWebView();
            //Add server's config 
            builder.Configuration["AuthServerUrl"] = "http://valerymassa30-001-site1.atempurl.com";
            builder.Configuration["BackendUrl"] = "http://exmonero-001-site1.itempurl.com";

#if DEBUG
            builder.Configuration["AuthServerUrl"] = "http://localhost:8050";
            builder.Configuration["BackendUrl"] = "http://localhost:5050";
#endif
            //Add HttpClient
            builder.Services.AddLogging();
            builder.Services.AddHttpClient();

            //-- register refit client
            builder.Services.RegisterBackendApi(builder.Configuration, typeof(IExMoneyUsersApi));
            builder.Services.RegisterBackendApi(builder.Configuration, typeof(IExMoneyCurrenciesApi));
            builder.Services.RegisterBackendApi(builder.Configuration, typeof(IExMoneyTransactionsApi));
            builder.Services.RegisterBackendApi(builder.Configuration, typeof(IExMoneyRatesApi));
            builder.Services.RegisterBackendApi(builder.Configuration, typeof(IExMoneyWalletsApi));
            builder.Services.RegisterBackendApi(builder.Configuration, typeof(IExMoneyKycStatusApi));


            builder.Services.AddMemoryCache();
            builder.Services.AddAuthorizationCore();

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
                o.ServerUrl = builder.Configuration["AuthServer"];
            });

            //add keycloak Authenticator
            builder.Services.AddSingleton<KeycloakAuthenticator>();

            //add discovery document
            builder.Services.AddSingleton<IDiscoveryCache>((sp) =>
            {
                IHttpClientFactory factory = sp.GetRequiredService<IHttpClientFactory>();
                return new DiscoveryCache(builder.Configuration["AuthServer"], () => factory.CreateClient());
            });
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}