using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExMoney.SharedLibs
{
    public class Currency
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(maximumLength: 3, MinimumLength = 3)]
        public string Symbol { get; set; }

        [Required, StringLength(maximumLength: 1024, MinimumLength = 5)]
        public string Name { get; set; }
    }
}

