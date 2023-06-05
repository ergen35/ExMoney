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
                CommissionPercentage = 0.05,
                Id = 1,
                EmailVerificationEnabled = false,
                IdentityVerificationEnabled = false,
                PhoneVerificationEnabled = false
            };

            builder.HasData(settings);
        }
    }
}
