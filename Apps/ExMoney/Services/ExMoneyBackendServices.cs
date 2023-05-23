using Microsoft.Extensions.Configuration;
using Refit;

namespace ExMoney.Services;

public static class ExMoneyBackendServices
{
    public static IServiceCollection RegisterBackendApi(this IServiceCollection services, ConfigurationManager configuration, Type backendType)
    {
        //Refit Settings
        RefitSettings settings = new() { Buffered = false };

        var backendUri = new Uri(configuration["BackendServer"]);

        services.AddRefitClient(backendType, settings)
            .ConfigureHttpClient(options =>
            {
                options.BaseAddress = backendUri;
                // options.DefaultRequestHeaders = TODO: Authorization Pronto
            }
        );

        return services;
    }
}

