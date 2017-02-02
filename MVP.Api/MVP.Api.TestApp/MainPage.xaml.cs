namespace MVP.Api.TestApp
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MVP.Api.Models.MicrosoftAccount;

    using Windows.Security.Authentication.Web;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    using WinUX;
    using WinUX.Messaging.Dialogs;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.UpdateButtonStates();
        }

        private async void OnLoginClicked(object sender, RoutedEventArgs e)
        {
            await this.AuthenticateAsync();
        }

        private async Task AuthenticateAsync()
        {
            var scopes = new List<MSAScope> { MSAScope.Basic, MSAScope.Emails, MSAScope.OfflineAccess, MSAScope.SignIn };

            var authUri = App.API.RetrieveAuthenticationUri(scopes);

            var result = await WebAuthenticationBroker.AuthenticateAsync(
                             WebAuthenticationOptions.None,
                             new Uri(authUri),
                             new Uri(ApiClient.RedirectUri));

            if (result.ResponseStatus == WebAuthenticationStatus.Success)
            {
                if (!string.IsNullOrWhiteSpace(result.ResponseData))
                {
                    var responseUri = new Uri(result.ResponseData);
                    if (responseUri.LocalPath.StartsWith("/oauth20_desktop.srf", StringComparison.OrdinalIgnoreCase))
                    {
                        var error = responseUri.ExtractQueryValue("error");
                        if (string.IsNullOrWhiteSpace(error))
                        {
                            var authCode = responseUri.ExtractQueryValue("code");
                            var msaCredentials = await App.API.ExchangeAuthCodeAsync(authCode);
                        }
                        else
                        {
                            await MessageDialogManager.Current.ShowAsync(error);
                        }
                    }
                }
            }

            this.UpdateButtonStates();
        }

        private async void OnLogoutClicked(object sender, RoutedEventArgs e)
        {
            await App.API.LogOutAsync();

            this.UpdateButtonStates();
        }

        private async void OnGetProfileClicked(object sender, RoutedEventArgs e)
        {
            var profile = await App.API.GetMyProfileAsync();

            await MessageDialogManager.Current.ShowAsync($"Welcome, {profile.DisplayName}!");
        }

        private void UpdateButtonStates()
        {
            if (App.API.Credentials != null)
            {
                this.LoginBtn.IsEnabled = false;
                this.GetProfileBtn.IsEnabled = true;
                this.LogoutBtn.IsEnabled = true;
            }
            else
            {
                this.LoginBtn.IsEnabled = true;
                this.GetProfileBtn.IsEnabled = false;
                this.LogoutBtn.IsEnabled = false;
            }
        }
    }
}