using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;

namespace ExMoney.Authenticator
{
    public class AppAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal currentUser = new();
        private readonly KeycloakAuthenticator authClient;
        private readonly ILogger logger;

        public AppAuthenticationStateProvider(KeycloakAuthenticator authClient, ILogger<AppAuthenticationStateProvider> logger)
        {
            this.authClient = authClient;
            this.logger = logger;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(new AuthenticationState(currentUser));
        }

        public async Task<bool> LogInAsync(string username, string password)
        {

            (ClaimsIdentity identity, bool isAuthenticated) = await LoginWithIdPAsync(username, password);
            currentUser = new ClaimsPrincipal(identity);
            currentUser.AddIdentity(identity);
            
            // logger.LogError("Current User Claims {user}", JsonSerializer.Serialize(currentUser.Identity));
            return isAuthenticated;
        }

        public void NotifyAuthStateChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        private async Task<(ClaimsIdentity claims, bool isAuthenticated)> LoginWithIdPAsync(string username, string password)
        {
            ClaimsIdentity claimsIdentity = new();

            (bool isSusscess, ClaimsIdentity identity) = await authClient.LoginAsync(username, password);

            if (isSusscess)
            {
                claimsIdentity = identity;
            }

            return (claimsIdentity, isSusscess);
        }

        public async void LogOut()
        {
            await authClient.LogoutAsync();
            currentUser = new ClaimsPrincipal();
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(currentUser)));
        }
    }
}
