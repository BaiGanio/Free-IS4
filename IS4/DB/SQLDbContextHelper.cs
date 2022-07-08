using Microsoft.Extensions.Configuration;

namespace IS4
{
    public class SQLDbContextHelper
    {
        public static SQLDbContext SetDbContext(string apiClient, IConfiguration config)
        {
            return apiClient switch
            {
                "scope.bgapi-free" or "scope.bgapi-test" => new SQLDbContext(config.GetConnectionString("dbconn_bgapi_test")),
                "scope.bgapi" => new SQLDbContext(config.GetConnectionString("dbconn")),
                _ => null,
            };
        }
    }
}
