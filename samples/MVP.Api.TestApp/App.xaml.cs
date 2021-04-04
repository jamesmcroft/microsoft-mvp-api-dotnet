namespace MVP.Api.TestApp
{
    using System;

    using Windows.ApplicationModel;
    using Windows.ApplicationModel.Activation;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;
    using MADE.Diagnostics;
    using MADE.Diagnostics.Logging;
    using Services;

    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
        public const string ClientId = "YOUR_CLIENT_ID";

        public const string ClientSecret = "YOUR_CLIENT_SECRET";

        public const string SubscriptionKey = "YOUR_SUBSCRIPTION_KEY";

        public static ApiClient API { get; set; }

        public static FileEventLogger Eventlogger { get; private set; }

        public static MessageDialogManager MessageDialogManager { get; set; }

        public AppDiagnostics AppDiagnostics { get; private set; }

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += this.OnSuspending;

            API = new ApiClient(ClientId, ClientSecret, SubscriptionKey);
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            if (!(Window.Current.Content is Frame rootFrame))
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += this.OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // TODO: Load state from previously suspended application
                }

                Eventlogger = new FileEventLogger();
                this.AppDiagnostics = new AppDiagnostics(Eventlogger);
                MessageDialogManager = new MessageDialogManager(Window.Current.Dispatcher);
                await this.AppDiagnostics.StartRecordingDiagnosticsAsync();

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated)
            {
                return;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }

            // Ensure the current window is active
            Window.Current.Activate();
        }


        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            SuspendingDeferral deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }
    }
}