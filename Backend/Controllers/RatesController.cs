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
            double rate;
            double exchangeValue;

            try
            {
                string exchangeResult = fx.Latest(data.BaseCurrencySymbol, data.ChangeCurrencySymbol);
                JToken responseJToken = JToken.Parse(exchangeResult);

                JToken responseDataToken = responseJToken["data"][data.ChangeCurrencySymbol]["value"];
                string rateResponse = responseDataToken.ToString();

                rate = Convert.ToDouble(rateResponse);
                exchangeValue = rate * data.Amount;

                if (data.BaseCurrencySymbol.ToLower() == "xof")
                {
                    settings.LatestF2NRate = rate;
                }
                else
                {
                    settings.LatestN2FRate = rate;
                }

                db.Entry(settings).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _ = await db.SaveChangesAsync();

            }
            catch (Exception)
            {
                logger.LogCritical("Unable to get rate data from {apiUrl}.", settings.CurrencyEcxhangeBaseUrl);
                logger.LogWarning("Falling back to stored values.");

                rate = data.BaseCurrencySymbol.ToLower() == "xof" ? settings.LatestF2NRate : settings.LatestN2FRate;
                exchangeValue = rate * data.Amount;
            }


            //apply 5% commission
            data.AmountToPay = exchangeValue * 1.05;
            data.Rate = rate;
            data.Commission = data.AmountToPay - exchangeValue;

            return data;
        }
    }
}
