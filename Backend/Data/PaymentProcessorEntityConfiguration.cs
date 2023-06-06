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
                Id = "8ce50226-1307-4de8-aff7-d839042dec53",
                ApiKey = "k",
                Name = "KkiaPay",
                SecretKey = "",
                SupportedCurrencies = "xof"
            };

            builder.HasData(kkiapay);
        }
    }
}
