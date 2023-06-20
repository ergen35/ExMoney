using System;
using ExMoney.SharedLibs.DTOs;
using Refit;

namespace ExMoney.Services
{
    public interface IExMoneyAuthApi
    {
        [Post("/api/v1/users/register-user")]
        public Task<IApiResponse<string>> RegisterUserAsync(UserRegisterDTO data);
    }
}
