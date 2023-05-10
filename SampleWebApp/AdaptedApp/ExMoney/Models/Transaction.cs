namespace ExMoney.Models;


public class Transaction
{
    public string Id { get; set; }

    public string BasCurrency { get; set; }

    public string ChangeCurrency { get; set; }

    public DateTime TransactionDate { get; set; }

    public double Amount { get; set; }

    public double Rate { get; set; }
    
    // value inferred from TransactionStatus enum    
    public string Status { get; set; }

}


public enum TransactionStatus
{
    Accepted,
    Processing,
    Finished,
    Rejected
}
