using System;

namespace ExMoney.Backend.Events
{
    public record class UserRegisteredEvent(string userId, string username);
}
