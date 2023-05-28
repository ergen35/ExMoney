using System;

namespace ExMoney.Data
{
    public static class Utilities
    {
        public static string GetTableIconCss(string currency)
        {
            if(currency.Contains("naira", StringComparison.OrdinalIgnoreCase))
                return "ti ti-currency-naira";
            
            if(currency.Contains("fran", StringComparison.OrdinalIgnoreCase))
                return "ti ti-currency-frank";

            return string.Empty;
        }
    }
}
