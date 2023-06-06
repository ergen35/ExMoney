using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace ExMoney.SharedLibs
{
    public class Transaction
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public DateTime Date { get; set; }
        [Required] public double Amount { get; set; } = 0.0;
        [Required] public double Rate { get; set; } = 1.0;
        public TransactionStatus Status { get; set; } = TransactionStatus.NoStatus;

        //Navigation properties
        public Currency BaseCurrency { get; set; }
        public Currency ChangeCurrency { get; set; }

        //User making the transaction
        [Required] public string UserId { get; set; }
        public User User { get; set; }

        //PayIn
        public string PayInId { get; set; }
        public PaymentOperation PayIn { get; set; }

        //PayOut
        public string PayOutId { get; set; }
        public PaymentOperation PayOut { get; set; }
    }

}
