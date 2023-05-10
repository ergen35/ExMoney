using ExMoney.Models;
using System.Net.Http;

namespace ExMoney.Services; 


public class Backend
{
    private readonly ILogger<Backend> logger;
    private readonly HttpClient client;

    public Backend(ILogger<Backend> logger, HttpClient client, IConfiguration configuration)
    {
        this.logger = logger;
        this.client = client;
        client.BaseAddress = new Uri(configuration["BackendBaseAddress"]);
    }

    #region Transactions

    public async Task<List<Transaction>> ListTransactions(string userId)
    {
        var response = await client.GetAsync($"/api/v1/transations/list/{userId}");
        if(response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<List<Transaction>>();            
        
        return new List<Transaction>();
    }

    #endregion

    #region Users

    public async Task<User> GetUserById(string id)
    {
        var response = await client.GetAsync($"/api/v1/users/getById?id={id}");

        if(response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<User>();
        
        return null;
    }

    #endregion
}