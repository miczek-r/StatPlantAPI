using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class AccessForbiddenException : Exception
    {
        public AccessForbiddenException()
        {

        }

        public AccessForbiddenException(string? message): base(message)
        {

        }

        public AccessForbiddenException(string? message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {

        }
    }
}
