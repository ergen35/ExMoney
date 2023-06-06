using System;
using AutoMapper;
using ExMoney.Backend.Data;
using Microsoft.AspNetCore.Mvc;

namespace ExMoney.Backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PaymentsController
    {
        private readonly IMapper mapper;
        private readonly BackendDbContext db;

        public PaymentsController(IMapper mapper, BackendDbContext db)
        {
            this.mapper = mapper;
            this.db = db;
        }
    }
}
