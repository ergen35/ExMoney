using AutoMapper;
using ExMoney.Backend.Data;
using ExMoney.SharedLibs;
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


        [HttpGet("get-user")]
        public ActionResult<User> GetUserById([FromQuery] string id)
        {
            var user = db.Users.Find(id);
            if(user is null)
                return NotFound();
            
            else return user;
        }
    }
}
