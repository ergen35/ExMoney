using System;
using Refit;
using ExMoney.Shared;
using ExMoney.SharedLibs;

namespace ExMoney.Services
{
    public interface IExMoneyKycStatusApi
    {
        [Get("/api/v1/kyc/get-status")]
        public Task<IApiResponse<KycVerification>> GetKycStatus(string userId);
    }
}
