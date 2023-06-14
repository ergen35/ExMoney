using System;
using ExMoney.SharedLibs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExMoney.Backend.Data
{
    public class TestUserEntityConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var user = new User {
                Id = "9fce8cc5-4017-4920-ab4c-1ff0ff06f4af",
                Address = "Porto-Novo, Bénin",
                BirthDate = new DateTime(2000, 6, 14),
                Country = "Bénin",
                CreationDate = new DateTime(2023, 4, 29),
                Email = "wassi@gmail.com",
                EmailVerified = true,
                FirstName = "wassi",
                LastName = "harif",
                Phone = "+22990210790",
                PhoneVerified = true,
                Sex = Sex.Male,
                Points = 45
            };

            _ = builder.HasData(user);
        }
    }
}
