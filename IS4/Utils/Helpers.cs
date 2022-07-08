using Microsoft.Extensions.Configuration;

namespace IS4.Utils
{
    public static class Helpers
    {
        public static SQLDbContext SetDbContext(string apiClient, IConfiguration config)
        {
            if (apiClient == "scope.bgapi-free")
                return new SQLDbContext(config.GetConnectionString("dbconn_bgapi_test"));
            if (apiClient == "scope.bgapi-test")
                return new SQLDbContext(config.GetConnectionString("dbconn_bgapi_test"));
            if (apiClient == "scope.bgapi")
                return new SQLDbContext(config.GetConnectionString("dbconn"));

            return null;
        }
    }
}
