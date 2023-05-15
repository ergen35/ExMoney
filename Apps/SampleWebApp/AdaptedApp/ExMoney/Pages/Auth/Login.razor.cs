using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ExMoney.Services;
using System.ComponentModel.DataAnnotations;

namespace ExMoney.Pages.Auth;

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
       try
       {
         var token = await authService.Login(Input.EmailAddress, Input.Password, true);
       }
       catch (System.Exception)
       {
            isSubmitting = false;
            StateHasChanged();
       }

        isSubmitting = false; StateHasChanged();

        //FIXME: verification disabled
        // if(!string.IsNullOrWhiteSpace(token))
        // {
        //     navManager.NavigateTo("/account/dashboard");
        // }

        navManager.NavigateTo("/account/dashboard");
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