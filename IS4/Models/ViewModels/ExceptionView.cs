using System;

namespace IS4
{
    public class ExceptionView
    {
        public string Id { get; set; }

        public string CustomMessage { get; set; }

        public string CustomStackTrace { get; set; }

        public string CustomInnerMessage { get; set; }

        public string CustomInnerStackTrace { get; set; }

        public DateTime DateCreated { get; set; }

        public string ExceptionSource { get; set; }

        //public static explicit operator ExceptionView(CustomException ex)
        //{
        //    if (ex == null) return null;
        //    return new ExceptionView
        //    {
        //        Id = ex.Id,
        //        CustomMessage = ex.CustomMessage,
        //        CustomStackTrace = ex.CustomStackTrace,
        //        CustomInnerMessage = ex.CustomInnerMessage,
        //        CustomInnerStackTrace = ex.CustomInnerStackTrace,
        //        DateCreated = ex.DateCreated
        //    };
        //}
    }
}
