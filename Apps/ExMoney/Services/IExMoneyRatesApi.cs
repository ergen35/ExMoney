using ExMoney.SharedLibs.DTOs;
using Refit;

namespace ExMoney.Services
{
    public interface IExMoneyRatesApi
    {
        [Post("/api/v1/rates/calculate")]
        public Task<IApiResponse<ExchangeRate>> GetExchangeData(ExchangeRate data);
    }
}
