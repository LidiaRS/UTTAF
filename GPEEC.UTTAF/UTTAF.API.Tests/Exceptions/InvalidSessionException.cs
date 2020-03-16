using System;

namespace UTTAF.API.Tests.Exceptions
{
    internal class InvalidSessionException : Exception
    {
        public InvalidSessionException(string msg) : base(msg)
        {
        }
    }
}