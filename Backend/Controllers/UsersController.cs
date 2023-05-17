using AutoMapper;
using ExMoney.Backend.Data;
using Microsoft.AspNetCore.Mvc;

namespace ExMoney.Backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController: ControllerBase
    {
        private readonly IMapper mapper;
        private readonly BackendDbContext db;
        private readonly ILogger logger;

        public UsersController(IMapper mapper, BackendDbContext db, ILogger<UsersController> logger)
        {
            this.mapper = mapper;
            this.db = db;
            this.logger = logger;
        }  


    }
}
