using System;

namespace ExMoney.Data
{
    public static class Utilities
    {
        public static string GetTableIconCss(string symbol)
        {
            if(symbol.Contains("ngn", StringComparison.OrdinalIgnoreCase))
                return "ti ti-currency-naira";
            
            if(symbol.Contains("xof", StringComparison.OrdinalIgnoreCase))
                return "ti ti-currency-frank";

            return string.Empty;
        }
    }
}
