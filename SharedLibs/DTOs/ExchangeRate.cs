using System.ComponentModel.DataAnnotations;

namespace ExMoney.SharedLibs.DTOs
{
    public class ExchangeRate
    {
        [Required, StringLength(maximumLength: 6, MinimumLength = 3)] 
        public string BaseCurrencySymbol { get; set; }
        [Required, StringLength(maximumLength: 6, MinimumLength = 3)] 
        public string ChangeCurrencySymbol { get; set; }
        [Required,] public double Amount { get; set; }


        public double Rate { get; set; }
        public double Commission { get; set; }
        public double AmountToPay { get; set; }
    }
}
