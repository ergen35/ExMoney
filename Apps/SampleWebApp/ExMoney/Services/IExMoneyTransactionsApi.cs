using Refit;
using ExMoney.SharedLibs;
using ExMoney.SharedLibs.DTOs;

namespace ExMoney.Services;

public interface IExMoneyTransactionsApi
{
    [Get("/api/v1/transations/list/{userId}")]
    public Task<IApiResponse<List<Transaction>>> ListTransactions(string userId);

    [Post("/api/v1/transations/create")]
    public Task<IApiResponse<Transaction>> CreateTransaction(TransactionCreateDTO data);
}
