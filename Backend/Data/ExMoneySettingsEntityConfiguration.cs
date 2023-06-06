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
                PhoneVerificationEnabled = false
            };

            builder.HasData(settings);
        }
    }
}
