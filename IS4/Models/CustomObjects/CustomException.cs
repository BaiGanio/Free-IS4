using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;

namespace IS4
{
    public sealed class CustomException : Exception
    {
        public CustomException(Exception ex)
        {
            Id = new CustomId().ToString();
            CustomMessage = ex?.Message ?? String.Empty;
            CustomStackTrace = ex?.StackTrace != null ? PrettifyStackTrace(ex.StackTrace) : String.Empty;
            InnerMessage = ex?.InnerException != null ? ex.InnerException.Message : String.Empty;
            InnerStackTrace = ex?.InnerException != null ? ex.InnerException.StackTrace : String.Empty;
            InnerSource = ex?.InnerException != null ? ex.InnerException.Source : String.Empty;
            DateCreated = DateTime.Now;
        }


        public string Id { get; set; }

        public string CustomMessage { get; set; }

        public string CustomStackTrace { get; set; }

        public string InnerMessage { get; set; }

        public string InnerStackTrace { get; set; }

        public string InnerSource { get; set; }

        public DateTime DateCreated { get; set; }

        public string ClientErrorMessage { get { return CreateClientErrorMessage(); } set { CustomMessage = value; } }


        public void Deserialize(string json)
        {
            CustomException deserializedContact = JsonConvert.DeserializeObject<CustomException>(json);
            this.Id = deserializedContact.Id;
            this.CustomMessage = deserializedContact.CustomMessage;
            this.CustomStackTrace = deserializedContact.CustomStackTrace;
            this.InnerMessage = deserializedContact.InnerMessage;
            this.InnerStackTrace = deserializedContact.InnerStackTrace;
            this.InnerSource = deserializedContact.InnerSource;
            this.DateCreated = deserializedContact.DateCreated;
            this.ClientErrorMessage = deserializedContact.ClientErrorMessage;
        }

        public string ToSeriliazedString()
        {
            return
                JsonConvert.SerializeObject
                (
                    this,
                    Formatting.None,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    }
                );
        }

        private string PrettifyStackTrace(string stackTrace)
        {
            return Regex.Replace(stackTrace, @"\t\n\r", Environment.NewLine);
        }

        private string CreateClientErrorMessage()
        {
            return
                $"Unexpected error with Id: ({Id}) has occured on the server. Message: ({(String.IsNullOrEmpty(InnerMessage) ? CustomMessage : InnerMessage)})";
        }
    }
}
