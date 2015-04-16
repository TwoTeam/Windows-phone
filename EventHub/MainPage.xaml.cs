using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.StartScreen;
using Windows.UI.Xaml.Controls;
using Windows.Foundation;
using Windows.System;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace EventHub
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private async void PinUnPinCommandButton_Click(object sender, RoutedEventArgs e)
        {
            SecondaryTile secondaryTile = new SecondaryTile(MainPage.appbarTileId,
                                        "Secondary tile pinned through app bar",
                                        MainPage.appbarTileId + " was pinned at=" + DateTime.Now.ToLocalTime().ToString(),
                                        new Uri("ms-appx:///Assets/150x150.png"),
                                        TileSize.Square150x150);


            Windows.UI.Popups.Placement placement = Windows.UI.Popups.Placement.Above;
            await secondaryTile.RequestCreateAsync();

        }

        public const string appbarTileId = "MySecondaryTile";

        private void ToggleAppBarButton(bool showPinButton)
        {
            if (showPinButton)
            {
                this.PinUnPinCommandButton.Label = "Pin to Start";
                this.PinUnPinCommandButton.Icon = new SymbolIcon(Symbol.Pin);
            }
            else
            {
                this.PinUnPinCommandButton.Label = "Unpin from Start";
                this.PinUnPinCommandButton.Icon = new SymbolIcon(Symbol.UnPin);
            }

            this.PinUnPinCommandButton.UpdateLayout();
        }

        void Init()
        {
            ToggleAppBarButton(!SecondaryTile.Exists(MainPage.appbarTileId));
           // PinUnPinCommandButton.Click += pinToAppBar_Click;
        }





    }


}
