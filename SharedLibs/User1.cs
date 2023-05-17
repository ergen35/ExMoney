using System;

namespace ExMoney.SharedLibs
{
    public class User1
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string PreferredCurrency { get; set; }
        public double Balance { get; set; }
        public DateTime RegistrationDate { get; set; }
    
        public bool IdentityVerified { get; set; } = false;
        public bool EmailVerified { get; set; } =  false;
        public bool PhoneVerified { get; set; } = false;
    }
}
