using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using T1708E_UWP.Entity;
using T1708E_UWP.Service;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using ApiHandle = T1708E_UWP.Service.ApiHandle;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace T1708E_UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SongForm : Page
    {
        private Song currentSong;
        public SongForm()
        {
            this.currentSong = new Song();
            this.InitializeComponent();
            this.Player.MediaPlayer.Play();
        }

        private async void BtnSignup_Click_1(object sender, RoutedEventArgs e)
        {
            // do validate first.
            this.currentSong.name = this.Name.Text;
            this.currentSong.description = this.Description.Text;
            this.currentSong.singer = this.Singer.Text;
            this.currentSong.author = this.Author.Text;
            this.currentSong.thumbnail = this.Thumbnail.Text;
            this.currentSong.link = this.Link.Text;
            if (this.currentSong.name == "")
            {
                name.Text = "Email khong duoc de trong!";
                name.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
            }
            else
            {
                name.Text = "";
            }
            if (this.currentSong.description == "")
            {
                description.Text = "Email khong duoc de trong!";
                description.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
            }
            else
            {
                description.Text = "";
            }
            if (this.currentSong.singer == "")
            {
                singer.Text = "Email khong duoc de trong!";
                singer.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
            }
            else
            {
                singer.Text = "";
            }
            if (this.currentSong.author == "")
            {
                author.Text = "Email khong duoc de trong!";
                author.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
            }
            else
            {
                author.Text = "";
            }
            if (this.currentSong.thumbnail == "")
            {
                thumbnail.Text = "Email khong duoc de trong!";
                thumbnail.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
            }
            else
            {
                thumbnail.Text = "";
            }
            if (this.currentSong.link == "")
            {
                link.Text = "Email khong duoc de trong!";
                link.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
            }
            else
            {
                link.Text = "";
            }

            if ((this.currentSong.name != "" && this.currentSong.description != "" && this.currentSong.singer != "" && this.currentSong.author != "" && this.currentSong.thumbnail != "" && this.currentSong.link != ""))
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + ApiHandle.Bien);
                var content = new StringContent(JsonConvert.SerializeObject(this.currentSong), System.Text.Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync(ApiHandle.SONG_API_URL, content);
                var contents = await response.Result.Content.ReadAsStringAsync();
                var resopnse1 = httpClient.PostAsync(ApiHandle.SONG_API_URL, content).Result;
                if (resopnse1.StatusCode != System.Net.HttpStatusCode.Created)
                {
                    ErrorResponse errorObject = JsonConvert.DeserializeObject<ErrorResponse>(contents);

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
                
                Debug.WriteLine(contents);
                //await ApiHandle.Create_Song(this.currentSong);
                Debug.WriteLine("Action success.");
            }
         
        }

        private void hientoken(object sender, RoutedEventArgs e)
        {
          
        }
    }
}
