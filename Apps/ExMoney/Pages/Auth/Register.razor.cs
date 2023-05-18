using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;

namespace ExMoney.Pages.Auth;

public partial class Register
{
    [Inject] public NavigationManager navigationManager { get; set; }

    public RegisterModel Input { get; set; } = new();

    private bool isSubmitting = false;

    public Task OnRegisterSubmit()
    {
        return Task.CompletedTask;
    }
}



public class RegisterModel
{
    [Required, StringLength(maximumLength: 250, MinimumLength = 2)]
    public string FirstName { get; set; }

    [Required, StringLength(maximumLength: 250, MinimumLength = 2)]
    public string LastName { get; set; }

    [Required, DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }

    [Required, EnumDataType(typeof(SexEnum))]
    public string Sex { get; set; } = SexEnum.Male.ToString();

    public string Phone { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string Country { get; set; }

    public string Password { get; set; }
    public string PasswordConfirmation { get; set; }
}

public enum SexEnum { Male, Female }