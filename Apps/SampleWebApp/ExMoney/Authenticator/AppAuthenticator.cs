using System.Security.Claims;
using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace ExMoney.Authenticator
{
    public class AppAuthenticator
    {
        private readonly IdpAuthenticationOptions options;
        private readonly IDiscoveryCache discoveryCache;
        private HttpClient httpClient;

        public string RefreshToken { get; set; }
        public string IdToken { get; set; }
        public string AccessToken { get; set; }

        public AppAuthenticator(IOptions<IdpAuthenticationOptions> IdpOptions, IDiscoveryCache discoveryCache, ILogger<AppAuthenticator> logger)
        {
            options = IdpOptions.Value;
            this.discoveryCache = discoveryCache;
            httpClient = new HttpClient();
        }

        public async Task<(bool isSusscess, ClaimsIdentity claims)> LoginAsync(string username, string password)
        {
            DiscoveryDocumentResponse disco = await discoveryCache.GetAsync();
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
                    List<Claim> claims = idResponse.Claims.ToList();

                    claims.Add(new Claim(ClaimTypes.Name, username));
                    claims.Add(new Claim(ClaimTypes.Role, "app-user"));

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "oidc", ClaimTypes.Name, ClaimTypes.Role);


                    // Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(claims));
                    // Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(new { claimsIdentity.Name, claimsIdentity.AuthenticationType, claimsIdentity.IsAuthenticated, claimsIdentity.Label }));

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
            DiscoveryDocumentResponse discoDoc = await discoveryCache.GetAsync();
            httpClient = new();

            TokenResponse response = await httpClient.RequestRefreshTokenAsync(new RefreshTokenRequest
            {
                Address = discoDoc.TokenEndpoint,

                ClientId = options.ClientId,

                RefreshToken = RefreshToken,

                Scope = options.Scope
            });

            if (response.IsError)
            {
                return (null, null);
            }

            RefreshToken = response.RefreshToken;
            AccessToken = response.AccessToken;
            IdToken = response.IdentityToken;

            return (response.AccessToken, response.IdentityToken);
        }

        public async Task<List<Claim>> GetUserInfosAsync()
        {
            DiscoveryDocumentResponse discoDoc = await discoveryCache.GetAsync();
            httpClient = new();

            UserInfoResponse response = await httpClient.GetUserInfoAsync(new UserInfoRequest
            {
                Address = discoDoc.UserInfoEndpoint,
                Token = AccessToken
            });

            return !response.IsError ? response.Claims.ToList() : default;
        }

        public async Task LogoutAsync()
        {
            discoveryCache.Refresh();
            DiscoveryDocumentResponse discoDoc = await discoveryCache.GetAsync();

            httpClient = new();

            _ = httpClient.RevokeTokenAsync(new TokenRevocationRequest
            {
                Address = discoDoc.RevocationEndpoint,
                ClientId = options.ClientId,
                Token = AccessToken, 
            });

            await Task.CompletedTask;
        }
    }
}
