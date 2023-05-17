
namespace ExMoney.SharedLibs
{


    //FIXME: Fix Date format; save Date as is in database instead of numbers
    public class User
    {
        public string Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double BirthDate { get; set; } //FIXME: DateTime
        public string Sex { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }

        public string Address { get; set; }

        public string Country { get; set; }

        public double Balance { get; set; }

        public double CreationDate { get; set; }  //FIXME: DateTime

        public bool IdentityVerified { get; set; }

        public bool EmailVerified { get; set; }

        public bool PhoneVerified { get; set; }
    }

    public enum Sex
    {
        Male,
        Female
    }
}