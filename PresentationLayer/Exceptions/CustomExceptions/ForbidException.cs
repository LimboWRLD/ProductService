namespace Products.Exceptions.CustomExceptions
{
    public class ForbidException : Exception
    {
        public ForbidException() : base("Access is forbidden.") { }
        public ForbidException(string message) : base(message) { }
    }
}
