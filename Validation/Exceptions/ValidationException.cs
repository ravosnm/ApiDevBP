namespace ApiDevBP.Validation.Exceptions
{
    public class ValidationException : Exception
    {
        public string Code { get; set; }
        public string Description { get; set; }

        public ValidationException(string code, string description) : base(description)
        {
            Code = code;
            Description = description;
        }

        public ValidationException()
        {
        }

        public ValidationException(string message) : base(message)
        {
        }

        public ValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
