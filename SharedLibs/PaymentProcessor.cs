using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExMoney.SharedLibs
{
    public class PaymentProcessor
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required] public string ApiKey { get; set; }

        [Required] public string SecretKey { get; set; }
        
        public string SupportedCurrencies { get; set; }
    }
}
