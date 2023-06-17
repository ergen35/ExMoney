using AutoMapper;
using ExMoney.Backend.Data;
using ExMoney.SharedLibs;
using ExMoney.SharedLibs.DTOs;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

namespace ExMoney.Backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly BackendDbContext db;
        private readonly ILogger logger;
        private readonly HttpClient httpClient;
        private readonly IConfiguration config;

        public UsersController(IMapper mapper, BackendDbContext db, ILogger<UsersController> logger, HttpClient httpClient, IConfiguration config)
        {
            this.mapper = mapper;
            this.db = db;
            this.logger = logger;
            this.httpClient = httpClient;
            this.config = config;
        }


        [HttpGet("get-user")]
        public ActionResult<User> GetUserById([FromQuery] string id)
        {
            var user = db.Users.Find(id);
            if (user is null)
                return NotFound();

            else return user;
        }

        [HttpPost("register-user")]
        public async Task<ActionResult> RegisterUser(UserRegisterDTO data)
        {
            //data are valid.
            //register user into provider
            httpClient.BaseAddress = new Uri(config["ServerAddresses:AuthServer"]);

            var response = await httpClient.PostAsJsonAsync("/idsrv/testusers/provision-user", data);

            if (!response.IsSuccessStatusCode)
            {
                var resMsg = await response.Content.ReadAsStringAsync();

                return new ObjectResult(new ProblemDetails{
                    Status = 500,
                    Detail = resMsg,
                    Title = "Une erreur s'est produite"
                });
            }

            _ = await response.Content.ReadAsStringAsync();

            var user = mapper.Map<User>(data);

            db.Users.Add(user);
            await db.SaveChangesAsync();

            return Ok("success registration");
        }
    }
}
