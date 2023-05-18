using ExMoney.SharedLibs;
using Refit;

namespace ExMoney.Services
{
    public interface IExMoneyCurrenciesApi
    {
        [Get("/api/v1/currencies/list")]
        public Task<IApiResponse<List<Currency>>> List();
    }
}
