using System;
using System.Security.Claims;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using IdentityModel;

namespace IdP
{
    public static class IdpConfiguration
    {
        public static List<TestUser> GetTestUsers()
        {
            return new(){

                new TestUser{
                    SubjectId = "9fce8cc5-4017-4920-ab4c-1ff0ff06f4af",
                    Username = "wassi-harif",
                    Password = "wassi-harif",
                    Claims = new List<Claim>(){
                        new Claim(ClaimTypes.Email, "wassi@gmail.com"),
                        new Claim(ClaimTypes.Role, "app_user"),
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

                new IdentityResource()
                {
                    Name = "role",
                    DisplayName = "User Roles",
                    Enabled = true,
                    ShowInDiscoveryDocument = true,
                    UserClaims = new List<string>{ "role" },
                    Description = "Roles access for Role Based Authorization"
                }
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
                }
            };
        }
    }
}
