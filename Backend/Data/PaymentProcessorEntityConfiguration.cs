using Microsoft.EntityFrameworkCore;
using ExMoney.SharedLibs;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExMoney.Backend.Data
{
    public class PaymentProcessorEntityConfiguration : IEntityTypeConfiguration<PaymentProcessor>
    {
        public void Configure(EntityTypeBuilder<PaymentProcessor> builder)
        {
            var kkiapay = new PaymentProcessor{
                Id = "", //TODO: Constant
                ApiKey = "k",
                Name = "KkiaPay",
                SecretKey = "",
                SupportedCurrencies = "xof"
            };
        }
    }
}
