using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using T1708E_UWP.Entity;

namespace T1708E_UWP.Service
{
    class ApiHandle
    {
        public static string API_URL = "https://2-dot-backup-server-002.appspot.com/_api/v2/members";
        public static string SONG_API_URL = "https://2-dot-backup-server-002.appspot.com/_api/v2/songs";
        public static string SONG_API = "https://2-dot-backup-server-002.appspot.com/_api/v2/songs";
        public static string Bien;
        public async static Task<string> Sign_Up(Member member) {
            HttpClient httpClient = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(member), System.Text.Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(API_URL, content);
            var contents = await response.Result.Content.ReadAsStringAsync();
         
          
            Debug.WriteLine(contents);
            return contents;
        }

        public async static Task<string> Create_Song(Song song)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + ApiHandle.Bien);
            var content = new StringContent(JsonConvert.SerializeObject(song), System.Text.Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(SONG_API_URL, content);
            var contents = await response.Result.Content.ReadAsStringAsync();
            Debug.WriteLine(contents);
            return contents;
        }
    }
}
