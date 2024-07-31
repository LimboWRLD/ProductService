﻿namespace Products.Exceptions.CustomExceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() : base("Unauthorized access.") { }
        public UnauthorizedException(string message) : base(message) { }
    }
}
