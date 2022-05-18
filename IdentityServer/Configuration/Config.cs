using IdentityServer4.Models;

namespace IdentityServer.Configuration
{
    public class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name ="role",
                    UserClaims = new List<string>{"role"}
                }
            };


        public static IEnumerable<ApiScope> ApiScopes =>
            new[]
            {
                new ApiScope("CoffeeApi.Read"),
                new ApiScope("CoffeeApi.Write")
            };


        public static IEnumerable<ApiResource> ApiResources =>
            new[]
            {
                new ApiResource("CoffeeApi")
                {
                    Scopes = new List<string>{ "CoffeeApi.Read", "CoffeeApi.Write" },
                    ApiSecrets = new List<Secret>{ new Secret("ScopeSecret".Sha256())},
                    UserClaims = new List<string>{ "role"}
                }
            };


        public static IEnumerable<Client> Clients =>
            new[]
            {
                new Client
                {
                    ClientId = "m2m.client",
                    ClientName = "Client Credentials Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("ClientSecret1".Sha256())},
                    AllowedScopes = { "CoffeeApi.Read", "CoffeeApi.Write" }
                },
                //interactive client using codeflow > pkce
                new Client
                {
                    ClientId = "interactive",
                    ClientSecrets = {new Secret("ClientSecret1".Sha256()) },
                    AllowedGrantTypes= GrantTypes.Code,
                    RedirectUris = { "https://localhost:7126/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:7126/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:7126/signout-callback-oidc" },
                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid","profile", "CoffeeApi.Read" },
                    RequirePkce = true,
                    RequireConsent = true,
                    AllowPlainTextPkce = false

                }
            };

    }
}
