using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(IDictionary<string, string> errors, bool ifShowResponseMessage = true)
			: base(400, errors, ifShowResponseMessage)
		{
		}
    } 
}
