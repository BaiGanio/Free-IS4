using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IS4
{
    public sealed class LogServiceManager : ILogServiceManager
    {
        private readonly IFirebaseServiceManager _firebaseManager;

        public LogServiceManager(IFirebaseServiceManager firebaseManager)
        {
            _firebaseManager = firebaseManager;
        }

        public async Task LogExceptionAsync(Exception ex)
        {
            var ce = new CustomException(ex);
            await _firebaseManager.LogCustomExceptionAsync(ce);
        }

        public async Task LogCustomExceptionAsync(CustomException ce)
        {
            await _firebaseManager.LogCustomExceptionAsync(ce);
        }

        public async Task<IEnumerable<CustomException>> GetTop25CustomExceptionsAsync()
        {
            return await _firebaseManager.GetTop25CustomExceptionsAsync();
        }
    }
}
