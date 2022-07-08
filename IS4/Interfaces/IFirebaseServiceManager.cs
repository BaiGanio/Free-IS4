using System.Collections.Generic;
using System.Threading.Tasks;

namespace IS4
{
    public interface IFirebaseServiceManager
    {
        Task LogCustomExceptionAsync(CustomException ce);
        Task<IEnumerable<CustomException>> GetTop25CustomExceptionsAsync();
    }
}
