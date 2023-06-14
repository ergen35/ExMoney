using Microsoft.EntityFrameworkCore;
using ExMoney.SharedLibs;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExMoney.Backend.Data
{
    public class ExMoneySettingsEntityConfiguration : IEntityTypeConfiguration<ExMoneySettings>
    {
        public void Configure(EntityTypeBuilder<ExMoneySettings> builder)
        {
            var settings = new ExMoneySettings
            {
                Id = 1,
                CurrencyExchangeApiKey = "STNcvlsyq6QpULgJhQYKqKqym6YI5MjrdPBalf5x",
                CurrencyEcxhangeBaseUrl = "http://currencyapi.com",
                CommissionPercentage = 0.05,
                EmailVerificationEnabled = false,
                IdentityVerificationEnabled = false,
                PhoneVerificationEnabled = false,

                LatestF2NRate = 1.32,
                LatestN2FRate = 0.85
            };

            builder.HasData(settings);
        }
    }
}
