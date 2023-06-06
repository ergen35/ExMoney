using System;
using System.ComponentModel.DataAnnotations;

namespace ExMoney.SharedLibs
{
    public class PaymentOperation
    {
        [Key] public string Id { get; set; }
        public DateTime Date { get; set; }
        [Required] public string PaymentId { get; set; }
        public string Hash { get; set; }

        //Parent transaction
        public string ParentTransactionId { get; set; }
    }
}
