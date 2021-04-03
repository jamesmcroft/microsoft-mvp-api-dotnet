namespace MVP.Api
{
    using System;

    public class ApiException : Exception
    {
        public ApiException(ApiExceptionCode code)
            : this(code, ApiExceptionMessageAttribute.GetMessage(code), null)
        {
        }

        public ApiException(ApiExceptionCode code, string message)
            : this(code, message, null)
        {
        }

        public ApiException(ApiExceptionCode code, string message, Exception innerException)
            : base(message, innerException)
        {
            this.Code = code;
        }

        public ApiExceptionCode Code { get; }
    }
}