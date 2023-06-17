using System;

namespace ExMoney.SharedLibs.DTOs
{
    public class UserRegisterDTO
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime BirthDate { get; set; }
        
        public string Email { get; set; }
        
        public string Country { get; set; }
        public string Address { get; set; }

    }
}
