using System;
using ExMoney.SharedLibs;
using Refit;

namespace ExMoney.Services
{
    public interface IExMoneyUsersApi
    {
        [Get("/api/v1/users/{userId}")]
        public Task<IApiResponse<User>> GteUserById(string userId);
    }
}
