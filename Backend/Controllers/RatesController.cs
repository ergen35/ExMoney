using System;
using ExMoney.Backend.Data;
using ExMoney.SharedLibs.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ExMoney.Backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RatesController: ControllerBase
    {
        private readonly BackendDbContext db;

        public RatesController(BackendDbContext db)
        {
            this.db = db;
        }

        [HttpPost("calculate")]
        public async Task<ActionResult<ExchangeRate>> CalculatreRate(ExchangeRate data)
        {
            //find currencies
            var baseCurrency = await db.Currencies.FindAsync(data.BaseCurrencyId);
            var changeCurrency = await db.Currencies.FindAsync(data.ChangeCurrencyId);

            //TODO: make request to external api

            return data;
        }
    }
}
