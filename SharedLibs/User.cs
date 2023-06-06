
using System;
using System.ComponentModel.DataAnnotations;

namespace ExMoney.SharedLibs
{
    public class User
    {
        public string Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
        
        [EnumDataType(typeof(Sex))]
        public Sex Sex { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }

        public string Address { get; set; }

        public string Country { get; set; }

        public double Balance { get; set; }

        public DateTime CreationDate { get; set; }

        public KycVerification KycVerification { get; set; }

        public bool EmailVerified { get; set; }

        public bool PhoneVerified { get; set; }
    }
}