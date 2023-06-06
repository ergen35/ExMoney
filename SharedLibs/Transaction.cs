using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace ExMoney.SharedLibs
{
    public class Transaction
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public DateTime TransactionDate { get; set; }
        [Required] public double Amount { get; set; } = 0.0;
        [Required] public double Rate { get; set; } = 1.0;
        public TransactionStatus Status { get; set; } = TransactionStatus.NoStatus;
        [Required] public string UserId { get; set; }

        //Navigation properties
        public Currency BaseCurrency { get; set; }
        public Currency ChangeCurrency { get; set; }

        //Paid in
        public string PayInId { get; set; }
        public PaymentOperation PayIn { get; set; }

        //Paid Out
        public string PayOutId { get; set; }
        public PaymentOperation PayOut { get; set; }
    }

}
