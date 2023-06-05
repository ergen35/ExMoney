using Microsoft.EntityFrameworkCore;
using ExMoney.SharedLibs;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExMoney.Backend.Data
{
    public class WalletEntityConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            Wallet nairaWallet = new Wallet
            {
                Id = "", //TODO: Define constant Guid
                Balance = 26400,
                CurrencyId = 2,
                Name = "Réserve Naira"
            };

            Wallet cfaWallet = new Wallet
            {
                Id = "", //TODO: Define constant Guid
                Balance = 13674,
                CurrencyId = 1,
                Name = "Réserve CFA"
            };

            _ = builder.HasData(nairaWallet, cfaWallet);
        }
    }
}
