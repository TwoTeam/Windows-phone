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
using Windows.Storage;
using Windows.Storage.Streams;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace EventHub
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignIn : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public SignIn()
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

        private async void Login_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;
            
            using (var client = new HttpClient()) ///naredi client za povezavo
            {
                var values = new Dictionary<string, string>
                 {
                     {"user",email.Text}, 
                     {"password",password.Password} 
                 };

                progress1.IsActive = true;
                progress1.Visibility = Visibility.Visible;

                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync("http://veligovsek.si/events/apis/login.php", content);
                var responsesString = await response.Content.ReadAsStringAsync();
                JObject rezultati = JObject.Parse(responsesString);

                foreach (var result in rezultati["result"])
                {
                    try
                    {
                        if (result["response"].ToString().ToLower() == "true")
                        {
                            uporabnik.user = email.Text;
                            this.Frame.Navigate(typeof(PivotPage));
                            progress1.IsActive = false;
                            progress1.Visibility = Visibility.Collapsed;


                            

                        }
                        else
                        {
                            //this.Frame.Navigate(typeof(BasicPage1));

                            progress1.IsActive = false;
                            progress1.Visibility = Visibility.Collapsed;
                            var krnek = new MessageDialog(result["message"].ToString()); // error
                            krnek.ShowAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        //output.Text = ex.ToString(); // error
                    }
                }
            }
        }

/*        public async Task SaveAsync()
        {
            StorageFile userdetailsfile = await ApplicationData.Current.LocalFolder.CreateFileAsync("UserDetails",
            CreationCollisionOption.ReplaceExisting);
            IRandomAccessStream raStream = await userdetailsfile.OpenAsync(FileAccessMode.ReadWrite);
            using (IOutputStream outStream = raStream.GetOutputStreamAt(0))
            {
                // Serialize the Session State.
                DataContractSerializer serializer = new DataContractSerializer(typeof(uporabnik));
                serializer.WriteObject(outStream.AsStreamForWrite(), Stats);
                await outStream.FlushAsync();
            }
        }*/

        private void forr_Tapped(object sender, TappedRoutedEventArgs e)
        {
            progress1.IsActive = true;
            progress1.Visibility = Visibility.Visible;
            this.Frame.Navigate(typeof(ItemPage));
            progress1.IsActive = false;
            progress1.Visibility = Visibility.Collapsed;
        }

        private void sign_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SignUp));
        }

        private async void butn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            HttpClient http = new HttpClient();
            var odgovor = await http.GetStringAsync("http://veligovsek.si/events/apis/user.php?email=mitja.miklav@gmail.com");

            Class2 myClass2 = null;
            //var FSfeed = JsonConvert.DeserializeObject<List<Class2>>(odgovor);
            try
            {
                var jsonSerializer = new DataContractJsonSerializer(typeof(Class2));

                using (var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(odgovor)))
                {
                    myClass2 = jsonSerializer.ReadObject(stream) as Class2;
                }
            }
            catch (Exception ex)
            {
                email.Text = ex.ToString();
            }
            //email.Text = myClass2.username;
        }






    }
}
