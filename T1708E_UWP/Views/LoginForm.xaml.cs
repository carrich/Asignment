using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using T1708E_UWP.Entity;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using T1708E_UWP.Service;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace T1708E_UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginForm : Page
    {
        private static string API_LOGIN = "http://2-dot-backup-server-002.appspot.com/_api/v2/members/authentication";
       

        public LoginForm()
        {
            this.InitializeComponent();
        }



        private void Login_Handle(object sender, TappedRoutedEventArgs e)
        {
            var Email_txt = Email.Text;
            var Password_txt = Password.Password.ToString();


            if (Email_txt == "")
            {
                email.Text = "Email khong duoc de trong!";
                email.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
            }
            else
            {
                email.Text = "";
            }

            if (Password_txt == "")
            {
                password.Text = "Mat khau khong duoc de trong!";
                password.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
            }
            else
            {
                password.Text = "";
            }

            //if (Email_txt != "" && Password_txt != "")
            //{
            //    this.Hide();
            //}
        }

        //private async System.Threading.Tasks.Task Button_ClickAsync(object sender, RoutedEventArgs e)
        //{
        //    Dictionary<String, String> LoginInfor = new Dictionary<string, string>();
        //    LoginInfor.Add("email", this.Email.Text);
        //    LoginInfor.Add("password", this.Password.Password);

        //    // Lay token
        //    HttpClient httpClient = new HttpClient();
        //    StringContent content = new StringContent(JsonConvert.SerializeObject(LoginInfor), System.Text.Encoding.UTF8, "application/json");
        //    var response = httpClient.PostAsync(API_LOGIN, content).Result;
        //    var responseContent = await response.Content.ReadAsStringAsync();
        //    Debug.WriteLine(response);
        //    Debug.WriteLine(responseContent);
        //    if (response.StatusCode == System.Net.HttpStatusCode.Created)
        //    {
        //        Debug.WriteLine("in if");
        //        // save file...
        //        Debug.WriteLine(responseContent);
        //        // Doc token
        //        TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(responseContent);

        //        // Luu token
        //        StorageFolder folder = ApplicationData.Current.LocalFolder;
        //        StorageFile file = await folder.CreateFileAsync("token.txt", CreationCollisionOption.ReplaceExisting);
        //        await FileIO.WriteTextAsync(file, responseContent);

        //        // Lay thong tin ca nhan bang token.
        //        HttpClient client2 = new HttpClient();
        //        client2.DefaultRequestHeaders.Add("Authorization", "Basic " + token.token);
        //        var resp = client2.GetAsync("http://2-dot-backup-server-002.appspot.com/_api/v2/members/information").Result;
        //        Debug.WriteLine(await resp.Content.ReadAsStringAsync());
        //        Debug.WriteLine(resp);

        //        var rootFrame = Window.Current.Content as Frame;
        //        this.Hide();
        //        rootFrame.Navigate(typeof(SplitView));
        //    }
        //    else
        //    {
        //        // Xu ly loi.
        //        ErrorResponse errorObject = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
        //        if (errorObject != null && errorObject.error.Count > 0)
        //        {
        //            foreach (var key in errorObject.error.Keys)
        //            {
        //                var textMessage = this.FindName(key);
        //                if (textMessage == null)
        //                {
        //                    continue;
        //                }
        //                TextBlock textBlock = textMessage as TextBlock;

        //                textBlock.Text = errorObject.error[key];
        //                textBlock.Visibility = Visibility.Visible;
        //            }
        //        }
        //    }
        //}

        private void Hide()
        {
            throw new NotImplementedException();
        }

        //private void Sign_Up(object sender, TappedRoutedEventArgs e)
        //{
        //    var rootFrame = Window.Current.Content as Frame;
        //    this.Hide();
        //    if (rootFrame != null) rootFrame.Navigate(typeof(Views.Sign));
        //}

        private async void handleclick(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("ag");
            var Email_txt = Email.Text;
            var Password_txt = Password.Password.ToString();


            if (Email_txt == "")
            {
                email.Text = "Email khong duoc de trong!";
                email.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
            }
            else
            {
                email.Text = "";
            }

            if (Password_txt == "")
            {
                password.Text = "Mat khau khong duoc de trong!";
                password.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
            }
            else
            {
                password.Text = "";
            }

            //if (Email_txt != "" && Password_txt != "")
            //{
            //    this.Hide();
            //}
            Dictionary<String, String> LoginInfor = new Dictionary<string, string>();
            LoginInfor.Add("email", this.Email.Text);
            LoginInfor.Add("password", this.Password.Password);

            // Lay token
            HttpClient httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(LoginInfor),
                System.Text.Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(API_LOGIN, content).Result;
            var responseContent = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(response);
            Debug.WriteLine(responseContent);
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                Debug.WriteLine("in if");
                // save file...
                Debug.WriteLine(responseContent);
                // Doc token
                TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(responseContent);

                // Luu token
                StorageFolder folder = ApplicationData.Current.LocalFolder;
                StorageFile file = await folder.CreateFileAsync("token.txt", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(file, responseContent);
                ApiHandle.Bien = token.token;
                // Lay thong tin ca nhan bang token.
                HttpClient client2 = new HttpClient();
                client2.DefaultRequestHeaders.Add("Authorization", "Basic " + token.token);
               
                var resp = client2.GetAsync("http://2-dot-backup-server-002.appspot.com/_api/v2/members/information")
                    .Result;
                Debug.WriteLine(await resp.Content.ReadAsStringAsync());
                Debug.WriteLine(resp);

                var rootFrame = Window.Current.Content as Frame;
               rootFrame.Navigate(typeof(Views.SongList));
            }
            else
            {
                // Xu ly loi.
                ErrorResponse errorObject = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
                if (errorObject != null && errorObject.error.Count > 0)
                {
                    foreach (var key in errorObject.error.Keys)
                    {
                        var textMessage = this.FindName(key);
                        if (textMessage == null)
                        {
                            continue;
                        }

                        TextBlock textBlock = textMessage as TextBlock;

                        textBlock.Text = errorObject.error[key];
                        textBlock.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void SignUp(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("ok");
            var rootFrame = Window.Current.Content as Frame;
         
            if (rootFrame != null) rootFrame.Navigate(typeof(Views.Sign));
        }
    }
}
