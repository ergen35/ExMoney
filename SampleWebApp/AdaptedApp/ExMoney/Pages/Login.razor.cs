using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using ExMoney;
using ExMoney.Shared;
using ExMoney.Services;
using ExMoney.Pages.Shared;
using System.ComponentModel.DataAnnotations;

namespace ExMoney.Pages
{
    public partial class Login
    {
        [Inject] public AuthService authService { get; set; }
        [Inject] public ILogger<Login> logger { get; set; }
        [Inject] public NavigationManager navManager { get; set; }
        public LoginModel Input { get; set; } = new();
        public bool isSubmitting  = false;

        public async Task OnLoginSubmit(EditContext ctx)
        {
            isSubmitting = true; StateHasChanged();
            
            //delay
            await Task.Delay(TimeSpan.FromSeconds(3));

            var isValid = ctx.Validate();
            if(!isValid) {
                logger.LogError("Form is not valid");
                return;
            }

            //form is valid
            var token = await authService.Login(Input.EmailAddress, Input.Password, true);

            isSubmitting = false; StateHasChanged();

            if(!string.IsNullOrWhiteSpace(token))
            {
                navManager.NavigateTo("/account/dahsboard");
            }
        }
    }

    public class LoginModel
    {
        [Required, EmailAddress]
        public string EmailAddress { get; set; }

        [Required, DataType(DataType.Password), MinLength(6)]
        public string Password { get; set; }

        public bool isEmail { get; set; } = true; 
    }
}