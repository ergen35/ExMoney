using System;
using System.ComponentModel.DataAnnotations;

namespace ExMoney.SharedLibs.DTOs
{
    public class UserRegisterDTO
    {
        [Required, StringLength(maximumLength: 256, MinimumLength = 2)]
        public string FirstName { get; set; }

        
        [Required, StringLength(maximumLength: 256, MinimumLength = 2)]
        public string LastName { get; set; }
        
        [DataType(DataType.Date)] 
        public DateTime BirthDate { get; set; }
        
        [Required] public string Phone { get; set; }
        
        [Required, EmailAddress] 
        public string Email { get; set; }

        [Required, StringLength(maximumLength: 128, MinimumLength = 6)] 
        public string Password { get; set; }
        
        
        public string Country { get; set; }
        public string Address { get; set; }
    }
}
