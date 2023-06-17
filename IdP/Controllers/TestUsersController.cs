using System;
using System.Security.Claims;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Test;
using Duende.IdentityServer.Validation;
using ExMoney.SharedLibs.DTOs;
using IdentityModel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace IdP.Controllers
{
    [ApiController]
    [Route("idsrv/[controller]")]
    public class TestUsersController: ControllerBase
    {
        private readonly IProfileService profileService;
        private readonly TestUserStore userStore;
        private readonly IResourceOwnerPasswordValidator ropcValidator;

        public TestUsersController(IProfileService profileService, TestUserStore userStore, IResourceOwnerPasswordValidator ropcValidator)
        {
            this.profileService = profileService;
            this.userStore = userStore;
            this.ropcValidator = ropcValidator;
        }

        [HttpPost("provision-user")]
        public ActionResult<string> CreateUser(UserRegisterDTO data)
        {
            //find user by email
            var existingUser = userStore.FindByUsername(data.Email);
            if(existingUser is not null)
                return BadRequest("existing user");

            var userId = Guid.NewGuid().ToString();

            var newUser = userStore.AutoProvisionUser("idsrv6", userId,
            new List<Claim>()
            {
                new Claim(JwtClaimTypes.Role, "app-user"),
                new Claim(JwtClaimTypes.PreferredUserName, data.Email),
            });

            newUser.IsActive = true;
            newUser.Password = data.Password;
            newUser.Username = data.Email;

            //Add user to store
            IdpConfiguration.Users.Add(newUser);
            
            return Ok(newUser.Username);
        }
    }
}
