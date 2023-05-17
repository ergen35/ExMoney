using Refit;

namespace ExMoney.Services;

public static class ExMoneyBackendServices
{
    public static WebApplicationBuilder RegisterBackendApi(this WebApplicationBuilder builder, Type backendType)
    {
        //Refit Settings
        RefitSettings settings = new() { Buffered = false };

        var backendUri = new Uri(builder.Configuration["BackendBaseAddress"]);

        builder.Services.AddRefitClient(backendType, settings)
            .ConfigureHttpClient(options =>
            {
                options.BaseAddress = backendUri;
                // options.DefaultRequestHeaders = TODO: Authorization Pronto
            }
        );

        return builder;
    }
}

