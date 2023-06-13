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
        private HttpClient httpClient;

        public string RefreshToken { get; set; }
        public string IdToken { get; set; }
        public string AccessToken { get; set; }

        public KeycloakAuthenticator(IOptions<IdpAuthenticationOptions> IdpOptions, IDiscoveryCache discoveryCache)
        {
            options = IdpOptions.Value;
            this.discoveryCache = discoveryCache;
            this.httpClient = new HttpClient();
        }

        public async Task<(bool isSusscess, ClaimsIdentity claims)> LoginAsync(string username, string password)
        {
            var disco = await discoveryCache.GetAsync();
            httpClient = new();

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
                //Assign RefreshToken
                RefreshToken = response.RefreshToken;
                AccessToken = response.AccessToken;
                IdToken = response.IdentityToken;

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
                    

                    // Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(new { claimsIdentity.Name, claimsIdentity.AuthenticationType, claimsIdentity.IsAuthenticated, claimsIdentity.Label }));
                    // Console.WriteLine("Access Token {0}", response.AccessToken);
                    // Console.WriteLine("Id Token {0}", response.IdentityToken);
                    // Console.WriteLine("Refresh Token {0}", response.RefreshToken);

                    //TODO: Map Roles
                    return (!response.IsError, claimsIdentity);
                }
                else
                {
                    Console.WriteLine("Error when fetching Id");
                }
            }

            return (false, null);
        }

        public async Task<(string accessToken, string idToken)> RefreshAllTokens()
        {
            var discoDoc = await discoveryCache.GetAsync();
            httpClient = new();

            var response = await httpClient.RequestRefreshTokenAsync(new RefreshTokenRequest{
                Address = discoDoc.TokenEndpoint,
                
                ClientId = options.ClientId,

                RefreshToken = this.RefreshToken,

                Scope = options.Scope
            });

            if(response.IsError)
                return (null, null);
            
            RefreshToken = response.RefreshToken;
            AccessToken = response.AccessToken;
            IdToken = response.IdentityToken;

            return (response.AccessToken, response.IdentityToken);
        }
        
        public async Task<List<Claim>> GetUserInfosAsync()
        {
            var discoDoc = await discoveryCache.GetAsync();
            httpClient = new();

            // Console.WriteLine("Access Token {0}", AccessToken);

            var response = await httpClient.GetUserInfoAsync(new UserInfoRequest{
                Address = discoDoc.UserInfoEndpoint,
                Token = AccessToken
            });

            if(!response.IsError)
            {
                return response.Claims.ToList();
            }

            return default;
        }
        
        public async Task LogoutAsync()
        {
            await Task.CompletedTask;
        }
    }
}
