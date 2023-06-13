using System.Security.Claims;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using IdentityModel;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace IdP
{
    public static class IdpConfiguration
    {
        public static List<TestUser> GetTestUsers()
        {
            return new(){

                new TestUser
                {
                    SubjectId = "9fce8cc5-4017-4920-ab4c-1ff0ff06f4af",
                    Username = "wassi@gmail.com",
                    Password = "wassi-harif",

                    Claims = new List<Claim>(){
                        new Claim(JwtClaimTypes.Email, "wassi@gmail.com"),
                        new Claim(JwtClaimTypes.Role, "app-user"),
                        new Claim(JwtClaimTypes.PreferredUserName, "wassi@gmail.com"),
                        new Claim(JwtClaimTypes.Address, "Porto-Novo, Bénin"),
                        new Claim(JwtClaimTypes.Name, "amoussa wassi"),
                        new Claim(JwtClaimTypes.FamilyName, "amoussa"),
                        new Claim(JwtClaimTypes.GivenName, "wassi"),
                        new Claim(JwtClaimTypes.EmailVerified, "true"),
                        new Claim(JwtClaimTypes.PhoneNumber, "+22990210790"),
                        new Claim(JwtClaimTypes.PhoneNumberVerified, "true"),
                        new Claim(JwtClaimTypes.Locale, "fr-Fr"),
                        
                        //TODO: Add Custom props

                        //FIXME: Prefer using userid to fetch these infos from the database
                        new Claim("kyc_verified", "true"),
                        new Claim("ngn-balance", "27500"),
                        new Claim("xof-balance", "0"),
                    },

                    IsActive = true
                }
            };
        }

        public static List<ApiScope> GetApiScopes()
        {
            return new(){
                new ApiScope("transactions:create")
            };
        }

        public static List<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
                new IdentityResources.Phone(),

                new IdentityResource()
                {
                    Name = "role",
                    DisplayName = "User Roles",
                    Enabled = true,
                    ShowInDiscoveryDocument = true,
                    UserClaims = new List<string>{ "role" },
                    Description = "Roles For RBAC"
                },

                new IdentityResource()
                {
                    Name = "kyc_verified",
                    DisplayName = "Whether KYC passed",
                    Enabled = true,
                    UserClaims = new List<string>{ "kyc_verified" },
                    ShowInDiscoveryDocument = true,

                    Required = true,
                    Description = "Kyc Params",
                },

                
            };
        }

        public static List<ApiResource> GetApiResources()
        {
            return new List<ApiResource>(){

                new ApiResource()
                {
                    Name = "backend-api",
                    DisplayName = "Backend Api",

                    ApiSecrets = new List<Secret>(){
                        new Secret("SuperBackendSecret".ToSha256())
                    },

                    Scopes = new List<string>(){
                        "transactions:create"
                    },

                    Enabled = true,
                    ShowInDiscoveryDocument = true,

                    UserClaims = new List<string>(){
                        "role"
                    }
                }
            };
        }

        public static List<Client> GetClients()
        {
            return new(){

                new Client {
                    ClientId = "backend-api-client",
                    ClientName = "Backend Api Client",

                    ClientSecrets = new List<Secret>{
                        new Secret("SperBcSecret".ToSha256())
                    },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    AllowedScopes = new List<string>()
                    {

                    },

                    Claims = new List<ClientClaim>(){

                    },

                    Enabled = true
                },

                new Client()
                {
                    ClientId = "exmoney-mobile-app",
                    ClientName = "ExMoney",

                    RequireClientSecret = false,

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    AllowedScopes = new List<string>{
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.OfflineAccess,

                        //TODO: Map custom claims
                        "kyc_verified",
                        "ngn-balance",
                        "xof-balance"
                    },

                    Enabled = true,
                    AllowOfflineAccess= true,
                    RequireConsent = false,
                }
            };
        }
    }
}
