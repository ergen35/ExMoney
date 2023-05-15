using System.ComponentModel.DataAnnotations;

namespace ExMoney.SharedLibs.DTOs
{
    public class CurrencyCreateDTO
    {
        [Required, StringLength(maximumLength: 6)]
        public string Symbol { get; set; }

        [Required, StringLength(maximumLength: 1024, MinimumLength = 2)]
        public string Name { get; set; }

        [Url]
        public string ValueProviderUrl { get; set; } = string.Empty;
    }
}
