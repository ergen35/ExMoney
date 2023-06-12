using System.Security.Claims;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace ExMoney.Authenticator
{
    public class KeycloakAuthenticator
    {
        private readonly IdpAuthenticationOptions options;
        private readonly IDiscoveryCache discoveryCache;

        public KeycloakAuthenticator(IOptions<IdpAuthenticationOptions> IdpOptions, IDiscoveryCache discoveryCache)
        {
            options = IdpOptions.Value;
            this.discoveryCache = discoveryCache;
        }

        public async Task<(bool isSusscess, string accessToken, string refreshToken, string idToken, ClaimsIdentity claims)> LoginAsync(string username, string password)
        {
            DiscoveryDocumentResponse disco = await discoveryCache.GetAsync();

            HttpClient httpClient = new HttpClient();

            TokenResponse response = await httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = options.ClientId,
                ClientSecret = options.Secret,
                Scope = options.Scope,

                UserName = username,
                Password = password
            });

            if (!response.IsError)
            {
                UserInfoResponse idResponse = await httpClient.GetUserInfoAsync(new UserInfoRequest
                {
                    Address = disco.UserInfoEndpoint,

                    Token = response.AccessToken
                });

                //get user identity
                if (!idResponse.IsError)
                {
                    var claims = idResponse.Claims.ToList();
                    
                    claims.Add(new Claim(ClaimTypes.Name, username));
                    claims.Add(new Claim(ClaimTypes.Role, "app-user"));

                    var claimsIdentity = new ClaimsIdentity(claims, "oidc", ClaimTypes.Name, ClaimTypes.Role);
                    

                    Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(new { claimsIdentity.Name, claimsIdentity.AuthenticationType, claimsIdentity.IsAuthenticated, claimsIdentity.Label }));

                    // Console.WriteLine("Access Token {0}", response.AccessToken);
                    // Console.WriteLine("Id Token {0}", response.IdentityToken);
                    // Console.WriteLine("Refresh Token {0}", response.RefreshToken);

                    //TODO: Map Roles
                    return (!response.IsError, response.AccessToken, 
                                response.RefreshToken, response.IdentityToken,
                                                                    claimsIdentity);
                }
                else
                {
                    Console.WriteLine("Error when fetching Id");
                }
            }

            return (false, null, null, null, null);
        }

        public async Task LogoutAsync()
        {
            await Task.CompletedTask;
        }
    }
}
