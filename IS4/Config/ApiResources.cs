using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IS4
{
    public static class ApiResources
    {
        #region BGAPI

        private static ApiResource bgapi_free =
            new ApiResource(
                "bgapi-free",
                "BGAPI local env resource",
                new[]
                {
                    JwtClaimTypes.Subject,
                    JwtClaimTypes.Email,
                    JwtClaimTypes.Role
                }
            );

        private static ApiResource bgapi_test =
            new ApiResource(
                "bgapi-test",
                "BGAPI test env resource",
                new[]
                {
                    JwtClaimTypes.Subject,
                    JwtClaimTypes.Email,
                    JwtClaimTypes.Role
                }
            );

        private static ApiResource bgapi =
            new ApiResource(
                "bgapi",
                "BGAPI prod env resource",
                new[]
                {
                    JwtClaimTypes.Subject,
                    JwtClaimTypes.Email,
                    JwtClaimTypes.Role
                }
            );

        #endregion

        public static List<ApiResource> Get()
        {
            return new List<ApiResource>
            {
                bgapi_free,
                bgapi_test,
                bgapi,
            };
        }
    }
}
