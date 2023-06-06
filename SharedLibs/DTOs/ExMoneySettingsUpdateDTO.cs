namespace ExMoney.SharedLibs.DTOs
{
    public class ExMoneySettingsUpdateDTO
    {
        public double CommissionPercentage { get; set; }
        public string CurrencyExchangeApiKey { get; set; }
        public string CurrencyEcxhangeBaseUrl { get; set; }
        public bool EmailVerificationEnabled { get; set; }
        public bool IdentityVerificationEnabled { get; set; }
        public bool PhoneVerificationEnabled { get; set; }
    }
}
