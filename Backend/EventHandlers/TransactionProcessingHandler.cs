using System;
using ExMoney.Backend.Data;
using ExMoney.Backend.Events;
using MassTransit;

namespace ExMoney.Backend.EventHandlers
{
    public class TransactionProcessingHandler : IConsumer<TransactionCreatedEvent>
    {
        private readonly BackendDbContext db;

        public TransactionProcessingHandler(BackendDbContext db)
        {
            this.db = db;
        }
        public async Task Consume(ConsumeContext<TransactionCreatedEvent> context)
        {
            var transaction = await db.Transactions.FindAsync(context.Message.transactionId);
            if (transaction is null) return;

            var user = await db.Users.FindAsync(context.Message.userId);
            if (user is null) return;

            var baseWallet = db.Wallets.FirstOrDefault(w => w.OwnerId == user.Id && w.CurrencyId == transaction.BaseCurrencyId);
            if (baseWallet is null) return;

            baseWallet.Balance += transaction.Amount;
            db.Entry(baseWallet).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await db.SaveChangesAsync();

            await Task.Delay(TimeSpan.FromSeconds(15));

            //balance change
            baseWallet.Balance -= transaction.Amount;
            db.Entry(baseWallet).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await db.SaveChangesAsync();

            //update transaction status
            transaction.Status = SharedLibs.TransactionStatus.Finished;
            db.Entry(transaction).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
