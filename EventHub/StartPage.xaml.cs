using EventHub.Common;
using EventHub.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Net.Http;
using Windows.UI.Popups;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.Azure;
using Microsoft.WindowsAzure;
using System.Diagnostics;
using Windows.Data.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// The Pivot Application template is documented at http://go.microsoft.com/fwlink/?LinkID=391641

namespace EventHub
{
    public sealed partial class PivotPage : Page
    {
        private const string FirstGroupName = "FirstGroup";
        private const string SecondGroupName = "SecondGroup";

        private readonly NavigationHelper navigationHelper;
        private readonly ObservableDictionary defaultViewModel = new ObservableDictionary();
        private readonly ResourceLoader resourceLoader = ResourceLoader.GetForCurrentView("Resources");

        private HttpClient httpClient;
        private HttpResponseMessage response;

        public PivotPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>.
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private async void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            // TODO: Create an appropriate data model for your problem domain to replace the sample data
            var sampleDataGroup = await SampleDataSource.GetGroupAsync("Group-1");
            this.DefaultViewModel[FirstGroupName] = sampleDataGroup;
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache. Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/>.</param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            // TODO: Save the unique state of the page here.
        }

        /// <summary>
        /// Adds an item to the list when the app bar button is clicked.
        /// </summary>
        private void AddAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            string groupName = this.pivot.SelectedIndex == 0 ? FirstGroupName : SecondGroupName;
            var group = this.DefaultViewModel[groupName] as SampleDataGroup;
            var nextItemId = group.Items.Count + 1;
            var newItem = new SampleDataItem(
                string.Format(CultureInfo.InvariantCulture, "Group-{0}-Item-{1}", this.pivot.SelectedIndex + 1, nextItemId),
                string.Format(CultureInfo.CurrentCulture, this.resourceLoader.GetString("NewItemTitle"), nextItemId),
                string.Empty,
                string.Empty,
                this.resourceLoader.GetString("NewItemDescription"),
                string.Empty);

            group.Items.Add(newItem);

            // Scroll the new item into view.
            var container = this.pivot.ContainerFromIndex(this.pivot.SelectedIndex) as ContentControl;
            var listView = container.ContentTemplateRoot as ListView;
            listView.ScrollIntoView(newItem, ScrollIntoViewAlignment.Leading);
        }

        /// <summary>
        /// Invoked when an item within a section is clicked.
        /// </summary>
        private void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            var itemId = ((SampleDataItem)e.ClickedItem).UniqueId;
            if (!Frame.Navigate(typeof(ItemPage), itemId))
            {
                throw new Exception(this.resourceLoader.GetString("NavigationFailedExceptionMessage"));
            }
        }

        /// <summary>
        /// Loads the content for the second pivot item when it is scrolled into view.
        /// </summary>
        private async void SecondPivot_Loaded(object sender, RoutedEventArgs e)
        {
            var sampleDataGroup = await SampleDataSource.GetGroupAsync("Group-2");
            this.DefaultViewModel[SecondGroupName] = sampleDataGroup;
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion


        private void Text_Forgot_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ItemPage));
        }

        private async void Butn_login_Tapped(object sender, TappedRoutedEventArgs e)
        {
            using (var client = new HttpClient()) ///krneki nardi client za povezavo
            {
                var values = new Dictionary<string, string>
                 {
                     {"user",Text_email1.Text}, //tu more bit enako ko je na loginu email pol pa lahka das namest nikolaj.colic... neki.txt
                     {"password",Pass.Password} // tu more bit pass pa pol lahka das isto ko prej krneki pać
                 };
                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync("http://veligovsek.si/events/apis/login.php", content);
                var responsesString = await response.Content.ReadAsStringAsync();
                //txt.Text = responsesString;
                //.Text = responsesString.ToString();
                JObject rezultati = JObject.Parse(responsesString);
                foreach (var result in rezultati["result"])
                {
                    try
                    {
                        if (result["response"].ToString().ToLower() == "true")
                        {
                            this.Frame.Navigate(typeof(BasicPage1));
                        }
                        else
                        {
                            var krnek = new MessageDialog(result["message"].ToString()); // error
                            krnek.ShowAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                       // output.Text = ex.ToString(); // error
                    }
                }


                /*response = new HttpResponseMessage();
                 test.Text = "";
                 string inputAddress = "http://obm.hostingsiteforfree.com/windows/login.php";

                 Uri resourceUri;
                 if (!Uri.TryCreate(inputAddress.Trim(), UriKind.Absolute, out resourceUri))
                 {
                     test.Text = "Invalid URI, please re-enter a valid URI";
                     return;
                 }
                 if (resourceUri.Scheme != "http" && resourceUri.Scheme != "https")
                 {
                     test.Text = "Only 'http' and 'https' schemes supported. Please re-enter URI";
                     return;
                 }

                 string responseBodyAsText;
                 test.Text = "Waiting for response ...";

                 try
                 {
                     response = await httpClient.GetAsync(resourceUri);

                     response.EnsureSuccessStatusCode();

                     responseBodyAsText = await response.Content.ReadAsStringAsync();

                 }
                 catch (Exception ex)
                 {
                     // Need to convert int HResult to hex string
                     test.Text = "Error = " + ex.HResult.ToString("X") +
                         "  Message: " + ex.Message;
                     responseBodyAsText = "";
                 }
                 test.Text = response.StatusCode + " " + response.ReasonPhrase;

                 // Format the HTTP response to display better
                 responseBodyAsText = responseBodyAsText.Replace("<br>", Environment.NewLine);
                 output.Text = responseBodyAsText;*/
            }
        }


    }
    }