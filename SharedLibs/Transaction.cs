using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace ExMoney.SharedLibs
{
    public class Transaction
    {
        public Currency BaseCurrency;
        public Currency ChangeCurrency;


        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        public string BaseCurrencyId { get; set; }

        [Required]
        public string ChangeCurrencyId { get; set; }
        
        public DateTime TransactionDate { get; set; }
        
        [Required]
        public double Amount { get; set; } = 0.0;

        [Required]
        public double Rate { get; set; } = 1.0;

        public TransactionStatus Status { get; set; }
        
        [Required]
        public string TransactionActorId { get; set; }
    }
}
