using System;
using System.ComponentModel.DataAnnotations;

namespace ExMoney.SharedLibs.DTOs
{
    public class TransactionCreateDTO
    {
        [Required] public int BaseCurrencyId { get; set; }
        [Required] public int ChangeCurrencyId { get; set; }
        [Required] public double Amount { get; set; } = 0.0;
        [Required] public string UserId { get; set; }
        [Required] public string TransactionId { get; set; }
        [Required] public double Rate { get; set; }
        [Required] public DateTime Date { get; set; }
    }
}
