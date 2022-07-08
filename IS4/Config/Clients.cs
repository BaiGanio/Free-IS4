using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace IS4
{
    public static class Clients
    {
        public static IEnumerable<Client> Get(IConfiguration config)
        {
            var swaggerUi = new Client
            {
                ClientId = "Swagger_UI",
                ClientName = "Swagger UI for BGAPI",
                ClientSecrets = { new Secret("secret".Sha256()) }, // change me!

                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                RequireClientSecret = false,
                RedirectUris = 
                {
                    config["BGAPI:URL:PROD"] + "/swagger/oauth2-redirect.html",
                    config["BGAPI:URL:TEST"] + "/swagger/oauth2-redirect.html",
                    config["BGAPI:URL:LOCAL"] + "/swagger/oauth2-redirect.html"
                },
                AllowedCorsOrigins = 
                { 
                    config["BGAPI:URL:PROD"], 
                    config["BGAPI:URL:TEST"], 
                    config["BGAPI:URL:LOCAL"] 
                },
                AllowedScopes = 
                { 
                    config["BGAPI:ALLOWED_SCOPES:PROD"], 
                    config["BGAPI:ALLOWED_SCOPES:TEST"], 
                    config["BGAPI:ALLOWED_SCOPES:FREE"] 
                }
            };

            return new List<Client>
            {
                /* BGLocalClient & BGLocalUser can be reused in many apps who are developed & run @ local environment */
                BaiGainioLocalClient,
                BaiGainioLocalUser,
                BaiGainioClient(),
                BaiGainioUser(),
                BOLocalClient,
                BOLocalUser,
                BOClient,
                BOUser,
                swaggerUi
             
            };
        }

        #region BaiGainio

        private static readonly Client BaiGainioLocalClient =
            new Client
            {
                ClientId = "baiganio-local-client",
                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("123".Sha256()) },
                // scopes that client has access to
                AllowedScopes = { "scope.bgapi-free" },
                AccessTokenLifetime = 43200, // 12 hours
                AccessTokenType = AccessTokenType.Jwt
            };

        private static readonly Client BaiGainioLocalUser =
            new Client
            {
                ClientId = "baiganio-local-user",
                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = { new Secret("123".Sha256()) },
                // scopes that client has access to
                AllowedScopes = { "scope.bgapi-free" },
                AccessTokenLifetime = 43200, // 12 hours
                AccessTokenType = AccessTokenType.Jwt,
                AllowedCorsOrigins = { "https://localhost:44364" },
                AllowAccessTokensViaBrowser = true,
            };

        private static Client BaiGainioClient()
        {
            return
                new Client
                {
                    ClientName = "BaiGanio Web App",
                    ClientId = "baiganio-client",//configuration["BaiGanio:Credentials:baiganio-client-id"],
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret(/*configuration["BaiGanio:Credentials:baiganio-client-secret"]*/"92837cf2-926f-4aa5-87b2-42cfa816ff7e".Sha256()) },
                    AllowedScopes = { "scope.bgapi" },
                    AccessTokenLifetime = 43200, // 12 hours
                    AccessTokenType = AccessTokenType.Jwt
                };
        }

        private static Client BaiGainioUser()
        {
            return
                new Client
                {
                    ClientName = "BaiGanio Web App on Behalf of a User",
                    ClientId = "baiganio-user",//configuration["BaiGanio:Credentials:baiganio-user-id"],
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    Enabled = true,
                    ClientSecrets = { new Secret(/*configuration["BaiGanio:Credentials:baiganio-user-secret"]*/"ac3e0c69-6cc4-4274-99d7-733ef67d48e0".Sha256()) },
                    AllowedScopes = { "scope.bgapi" },
                    AccessTokenLifetime = 43200, // 12 hours
                    AccessTokenType = AccessTokenType.Jwt
                };
        }

        #endregion BaiGanio

        #region BackOffice

        private static readonly Client BOLocalClient =
            new Client
            {
                ClientId = "bo-local-client",
                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("123".Sha256()) },
                // scopes that client has access to
                AllowedScopes = { "scope.bgapi-free" },
                AccessTokenLifetime = 43200, // 12 hours
                AccessTokenType = AccessTokenType.Jwt
            };

        private static readonly Client BOLocalUser =
            new Client
            {
                ClientId = "bo-local-user",
                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = { new Secret("123".Sha256()) },
                // scopes that client has access to
                AllowedScopes = { "scope.bgapi-free" },
                AccessTokenLifetime = 43200, // 12 hours
                AccessTokenType = AccessTokenType.Jwt
            };

        private static readonly Client BOClient =
        new Client
        {
            ClientId = "bo-client",
            // no interactive user, use the clientid/secret for authentication
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            ClientSecrets = { new Secret("9aa9796b-1261-40a7-a3f3-3308639b0b19".Sha256()) },
            // scopes that client has access to
            AllowedScopes = { "scope.bgapi" },
            AccessTokenLifetime = 43200, // 12 hours
            AccessTokenType = AccessTokenType.Jwt
        };

        private static readonly Client BOUser =
            new Client
            {
                ClientId = "bo-user",
                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = { new Secret("aca35df5-8747-44ad-8eba-fec042d049b6".Sha256()) },
                // scopes that client has access to
                AllowedScopes = { "scope.bgapi" },
                AccessTokenLifetime = 43200, // 12 hours
                AccessTokenType = AccessTokenType.Jwt
            };
        #endregion BackOffice

    }
}
