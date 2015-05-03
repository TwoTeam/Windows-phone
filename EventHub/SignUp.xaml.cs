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
using Windows.UI.Notifications;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace EventHub
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignUp : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public SignUp()
        {
            this.InitializeComponent();

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
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
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
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private async void Butn_register_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string pattern = null;
            pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;
            //string email = Text_email.Text;
            if (Pass1.Password == Pass2.Password && Regex.IsMatch(Text_email.Text, pattern))
            {
                using (var client = new HttpClient()) ///naredi client za povezavo
                {
                    var values = new Dictionary<string, string>
                 {
                     {"username",Text_username.Text},
                     {"name",Text_name.Text}, 
                     {"surname",Text_surname.Text}, 
                     {"password", Pass1.Password}, 
                     {"email",Text_email.Text} 
                 };


                    progress2.IsActive = true;
                    progress2.Visibility = Visibility.Visible;

                    var content = new FormUrlEncodedContent(values);
                    var response = await client.PostAsync("http://veligovsek.si/events/apis/register.php", content);
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
                                var toast = new MessageDialog("Registration successfull!");
                                toast.ShowAsync();
                                this.Frame.Navigate(typeof(SignIn));
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
                            // output.Text = ex.ToString(); // error
                        }
                    }
                }
            }
            else
            {
                var toast = new MessageDialog("Please check the registered data!");
                toast.ShowAsync();
            }
        }
    }
}
