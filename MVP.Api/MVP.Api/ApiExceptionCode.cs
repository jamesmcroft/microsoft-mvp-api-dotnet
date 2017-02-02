namespace MVP.Api
{
    using WinUX.Attributes;

    public enum ApiExceptionCode
    {
        /// <summary>
        /// Caused when an error occurs with a network request.
        /// </summary>
        [Description("A network error has occurred.")]
        NetworkError,

        /// <summary>
        /// Caused when an error occurs with authentication.
        /// </summary>
        [Description("An authentication error has occurred.")]
        AuthenticationError,

        /// <summary>
        /// Caused when a general error occurs.
        /// </summary>
        [Description("An error has occurred.")]
        Error
    }
}