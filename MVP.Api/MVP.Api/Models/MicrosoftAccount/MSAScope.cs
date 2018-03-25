namespace MVP.Api.Models.MicrosoftAccount
{
    public enum MSAScope
    {
        /// <summary>
        /// Provides read access to a user's basic profile info.
        /// </summary>
        [MSAScopeName("wl.basic")]
        Basic,

        /// <summary>
        /// Provides read access to a user's birthday info including birth day, month and year.
        /// </summary>
        [MSAScopeName("wl.birthday")]
        Birthday,

        /// <summary>
        /// Provides read access to a user's personal, preferred and business email addresses.
        /// </summary>
        [MSAScopeName("wl.emails")]
        Emails,

        /// <summary>
        /// Provides the ability for an app to read & update a user's info at any time.
        /// </summary>
        [MSAScopeName("wl.offline_access")]
        OfflineAccess,

        /// <summary>
        /// Provides single sign-in behavior.
        /// </summary>
        [MSAScopeName("wl.signin")]
        SignIn
    }
}