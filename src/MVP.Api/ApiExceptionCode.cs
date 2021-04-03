namespace MVP.Api
{
    public enum ApiExceptionCode
    {
        /// <summary>
        /// Caused when an error occurs with a network request.
        /// </summary>
        [ApiExceptionMessage("A network error has occurred.")]
        NetworkError,

        /// <summary>
        /// Caused when an error occurs with authentication.
        /// </summary>
        [ApiExceptionMessage("An authentication error has occurred.")]
        AuthenticationError,

        /// <summary>
        /// Caused when a general error occurs.
        /// </summary>
        [ApiExceptionMessage("An error has occurred.")]
        Error
    }
}