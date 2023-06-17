using System;
using ExMoney.Backend.Data;
using ExMoney.Backend.Events;
using ExMoney.SharedLibs;
using MassTransit;

namespace ExMoney.Backend.EventHandlers
{
    public class UserRegisteredHandler : IConsumer<UserRegisteredEvent>
    {
        private readonly BackendDbContext db;
        private readonly ILogger<UserRegisteredHandler> logger;

        public UserRegisteredHandler(BackendDbContext db, ILogger<UserRegisteredHandler> logger)
        {
            this.db = db;
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<UserRegisteredEvent> context)
        {
            var user = await db.Users.FindAsync(context.Message.userId);
            if(user is null)
                return;

            logger.LogInformation("Processing {event} with {id}.", context.Message.GetType().Name, context.MessageId);

            //create wallets
            var ngnWallet = new Wallet{
                OwnerId = context.Message.userId,
                CurrencyId = 2,
                Name = $"ngn-wallet-{context.Message.username}",
            };

            var xofWallet = new Wallet{
                OwnerId = context.Message.userId,
                CurrencyId = 1,
                Name = $"xof-wallet-{context.Message.username}",
            };

            await db.Wallets.AddRangeAsync(xofWallet, ngnWallet);
            
            //create Kyc
            var kyc = new KycVerification{
                UserId = user.Id,
                VerificationResult = KycVerificationResult.NoStatus
            };

            await db.KycVerifications.AddAsync(kyc);

            //TODO: Send email confirmation
            
            await db.SaveChangesAsync();
            logger.LogInformation("Created wallet {walletName}.", xofWallet.Name);
            logger.LogInformation("Created wallet {walletName}.", ngnWallet.Name);
            
            logger.LogInformation("Processed {event} with {id}.", context.Message.GetType().Name, context.MessageId);
        }
    }
}
