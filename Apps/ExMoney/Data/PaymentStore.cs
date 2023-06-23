using System;

namespace ExMoney.Data
{
    public static class PaymentStore
    {
        public static double Amount = 0d;
        public static double Rate = 0d;
        public static DateTime Date;
        public static int BaseCurrencyId;
        public static int ChangeCurrencyId;
        public static string TransactionId;

    }
}
