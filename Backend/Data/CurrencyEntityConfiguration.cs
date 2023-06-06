using Microsoft.EntityFrameworkCore;
using ExMoney.SharedLibs;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExMoney.Backend.Data
{
    public class CurrencyEntityConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            var nairaCurrency = new Currency
            {
                Id = 2,
                Name = "Naira",
                Symbol = "NGN"
            };

            var cfaCurrency = new Currency 
            {  
                Id = 1,
                Name = "FCFA",
                Symbol = "XOF"
            };

            builder.HasData(nairaCurrency, cfaCurrency);
        }
    }
}
