using ExMoney.Backend.Data;
using ExMoney.SharedLibs.DTOs;
using Microsoft.AspNetCore.Mvc;
using currencyapi;
using Newtonsoft.Json.Linq;

namespace ExMoney.Backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RatesController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public RatesController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost("calculate")]
        public async Task<ActionResult<ExchangeRate>> CalculateRate(ExchangeRate data)
        {
            //find currencies
            string apikey = configuration["ExchangeApi:Key"];
            var fx = new Currencyapi(apikey);

            var exchangeResult = fx.Latest(data.BaseCurrencySymbol, data.ChangeCurrencySymbol);
            var responseJToken = JToken.Parse(exchangeResult);

            var responseDataToken = responseJToken["data"];
            var currencyChangeToken = responseDataToken[data.ChangeCurrencySymbol];
            var rateResponse = currencyChangeToken["value"].ToString();

            double rate = Convert.ToDouble(rateResponse);

            var exchangeValue = rate * data.Amount;
            
            //apply 5% commission
            data.AmountToPay = exchangeValue * 1.05;
            data.Rate = rate;
            data.Commission = data.AmountToPay - exchangeValue;

            return data;
        }
    }
}
