namespace MVP.Api.TestApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MVP.Api.Models;
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
            List<MSAScope> scopes = new List<MSAScope> { MSAScope.Basic, MSAScope.Emails, MSAScope.OfflineAccess, MSAScope.SignIn };

            string authUri = App.API.RetrieveAuthenticationUri(scopes);

            var result = await WebAuthenticationBroker.AuthenticateAsync(
                             WebAuthenticationOptions.None,
                             new Uri(authUri),
                             new Uri(ApiClient.RedirectUri));

            if (result.ResponseStatus == WebAuthenticationStatus.Success)
            {
                if (!string.IsNullOrWhiteSpace(result.ResponseData))
                {
                    Uri responseUri = new Uri(result.ResponseData);
                    if (responseUri.LocalPath.StartsWith("/oauth20_desktop.srf", StringComparison.OrdinalIgnoreCase))
                    {
                        string error = responseUri.ExtractQueryValue("error");
                        if (string.IsNullOrWhiteSpace(error))
                        {
                            string authCode = responseUri.ExtractQueryValue("code");
                            MSACredentials msaCredentials = await App.API.ExchangeAuthCodeAsync(authCode);
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

        private async void OnPerformApiTestClicked(object sender, RoutedEventArgs e)
        {
            await TestProfileAsync();
            await TestContributionAsync();
            await TestOnlineIdentityAsync();
        }

        private static async Task TestOnlineIdentityAsync()
        {
            try
            {
                IEnumerable<OnlineIdentity> onlineIdentities = await App.API.GetOnlineIdentitiesAsync();
            }
            catch (Exception ex)
            {
                await MessageDialogManager.Current.ShowAsync(
                    "Error",
                    $"Error in GetOnlineIdentitiesAsync method. Error: {ex}");
            }
        }

        private static async Task TestContributionAsync()
        {
            IEnumerable<AwardContribution> contributionAreas = null;

            try
            {
                contributionAreas = await App.API.GetContributionAreasAsync();
            }
            catch (Exception ex)
            {
                await MessageDialogManager.Current.ShowAsync(
                    "Error",
                    $"Error in GetContributionAreasAsync method. Error: {ex}");
            }

            Contributions contributions = null;

            try
            {
                contributions = await App.API.GetContributionsAsync(0, 25);
            }
            catch (Exception ex)
            {
                await MessageDialogManager.Current.ShowAsync(
                    "Error",
                    $"Error in GetContributionsAsync method. Error: {ex}");
            }

            if (contributions != null)
            {
                try
                {
                    Contribution ctb = contributions.Items.FirstOrDefault();

                    Contribution contribution = await App.API.GetContributionByIdAsync(ctb.Id.Value);
                }
                catch (Exception ex)
                {
                    await MessageDialogManager.Current.ShowAsync(
                        "Error",
                        $"Error in GetContributionByIdAsync method. Error: {ex}");
                }
            }

            IEnumerable<ContributionType> contributionTypes = null;

            try
            {
                contributionTypes = await App.API.GetContributionTypesAsync();
            }
            catch (Exception ex)
            {
                await MessageDialogManager.Current.ShowAsync(
                    "Error",
                    $"Error in GetContributionTypesAsync method. Error: {ex}");
            }

            if (contributionTypes != null && contributionAreas != null)
            {
                ContributionType contributionType = contributionTypes.FirstOrDefault();
                AwardContribution awardContribution = contributionAreas.FirstOrDefault();
                ContributionArea area = awardContribution.Areas.FirstOrDefault();

                ContributionTechnology technology = new ContributionTechnology
                                     {
                                         AwardCategory = awardContribution.AwardCategory,
                                         AwardName = area.AwardName,
                                         Id = area.Items.FirstOrDefault().Id,
                                         Name = area.Items.FirstOrDefault().Name
                                     };

                Contribution newContribution = new Contribution
                                          {
                                              Id = 0,
                                              Type = contributionType,
                                              TypeName = contributionType.Name,
                                              Technology = technology,
                                              StartDate = DateTime.Now,
                                              Title = "MVP API Test",
                                              ReferenceUrl =
                                                  "https://github.com/jamesmcroft/mvp-api-portable",
                                              Visibility =
                                                  new ItemVisibility
                                                      {
                                                          Id = 299600000,
                                                          Description = "Everyone",
                                                          LocalizeKey =
                                                              "PublicVisibilityText"
                                                      },
                                              AnnualQuantity = 0,
                                              SecondAnnualQuantity = 0,
                                              AnnualReach = 0,
                                              Description = "Hello, World!"
                                          };

                Contribution submittedContribution = null;

                try
                {
                    submittedContribution = await App.API.AddContributionAsync(newContribution);
                }
                catch (Exception ex)
                {
                    await MessageDialogManager.Current.ShowAsync(
                        "Error",
                        $"Error in AddContributionAsync method. Error: {ex}");
                }

                if (submittedContribution != null)
                {
                    submittedContribution.Description = "This is a new description";

                    try
                    {
                        bool updated = await App.API.UpdateContributionAsync(submittedContribution);
                    }
                    catch (Exception ex)
                    {
                        await MessageDialogManager.Current.ShowAsync(
                            "Error",
                            $"Error in UpdateContributionAsync method. Error: {ex}");
                    }

                    try
                    {
                        bool deleted = await App.API.DeleteContributionAsync(submittedContribution.Id.Value);
                    }
                    catch (Exception ex)
                    {
                        await MessageDialogManager.Current.ShowAsync(
                            "Error",
                            $"Error in DeleteContributionAsync method. Error: {ex}");
                    }
                }
            }
        }

        private static async Task TestProfileAsync()
        {
            try
            {
                MVPProfile profile = await App.API.GetMyProfileAsync();
            }
            catch (Exception ex)
            {
                await MessageDialogManager.Current.ShowAsync("Error", $"Error in GetMyProfileAsync method. Error: {ex}");
            }

            try
            {
                MVPProfile profile = await App.API.GetProfileAsync("5001534");
            }
            catch (Exception ex)
            {
                await MessageDialogManager.Current.ShowAsync("Error", $"Error in GetProfileAsync method. Error: {ex}");
            }

            try
            {
                string profileImage = await App.API.GetMyProfileImageAsync();
            }
            catch (Exception ex)
            {
                await MessageDialogManager.Current.ShowAsync(
                    "Error",
                    $"Error in GetMyProfileImageAsync method. Error: {ex}");
            }
        }

        private void UpdateButtonStates()
        {
            if (App.API.Credentials != null)
            {
                this.LoginBtn.IsEnabled = false;
                this.PerformApiTestBtn.IsEnabled = true;
                this.LogoutBtn.IsEnabled = true;
            }
            else
            {
                this.LoginBtn.IsEnabled = true;
                this.PerformApiTestBtn.IsEnabled = false;
                this.LogoutBtn.IsEnabled = false;
            }
        }
    }
}