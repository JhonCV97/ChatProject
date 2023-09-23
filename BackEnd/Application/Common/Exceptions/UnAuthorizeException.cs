using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Exceptions
{
    public class UnAuthorizeException : Exception
    {
        public UnAuthorizeException(string message) : base(message)
        {

        }
    }
}
