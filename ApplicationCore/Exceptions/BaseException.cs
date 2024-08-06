using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApplicationCore.Exceptions
{
    public class BaseException:Exception
    {
        public readonly int StatusCode;
        public readonly string ResponseMessage;
        public readonly bool IfShowResponseMessage;

        protected BaseException(int statusCode, string internalMessage, string responseMessage, bool ifShowResponseMessage = true)
            : base(internalMessage)
        {
            StatusCode = statusCode;
            ResponseMessage = responseMessage;
            Data.Add("message", responseMessage);
            IfShowResponseMessage = ifShowResponseMessage;
        }

        protected BaseException(int statusCode, IDictionary<string, string> errors, bool ifShowResponseMessage = true)
            : base()
        {
            StatusCode = statusCode;
            ResponseMessage = string.Join($";{Environment.NewLine}", errors);
            foreach (var error in errors)
            {
                Data.Add(error.Key, error.Value);
            }
            IfShowResponseMessage = ifShowResponseMessage;
        }
    }
}
