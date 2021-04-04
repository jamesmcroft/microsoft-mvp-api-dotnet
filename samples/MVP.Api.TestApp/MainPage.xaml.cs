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
    using MADE.Data.Validation.Extensions;
    using MADE.Networking.Extensions;
    using Newtonsoft.Json;
    using XPlat.Storage;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const string CredentialsFileName = "credentials.json";

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            IStorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(CredentialsFileName, true);
            string credentialsJson = await file.ReadTextAsync();
            App.API.Credentials = credentialsJson.IsNullOrWhiteSpace()
                ? null
                : JsonConvert.DeserializeObject<MSACredentials>(credentialsJson);

            this.UpdateButtonStates();
        }

        private async void OnLoginClicked(object sender, RoutedEventArgs e)
        {
            await this.AuthenticateAsync();
        }

        private async Task AuthenticateAsync()
        {
            var scopes = new List<MSAScope> { MSAScope.Basic, MSAScope.Emails, MSAScope.OfflineAccess, MSAScope.SignIn };

            string authUri = App.API.RetrieveAuthenticationUri(scopes);

            WebAuthenticationResult result = await WebAuthenticationBroker.AuthenticateAsync(
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
                        string error = responseUri.GetQueryValue("error");
                        if (string.IsNullOrWhiteSpace(error))
                        {
                            string authCode = responseUri.GetQueryValue("code");
                            MSACredentials msaCredentials = await App.API.ExchangeAuthCodeAsync(authCode);
                            IStorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(CredentialsFileName, CreationCollisionOption.OpenIfExists);
                            await file.WriteTextAsync(JsonConvert.SerializeObject(msaCredentials));
                        }
                        else
                        {
                            await App.MessageDialogManager.ShowAsync(error);
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

        private async void OnPerformProfileApiTestsClicked(object sender, RoutedEventArgs e)
        {
            await TestProfileAsync();
        }

        private async void OnPerformOnlineIdentityApiTestsClicked(object sender, RoutedEventArgs e)
        {
            await TestOnlineIdentityAsync();
        }

        private async void OnPerformContributionApiTestsClicked(object sender, RoutedEventArgs e)
        {
            await TestContributionAsync();
        }

        private async void OnPerformAwardQuestionApiTestsClicked(object sender, RoutedEventArgs e)
        {
            await TestAwardQuestionAsync();
        }

        private static async Task TestAwardQuestionAsync()
        {
            try
            {
                IEnumerable<AwardQuestion> awardQuestions = await App.API.GetCurrentAwardQuestionsAsync();
            }
            catch (Exception ex)
            {
                await App.MessageDialogManager.ShowAsync(
                    "Error",
                    $"Error in GetCurrentAwardQuestionsAsync method. Error: {ex}");
                return;
            }

            try
            {
                IEnumerable<AwardQuestionAnswer> answers = await App.API.GetAwardQuestionAnswersAsync();
            }
            catch (Exception ex)
            {
                await App.MessageDialogManager.ShowAsync(
                    "Error",
                    $"Error in GetAwardQuestionAnswersAsync method. Error: {ex}");
                return;
            }
        }

        private static async Task TestOnlineIdentityAsync()
        {
            try
            {
                IEnumerable<OnlineIdentity> onlineIdentities = await App.API.GetOnlineIdentitiesAsync();
            }
            catch (Exception ex)
            {
                await App.MessageDialogManager.ShowAsync(
                    "Error",
                    $"Error in GetOnlineIdentitiesAsync method. Error: {ex}");
                return;
            }
        }

        private static async Task TestContributionAsync()
        {
            IEnumerable<ItemVisibility> visibilities;

            try
            {
                visibilities = await App.API.GetSharingPreferencesAsync();
            }
            catch (Exception ex)
            {
                await App.MessageDialogManager.ShowAsync(
                    "Error",
                    $"Error in {nameof(ApiClient.GetSharingPreferencesAsync)} method. Error: {ex}");
                return;
            }

            IEnumerable<AwardContribution> contributionAreas;

            try
            {
                contributionAreas = await App.API.GetContributionAreasAsync();
            }
            catch (Exception ex)
            {
                await App.MessageDialogManager.ShowAsync(
                    "Error",
                    $"Error in GetContributionAreasAsync method. Error: {ex}");
                return;
            }

            Contributions contributions;

            try
            {
                contributions = await App.API.GetContributionsAsync(0, 25);
            }
            catch (Exception ex)
            {
                await App.MessageDialogManager.ShowAsync(
                    "Error",
                    $"Error in GetContributionsAsync method. Error: {ex}");
                return;
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
                    await App.MessageDialogManager.ShowAsync(
                        "Error",
                        $"Error in GetContributionByIdAsync method. Error: {ex}");
                    return;
                }
            }

            IEnumerable<ContributionType> contributionTypes;

            try
            {
                contributionTypes = await App.API.GetContributionTypesAsync();
            }
            catch (Exception ex)
            {
                await App.MessageDialogManager.ShowAsync(
                    "Error",
                    $"Error in GetContributionTypesAsync method. Error: {ex}");
                return;
            }

            if (contributionTypes != null && contributionAreas != null)
            {
                ContributionType contributionType = contributionTypes.FirstOrDefault();
                AwardContribution awardContribution = contributionAreas.FirstOrDefault();
                ContributionArea area = awardContribution.Areas.FirstOrDefault();

                var technology = new ContributionTechnology
                                     {
                                         AwardCategory = awardContribution.AwardCategory,
                                         AwardName = area.AwardName,
                                         Id = area.Items.FirstOrDefault().Id,
                                         Name = area.Items.FirstOrDefault().Name
                                     };

                var newContribution = new Contribution
                                          {
                                              Type = contributionType,
                                              TypeName = contributionType.Name,
                                              Technology = technology,
                                              StartDate = DateTime.Now,
                                              Title = "MVP API Test",
                                              ReferenceUrl =
                                                  "https://github.com/jamesmcroft/mvp-api-portable",
                                              Visibility = visibilities.FirstOrDefault(),
                                              AnnualQuantity = 0,
                                              SecondAnnualQuantity = 0,
                                              AnnualReach = 0,
                                              Description = "Hello, World!"
                                          };

                Contribution submittedContribution;

                try
                {
                    submittedContribution = await App.API.AddContributionAsync(newContribution);
                }
                catch (Exception ex)
                {
                    await App.MessageDialogManager.ShowAsync(
                        "Error",
                        $"Error in AddContributionAsync method. Error: {ex}");
                    return;
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
                        await App.MessageDialogManager.ShowAsync(
                            "Error",
                            $"Error in UpdateContributionAsync method. Error: {ex}");
                        return;
                    }

                    try
                    {
                        bool deleted = await App.API.DeleteContributionAsync(submittedContribution.Id.Value);
                    }
                    catch (Exception ex)
                    {
                        await App.MessageDialogManager.ShowAsync(
                            "Error",
                            $"Error in DeleteContributionAsync method. Error: {ex}");
                        return;
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
                await App.MessageDialogManager.ShowAsync("Error", $"Error in GetMyProfileAsync method. Error: {ex}");
                return;
            }

            try
            {
                MVPProfile profile = await App.API.GetProfileAsync("5001534");
            }
            catch (Exception ex)
            {
                await App.MessageDialogManager.ShowAsync("Error", $"Error in GetProfileAsync method. Error: {ex}");
                return;
            }

            try
            {
                string profileImage = await App.API.GetMyProfileImageAsync();
            }
            catch (Exception ex)
            {
                await App.MessageDialogManager.ShowAsync(
                    "Error",
                    $"Error in GetMyProfileImageAsync method. Error: {ex}");
                return;
            }
        }

        private void UpdateButtonStates()
        {
            if (App.API.Credentials != null)
            {
                this.LoginBtn.IsEnabled = false;
                this.AwardQuestionBtn.IsEnabled = true;
                this.ContributionBtn.IsEnabled = true;
                this.OnlineIdentityBtn.IsEnabled = true;
                this.ProfileBtn.IsEnabled = true;
                this.LogoutBtn.IsEnabled = true;
            }
            else
            {
                this.LoginBtn.IsEnabled = true;
                this.AwardQuestionBtn.IsEnabled = false;
                this.ContributionBtn.IsEnabled = false;
                this.OnlineIdentityBtn.IsEnabled = false;
                this.ProfileBtn.IsEnabled = false;
                this.LogoutBtn.IsEnabled = false;
            }
        }
    }
}