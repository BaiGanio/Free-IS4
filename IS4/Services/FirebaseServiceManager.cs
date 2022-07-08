using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IS4
{
    public sealed class FirebaseServiceManager : IFirebaseServiceManager
    {
        private readonly FirebaseClient _firebaseBaseEventsClient;
        private IReadOnlyCollection<FirebaseObject<object>> _firebasePayload;

        public FirebaseServiceManager()
        {
            _firebaseBaseEventsClient =
               new FirebaseClient(
                   "https://ids4-events.firebaseio.com/",
                   new FirebaseOptions
                   {
                       AuthTokenAsyncFactory =
                           () => Task.FromResult("rguRHFty78XZdHQbSirDpvFckz15lKa8uEl3CZ2T ")
                   }
               );
        }

        public async Task LogCustomExceptionAsync(CustomException ce)
        {
            try
            {
                await
                    _firebaseBaseEventsClient
                        .Child(DateTimeUtils.GetCurrentYear().ToString())
                        .Child("Exceptions")
                        .Child(DateTimeUtils.GetCurrentMonthName)
                        .Child(DateTimeUtils.GetCurrentMonthDate())
                        .PostAsync(ce);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<CustomException>> GetTop25CustomExceptionsAsync()
        {
            _firebasePayload =
                  await _firebaseBaseEventsClient
                  .Child(DateTimeUtils.GetCurrentYear().ToString())
                  .Child("Exceptions")
                  .Child(DateTimeUtils.GetCurrentMonthName)
                  .Child(DateTimeUtils.GetCurrentMonthDate())
                  .OrderByKey()
                  .OnceAsync<object>();

            return
              OrganizeExceptionsPayload(_firebasePayload)
              .OrderByDescending(x => x.DateCreated);
        }


        private static List<CustomException> OrganizeExceptionsPayload(IReadOnlyCollection<FirebaseObject<object>> payload)
        {
            var output = new List<CustomException>();
            foreach (var item in payload)
            {
                var rawData =
                JsonConvert.DeserializeObject<CustomException>(item.Object.ToString());
                output.Add(rawData);
            }

            return output;
        }

    }
}
