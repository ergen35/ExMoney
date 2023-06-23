using Refit;
using ExMoney.SharedLibs;
using ExMoney.SharedLibs.DTOs;

namespace ExMoney.Services;

public interface IExMoneyTransactionsApi
{
    [Get("/api/v1/transactions/list")]
    public Task<IApiResponse<List<Transaction>>> List(string userId);

    [Post("/api/v1/transactions/create")]
    public Task<IApiResponse<Transaction>> Create(TransactionCreateDTO data);

    [Get("/api/v1/transactions/ongoing")]
    public Task<IApiResponse<List<Transaction>>> ListOngoing(string userId, int count = 5);
    
    [Get("/api/v1/transactions/latest")]
    public Task<IApiResponse<List<Transaction>>> ListLastest(string userId, int count = 5);
}
