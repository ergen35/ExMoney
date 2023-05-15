using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExMoney.SharedLibs
{
    public class Currency
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(maximumLength: 6)]
        public string Symbol { get; set; }

        [Required, StringLength(maximumLength: 1024, MinimumLength = 2)]
        public string Name { get; set; }

        [Url]
        public string ValueProviderUrl { get; set; } = string.Empty;
    }
}
