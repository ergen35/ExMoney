using AutoMapper;
using ExMoney.Backend.Data;
using ExMoney.SharedLibs;
using ExMoney.SharedLibs.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ExMoney.Backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]

    public class WalletsController: ControllerBase
    {
        private readonly BackendDbContext db;
        private readonly IMapper mapper;

        public WalletsController(BackendDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        } 

        [HttpGet("{id}")]
        public async Task<ActionResult<Wallet>> GetById(string id)
        {
            var wallet = await db.ExMoneyWallets.FindAsync(id);
            if(wallet is null)
                return NotFound();
            return wallet;
        }  

        [HttpGet]
        public async Task<ActionResult<List<Wallet>>> List()
        {
            var wallets = db.ExMoneyWallets.ToList();
            if(wallets is null)
                return NotFound();
            await Task.CompletedTask;
            return wallets;
        }

        [HttpPost]
        public async Task<ActionResult<Wallet>> Create(WalletCreateDTO data)
        {
            var currency = await db.Currencies.FindAsync(data.CurrencyId);
            if(currency is null)
                return BadRequest(new  { Error = "" });
            var existingWallet = db.ExMoneyWallets.FirstOrDefault(w => w.CurrencyId == currency.Id);
            if(existingWallet is not null)
                return BadRequest(new { Error = "Un portefeuille avec la devise spécifiée existe déja." });

            var wallet = mapper.Map<Wallet>(data);

            var addResult = await db.ExMoneyWallets.AddAsync(wallet);
            await db.SaveChangesAsync();

            return wallet;
        }
    }
}
