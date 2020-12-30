using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Demo.Exceptions
{
    public class DemoException : Exception
    {

        public HttpStatusCode statusCode { get; set; }

        public DemoException(string message, HttpStatusCode statusCode) : base(message)
        {
            this.statusCode = statusCode;
        }

    }
}
