namespace MVP.Api.Models.MicrosoftAccount
{
    using WinUX.Attributes;

    public enum MSAScope
    {
        /// <summary>
        /// Provides read access to a user's basic profile info.
        /// </summary>
        [Description("wl.basic")]
        Basic,

        /// <summary>
        /// Provides read access to a user's birthday info including birth day, month and year.
        /// </summary>
        [Description("wl.birthday")]
        Birthday,

        /// <summary>
        /// Provides read access to a user's personal, preferred and business email addresses.
        /// </summary>
        [Description("wl.emails")]
        Emails,

        /// <summary>
        /// Provides the ability for an app to read & update a user's info at any time.
        /// </summary>
        [Description("wl.offline_access")]
        OfflineAccess,

        /// <summary>
        /// Provides single sign-in behavior.
        /// </summary>
        [Description("wl.signin")]
        SignIn
    }
}