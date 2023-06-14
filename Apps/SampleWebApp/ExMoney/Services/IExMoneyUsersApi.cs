using ExMoney.SharedLibs;
using Refit;

namespace ExMoney.Services
{
    public interface IExMoneyUsersApi
    {
        [Get("/api/v1/users/get-user")]
        public Task<IApiResponse<User>> GetUserById(string id);
    }
}
