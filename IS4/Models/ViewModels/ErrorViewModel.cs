using IdentityServer4.Models;

namespace IS4
{

    public class ErrorViewModel
    {
        public ErrorViewModel()
        {
        }
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public ErrorViewModel(string error)
        {
            Error = new ErrorMessage { Error = error };
        }

        public ErrorMessage Error { get; set; }
    }
}
