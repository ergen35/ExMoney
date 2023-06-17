using System;
using AutoMapper;
using ExMoney.Backend.Data;
using ExMoney.SharedLibs;
using Microsoft.AspNetCore.Mvc;

namespace ExMoney.Backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class KycController: ControllerBase
    {
        private readonly IMapper mapper;
        private readonly BackendDbContext db;

        public KycController(IMapper mapper, BackendDbContext db)
        {
            this.mapper = mapper;
            this.db = db;
        }

        [HttpGet("get-status")]
        public ActionResult<KycVerification> GetKyc(string userId)
        {
            var kycResult = db.KycVerifications.FirstOrDefault(kyc => kyc.UserId == userId);
            return (kycResult is null) ? NotFound() : kycResult; 
        }
    }
}
