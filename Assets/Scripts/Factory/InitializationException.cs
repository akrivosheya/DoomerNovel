using System;

namespace Factory
{
    public class InitializationException : Exception
    {
        public InitializationException(string message): base(message) { }
    }
}
