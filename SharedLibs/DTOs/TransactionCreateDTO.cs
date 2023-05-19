using System.ComponentModel.DataAnnotations;

namespace ExMoney.SharedLibs.DTOs
{
    public class TransactionCreateDTO
    {
        [Required]
        public string BaseCurrencyId { get; set; }
        
        [Required]
        public string ChangeCurrencyId { get; set; }
                
        [Required]
        public double Amount { get; set; } = 0.0;
        
        [Required]
        public string TransactionActorId { get; set; }
    }
}
