using AutoMapper;
using ExMoney.Backend.Data;
using ExMoney.Backend.Events;
using ExMoney.SharedLibs;
using ExMoney.SharedLibs.DTOs;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace ExMoney.Backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IBus bus;
        private readonly BackendDbContext db;
        private readonly ILogger logger;
        private readonly HttpClient httpClient;
        private readonly IConfiguration config;

        public UsersController(IMapper mapper, 
                                IBus bus,
                                BackendDbContext db, 
                                ILogger<UsersController> logger, 
                                HttpClient httpClient, 
                                IConfiguration config)
        {
            this.mapper = mapper;
            this.bus = bus;
            this.db = db;
            this.logger = logger;
            this.httpClient = httpClient;
            this.config = config;
        }


        [HttpGet("get-user")]
        public ActionResult<User> GetById([FromQuery] string id)
        {
            var user = db.Users.Find(id);
            if (user is null)
                return NotFound();

            else return user;
        }

        [HttpGet("get-user-by-username")]
        public ActionResult<User> GetByUsername([FromQuery] string username)
        {
            var user = db.Users.FirstOrDefault(u => u.Email == username);
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

                return new ObjectResult(new ProblemDetails
                {
                    Status = 500,
                    Detail = resMsg,
                    Title = "Une erreur s'est produite"
                });
            }

            var user = mapper.Map<User>(data);

            db.Users.Add(user);
            await db.SaveChangesAsync();

            //publis user registered event
            // var sep = await bus.GetPublishSendEndpoint<UserRegisteredEvent>();

            logger.LogInformation("Publishing {event}", nameof(UserRegisteredEvent));
            await bus.Publish(new UserRegisteredEvent(user.Id, user.Email));

            return Ok("success registration");
        }
    }
}
