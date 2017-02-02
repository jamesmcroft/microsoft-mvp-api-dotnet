namespace MVP.Api
{
    using System;

    using WinUX;

    public class ApiException : Exception
    {
        public ApiException(ApiExceptionCode code)
            : this(code, code.GetDescriptionAttribute(), null)
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