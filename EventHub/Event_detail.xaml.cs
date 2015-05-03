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
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace EventHub
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Event_detail : Page
    {
        public Event_detail()
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
            var myObject = (Class1) e.Parameter;
            if (!String.IsNullOrEmpty(myObject.title))
            { 
            tit.Text = myObject.title;
            des.Text = myObject.description;
            loc.Text = myObject.location;
            sta.Text = myObject.start;
            end.Text = myObject.end;
             }
           // Uri uri = myObject.image;
            //ImageSource imgSource = myObject.image;
            string abc = myObject.image;

            //Image img = new Image();
            asd.Source = new BitmapImage(new Uri(abc));

            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        private void go_Tapped(object sender, TappedRoutedEventArgs e)
        {
           
        }



        void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            e.Handled = true;
            Windows.Phone.UI.Input.HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            this.Frame.Navigate(typeof(PivotPage));
        }

        private void Grid_Holding(object sender, HoldingRoutedEventArgs e)
        {
            FrameworkElement senderElement = sender as FrameworkElement;
            // If you need the clicked element:
            // Item whichOne = senderElement.DataContext as Item;
            FlyoutBase flyoutBase = FlyoutBase.GetAttachedFlyout(senderElement);
            flyoutBase.ShowAt(senderElement);
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // get the clicked element:
            Class1 datacontext = (e.OriginalSource as FrameworkElement).DataContext as Class1;
            await new MessageDialog("Edit").ShowAsync();
        }

        }
        
    }

