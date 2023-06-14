using Microsoft.EntityFrameworkCore;
using ExMoney.SharedLibs;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExMoney.Backend.Data
{
    public class WalletEntityConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            var nairaWallet = new Wallet
            {

                Id = "001066c3-457b-4b3c-a942-b4ebca1afeb2",
                Name = "Réserve Naira",
                OwnerId = "exmoney-system",
                Balance = 26400,
                CurrencyId = 2,
            };

            var cfaWallet = new Wallet
            {
                Id = "bab8e547-7f6e-4955-976f-80b9c4a2298e",
                Name = "Réserve CFA",
                Balance = 13674,
                OwnerId = "exmoney-system",
                CurrencyId = 1,
            };

            //Add data
            _ = builder.HasData(nairaWallet, cfaWallet);


            var wassiXOFWallet = new Wallet()
            {
                Id = "f07ce690-336e-45dd-aee7-11dac71b37e4",
                Name = "XOF Wallet",
                Balance = 27000d,
                OwnerId = "9fce8cc5-4017-4920-ab4c-1ff0ff06f4af",
                CurrencyId = 1
            };

            var wassiNGNWallet = new Wallet()
            {
                Id = "b4bca84f-0520-4c99-97d1-433d4df7d802",
                Name = "NGN Wallet",
                Balance = 0d,
                OwnerId = "9fce8cc5-4017-4920-ab4c-1ff0ff06f4af",
                CurrencyId = 2
            };

            //Add data
            _ = builder.HasData(wassiNGNWallet, wassiXOFWallet);
        }
    }
}
