using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExMoney.SharedLibs
{
    public class Wallet
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public string Id { get; set; }
        [Required] public string Name { get; set; }
        
        [Required] public string OwnerId { get; set; }

        [Required] public int CurrencyId { get; set; }
        public double Balance { get; set; } = 0d;
    }
}
