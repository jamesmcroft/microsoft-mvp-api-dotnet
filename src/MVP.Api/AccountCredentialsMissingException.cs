namespace MVP.Api
{
    using System;

    /// <summary>
    /// Defines an exception thrown if the user's account credentials for authentication is missing.
    /// </summary>
    public class AccountCredentialsMissingException : ApiException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountCredentialsMissingException"/> class.
        /// </summary>
        public AccountCredentialsMissingException()
            : base(ApiExceptionCode.AuthenticationError)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="AccountCredentialsMissingException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public AccountCredentialsMissingException(string message)
            : base(ApiExceptionCode.AuthenticationError, message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountCredentialsMissingException"/> class with a specified error message and inner exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public AccountCredentialsMissingException(string message, Exception innerException)
            : base(ApiExceptionCode.AuthenticationError, message, innerException)
        {
        }
    }
}