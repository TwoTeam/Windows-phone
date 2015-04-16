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
using Windows.UI.Xaml.Controls;
using Windows.Media.Capture;


// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace EventHub
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BasicPage1 : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        public BasicPage1()
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
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        public async void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {

        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
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


            var response = await http.GetStringAsync("http://www.veligovsek.si/events/apis/events.php");

            var FSfeed = JsonConvert.DeserializeObject<List<Class1>>(response);
            Reviews.ItemsSource = FSfeed;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion


        private void logout_Click(object sender, RoutedEventArgs e)
        {
                this.Frame.Navigate(typeof(PivotPage));
        }

        private void about_click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(About));
        }
        //-----------------------------------------------------------------------------------------------

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
            ToggleAppBarButton(!SecondaryTile.Exists(BasicPage1.appbarTileId));
        }

        private async void PinUnPinCommandButton_Click(object sender, RoutedEventArgs e)
        {
            string displayName = "EventHub";
            string tileActivationArguments = BasicPage1.appbarTileId + " was pinned at=" + DateTime.Now.ToLocalTime().ToString();
            Uri square150x150Logo = new Uri("ms-appx:///Assets/150x150.png");
            TileSize newTileDesiredSize = TileSize.Square150x150;

            SecondaryTile secondaryTile = new SecondaryTile(BasicPage1.appbarTileId,
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

        private async void capture_Click(object sender, RoutedEventArgs e)
        {

        }

       /* private async Task<RenderTargetBitmap> CreateBitmapFromElement(FrameworkElement uielement)
        {
            try
            {
                var renderTargetBitmap = new RenderTargetBitmap();
                await renderTargetBitmap.RenderAsync(uielement);

                return renderTargetBitmap;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return null;
        }*/





    }
}
