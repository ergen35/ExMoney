using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Blazored.Modal;
using ExMoney.Services;
using Microsoft.AspNetCore.Authorization;

namespace ExMoney;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		//-- Services

        builder.Services.AddMauiBlazorWebView();
        //Add server's config
        //builder.Configuration["AuthServer"] = "http://10.34.64.124:57575";
        builder.Configuration["AuthServer"] = "http://localhost:8080";
        //builder.Configuration["BackendServer"] = "http://api.exmoney.com";
        builder.Configuration["BackendServer"] = "http://localhost:5050";


        //Add HttpClient
        builder.Services.AddHttpClient();
        //-- register refit client
        builder.Services.RegisterBackendApi(builder.Configuration, typeof(IExMoneyUsersApi));
        builder.Services.RegisterBackendApi(builder.Configuration, typeof(IExMoneyCurrenciesApi));
        builder.Services.RegisterBackendApi(builder.Configuration, typeof(IExMoneyTransactionsApi));

        //add blazored modal
        builder.Services.AddBlazoredModal();


#if DEBUG

        builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
