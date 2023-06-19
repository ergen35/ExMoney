using ExMoney.SharedLibs;
using Refit;

namespace ExMoney.Services
{
    public interface IExMoneyWalletsApi
    {
        
        [Get("/api/v1/wallets/user-wallets")]
        public Task<IApiResponse<List<Wallet>>> GetUserWallets(string userId);
    }
}
