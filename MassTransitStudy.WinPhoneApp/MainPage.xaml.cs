namespace MassTransitStudy.WinPhoneApp
{
    using System;
    using System.Collections.Generic;

    using Windows.UI.Popups;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    using MassTransitStudy.Messages;

    using RestSharp;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void LoadMessagesButtonClick(object sender, RoutedEventArgs e)
        {
            var client = new ApiClient(this.ApiServiceBaseAddressTextBox.Text);
            client.SaveSampleMessageAsync(
                new SampleMessage
                    {
                        Data = "Whatever",
                        Timestamp = DateTime.UtcNow
                    },
                this.SaveSampleMessageCallback);
            
            client.GetSampleMessagesAsync(0, 100, this.GetSampleMessagesCallback);
        }

        private async void SaveSampleMessageCallback(IRestResponse<SampleMessage> restResponse, RestRequestAsyncHandle restRequestAsyncHandle)
        {
            var message = new MessageDialog(restResponse.Data.Data);
            await message.ShowAsync();
        }

        private void GetSampleMessagesCallback(
            IRestResponse<List<SampleMessage>> restResponse,
            RestRequestAsyncHandle restRequestAsyncHandle)
        {
            LoadMessagesButton.Content = restResponse.Data.Count;

            MessagesListView.ItemsSource = restResponse.Data;
        }
    }
}
