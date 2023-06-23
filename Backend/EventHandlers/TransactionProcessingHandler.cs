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
            var transactionId = context.Message.transactionId;


        }
    }
}
