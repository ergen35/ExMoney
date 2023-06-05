using System.ComponentModel.DataAnnotations;

namespace ExMoney.SharedLibs
{
    public class ExMoneySettings
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public double CommissionPercentage { get; set; } = 0.05;

        public bool EmailVerificationEnabled { get; set; } = false;
        public bool IdentityVerificationEnabled { get; set; } = false;
        public bool PhoneVerificationEnabled { get; set; } = false;
    }
}
