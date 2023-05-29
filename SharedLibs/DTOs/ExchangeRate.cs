using System.ComponentModel.DataAnnotations;

namespace ExMoney.SharedLibs.DTOs
{
    public class ExchangeRate
    {
        [Required] public int BaseCurrencyId { get; set; }
        [Required] public int ChangeCurrencyId { get; set; }
        [Required] public double Amount { get; set; }


        public double Rate { get; set; }
        public double Commission { get; set; }
        public double AmountToPay { get; set; }
    }
}
