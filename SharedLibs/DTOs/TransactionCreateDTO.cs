using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;



namespace ExMoney.SharedLibs.DTOs
{
    public class TransactionCreateDTO
    {
        [Required]
        public string BaseCurrency { get; set; }
        
        [Required]
        public string ChangeCurrency { get; set; }
                
        [Required]
        public double Amount { get; set; } = 0.0;
        
        [Required]
        public string TransactionActorId { get; set; }
    }
}
