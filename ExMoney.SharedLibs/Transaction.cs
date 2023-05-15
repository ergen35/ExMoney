using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace ExMoney.SharedLibs
{
    public class Transaction
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        public string BaseCurrency { get; set; }
        
        [Required]
        public string ChangeCurrency { get; set; }
        
        public DateTime TransactionDate { get; set; }
        
        [Required]
        public double Amount { get; set; } = 0.0;

        [Required]
        public double rate { get; set; } = 1.0;

        public TransactionStatus Status { get; set; }
        
        [Required]
        public string TransactionActorId { get; set; }
    }
}
