using Refit;

namespace ExMoney.Services;

public static class ExMoneyBackendServices
{
    public static IServiceCollection RegisterBackendApi(this IServiceCollection services, IConfiguration configuration, Type backendType)
    {
        //Refit Settings
        RefitSettings settings = new() { Buffered = false };

        var backendUri = new Uri(configuration["BackendServer"]);

        services.AddRefitClient(backendType, settings)
            .ConfigureHttpClient(options =>
            {
                options.BaseAddress = backendUri;
            }
        );

        return services;
    }
}

