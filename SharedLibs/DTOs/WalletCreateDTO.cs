using System;
using System.ComponentModel.DataAnnotations;

namespace ExMoney.SharedLibs.DTOs
{
    public class WalletCreateDTO
    {
        [Required] public string Name { get; set; }
        [Required] public int CurrencyId { get; set; }
    }
}
