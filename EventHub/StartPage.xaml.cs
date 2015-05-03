using EventHub.Common;
using EventHub.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
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
using Windows.UI.StartScreen;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Streams;
using System.Runtime.Serialization;

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
        /// 
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
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
            HttpClient http = new HttpClient();
            string str = ((ComboBoxItem)cbox.SelectedItem).Content.ToString();
            var response = await http.GetStringAsync("http://veligovsek.si/events/apis/facebook_events.php?city=" + str);

            var FSfeed = JsonConvert.DeserializeObject<List<Class1>>(response);

            Reviews.ItemsSource = FSfeed;
            
            //test.Text = uporabnik.user;
        }



        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        public const string appbarTileId = "MySecondaryTile";
        private void ToggleAppBarButton(bool showPinButton)
        {
            if (showPinButton)
            {
                this.PinUnPinCommandButton.Label = "Pin to Start";
            }
            else
            {
                this.PinUnPinCommandButton.Label = "Unpin from Start";
            }

            this.PinUnPinCommandButton.UpdateLayout();
        }

        void Init()
        {
            ToggleAppBarButton(!SecondaryTile.Exists(PivotPage.appbarTileId));
        }
        private async void PinUnPinCommandButton_Click(object sender, RoutedEventArgs e)
        {
            string displayName = "EventHub";
            string tileActivationArguments = PivotPage.appbarTileId + " was pinned at=" + DateTime.Now.ToLocalTime().ToString();
            Uri square150x150Logo = new Uri("ms-appx:///Assets/150x150.png");
            TileSize newTileDesiredSize = TileSize.Square150x150;

            SecondaryTile secondaryTile = new SecondaryTile(PivotPage.appbarTileId,
                                                displayName,
                                                tileActivationArguments,
                                                square150x150Logo,
                                                TileSize.Square150x150);

            secondaryTile.VisualElements.ShowNameOnSquare150x150Logo = true;
            secondaryTile.VisualElements.ForegroundText = ForegroundText.Dark;
            secondaryTile.VisualElements.Square30x30Logo = new Uri("ms-appx:///Assets/30x30.png");

            //Windows.Foundation.Rect rect = BasicPage1.GetElementRect
            Windows.UI.Popups.Placement placement = Windows.UI.Popups.Placement.Above;

            bool isPinned = true;
            await secondaryTile.RequestCreateAsync();
        }

        private void about_click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(About));
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SignIn));
        }

        private void capture_Click(object sender, RoutedEventArgs e)
        {
            string str = ((ComboBoxItem)cbox.SelectedItem).Content.ToString();
        }

        private void Reviews_Tapped(object sender, TappedRoutedEventArgs e)
        {

            Class1 myobject = Reviews.SelectedItem as Class1;

         
            Frame.Navigate(typeof(Event_detail), myobject);
            
            //ena.Text = myobject.id;
        }

        private void Reviews_Holding(object sender, HoldingRoutedEventArgs e)
        {
            FrameworkElement senderElement = sender as FrameworkElement;
            FlyoutBase flyoutBase = FlyoutBase.GetAttachedFlyout(senderElement);
            flyoutBase.ShowAt(senderElement);

            FrameworkElement element = (FrameworkElement)e.OriginalSource;
             if (element.DataContext != null && element.DataContext is Class1)
             {
                 Class1 selectedOne = (Class1)element.DataContext;
                 // rest of the code
                 //ena.Text = selectedOne.id;
                 ajdi.aj = selectedOne.id;
                 //asd.Text = ajdi.aj + " ";
             }

        }


        private async void pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HttpClient http = new HttpClient();
            string str = ((ComboBoxItem)cbox.SelectedItem).Content.ToString();
            var response = await http.GetStringAsync("http://veligovsek.si/events/apis/facebook_events.php?city=" + str);
            var FSfeed = JsonConvert.DeserializeObject<List<Class1>>(response);

            Reviews.ItemsSource = FSfeed;
        }



        private async void fav_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void hid_Click(object sender, RoutedEventArgs e)
        {
            //Class1 myobject = Reviews.SelectedItem as Class1;
            //ena.Text = ajdi.aj;
            //dva.Text = uporabnik.user;

            using (var client = new HttpClient()) ///naredi client za povezavo
            {
                var values = new Dictionary<string, string>
                 {
                     {"user", "xklemenx@gmail.com"},
                     {"event", "218"}
                 };

                progress2.IsActive = true;
                progress2.Visibility = Visibility.Visible;

                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync("http://www.veligovsek.si/events/apis/hide.php", content);
                var responsesString = await response.Content.ReadAsStringAsync();
                JObject rezultati = JObject.Parse(responsesString);
                foreach (var result in rezultati["result"])
                {

                    try
                    {

                        if (result["response"].ToString().ToLower() == "true")
                        {
                            progress2.IsActive = false;
                            progress2.Visibility = Visibility.Collapsed;
                            var toast = new MessageDialog("success!");
                            toast.ShowAsync();
                        }
                        else
                        {
                            progress2.IsActive = false;
                            progress2.Visibility = Visibility.Collapsed;
                            var krnek = new MessageDialog(result["message"].ToString()); // error
                            krnek.ShowAsync();

                        }
                    }
                    catch (Exception ex)
                    {
                        //output.Text = ex.ToString();  error
                        
                    }
                }
            }
        }


    }
}


        
    