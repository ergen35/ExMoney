using ExMoney.SharedLibs;
using Refit;

namespace ExMoney.Services
{
    public interface IExMoneyUsersApi
    {
        [Get("/api/v1/users/get-user")]
        public Task<IApiResponse<User>> GetUserById(string id);

        [Get("/api/v1/users/get-user-by-username")]
        public Task<IApiResponse<User>> GetUserByUsername(string username);
    }
}
