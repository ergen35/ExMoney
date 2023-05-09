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
using ExMoney.Pages.Shared;
using System.ComponentModel.DataAnnotations;

namespace ExMoney.Pages
{
    public partial class Login
    {
        public LoginModel Input { get; set; } = new();


        public void OnValidSubmit(EditContext ctx)
        {
            
        }
    }

    public class LoginModel
    {
        [Required, Phone]
        public string Phone { get; set; }

        [Required, EmailAddress]
        public string EmailAddress { get; set; }

        [Required, DataType(DataType.Password), MinLength(6)]
        public string Password { get; set; }

        public bool isEmail { get; set; } = true; 
    }
}