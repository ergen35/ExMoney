using ExMoney.SharedLibs;
using ExMoney.SharedLibs.DTOs;
using AutoMapper;
using ExMoney.Backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExMoney.Backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TransactionsController: ControllerBase
    {
        private readonly IMapper mapper;
        private readonly BackendDbContext db;

        public TransactionsController(IMapper mapper, BackendDbContext db)
        {
            this.mapper = mapper;
            this.db = db;
        }

        [HttpGet("list")]
        public IEnumerable<Transaction> List(string userId)
        {
            return db.Transactions.Where(t => t.UserId == userId);

        }

        [HttpGet("latest")]
        public IEnumerable<Transaction> ListLatests(string userId, int count)
        {
            var latestTransactions = db.Transactions.Where(t => t.UserId == userId).OrderByDescending(t => t.Date).Take(count);
            return latestTransactions;
        }

        [HttpGet("ongoing")]
        public IEnumerable<Transaction> ListOngoing(string userId, int count)
        {
            var ongoingTransactions = db.Transactions.Where(t => t.UserId == userId && t.Status == TransactionStatus.Processing).Take(count);
            return ongoingTransactions;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Transaction>> Add(TransactionCreateDTO data)
        {
            //TODO: Do check user's existence
            var transaction = mapper.Map<Transaction>(data);
            transaction.Status = TransactionStatus.Processing;

            try
            {
                db.Transactions.Add(transaction);
                await db.SaveChangesAsync();
                //TODO: propagate transaction created, prompt for payment  
            }
            catch (System.Exception)
            {
                return new ObjectResult(new ProblemDetails
                {
                    Status = 500,
                    Title = "Unknow error"
                });
            }

            return Created(nameof(Add), transaction);
        }

    }
}
