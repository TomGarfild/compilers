using System;

namespace Scala
{
    public class SyntaxException : Exception
    {
        public SyntaxException(): base() { }
        public SyntaxException(string message) : base(message) { }
        public SyntaxException(string message,  Exception innerException) : base(message, innerException) { }
    }
}