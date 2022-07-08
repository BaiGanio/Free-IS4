using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IS4
{
    public interface ILogServiceManager
    {
        Task LogExceptionAsync(Exception ex);
        Task LogCustomExceptionAsync(CustomException ce);
        Task<IEnumerable<CustomException>> GetTop25CustomExceptionsAsync();
    }
}
