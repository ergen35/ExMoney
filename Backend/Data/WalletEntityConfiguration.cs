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

                Id = "001066c3-457b-4b3c-a942-b4ebca1afeb2",
                Name = "Réserve Naira",
                OwnerId = "exmoney-system",
                Balance = 26400,
                CurrencyId = 2,
            };

            Wallet cfaWallet = new Wallet
            {
                Id = "bab8e547-7f6e-4955-976f-80b9c4a2298e",
                Name = "Réserve CFA",
                Balance = 13674,
                OwnerId = "exmoney-system",
                CurrencyId = 1,
            };

            _ = builder.HasData(nairaWallet, cfaWallet);
        }
    }
}
