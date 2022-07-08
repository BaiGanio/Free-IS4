using IdentityServer4.Models;
using System.Collections.Generic;

namespace IS4
{
    public static class ApiScopes
    {
        public static List<ApiScope> Get()
        {
            return
               new List<ApiScope>
               {
                    new ApiScope(){ Name = "scope.bgapi-free", DisplayName = "BGAPI-FREE"},
                    new ApiScope(){ Name = "scope.bgapi-test", DisplayName = "BGAPI-TEST"},
                    new ApiScope(){ Name = "scope.bgapi", DisplayName = "BGAPI"},
               };
        }
    }
}
