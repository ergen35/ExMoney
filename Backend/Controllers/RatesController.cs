using currencyapi;
using ExMoney.Backend.Data;
using ExMoney.SharedLibs.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace ExMoney.Backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RatesController : ControllerBase
    {
        private readonly BackendDbContext db;
        private readonly ILogger<RatesController> logger;

        public RatesController(BackendDbContext db, ILogger<RatesController> logger)
        {
            this.db = db;
            this.logger = logger;
        }

        [HttpPost("calculate")]
        public async Task<ActionResult<ExchangeRate>> CalculateRate(ExchangeRate data)
        {
            SharedLibs.ExMoneySettings settings = db.ExMoneySettings.FirstOrDefault() ?? throw new Exception("Erreur interne");

            //find currencies
            Currencyapi fx = new(settings.CurrencyExchangeApiKey);
            double rate = 0d;
            double exchangeValue = 0d;

            try
            {
                string exchangeResult = fx.Latest(data.BaseCurrencySymbol, data.ChangeCurrencySymbol);
                JToken responseJToken = JToken.Parse(exchangeResult);

                JToken responseDataToken = responseJToken["data"][data.ChangeCurrencySymbol]["value"];
                string rateResponse = responseDataToken.ToString();

                rate = Convert.ToDouble(rateResponse);
                exchangeValue = rate * data.Amount;

            }
            catch (Exception)
            {
                logger.LogCritical("Unable to get rate data from {apiUrl}", settings.CurrencyEcxhangeBaseUrl);
            }

            //apply 5% commission
            data.AmountToPay = exchangeValue * 1.05;
            data.Rate = rate;
            data.Commission = data.AmountToPay - exchangeValue;
            await Task.CompletedTask;

            return data;
        }
    }
}
