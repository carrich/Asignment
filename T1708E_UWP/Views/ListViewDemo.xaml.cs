using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using T1708E_UWP.Data;
using T1708E_UWP.Entity;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace T1708E_UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListViewDemo : Page
    {
        private bool isPlaying = false;

        int onPlay = 0;

        TimeSpan _position;

        DispatcherTimer _timer = new DispatcherTimer();

        private ObservableCollection<Song> listSong;

        internal ObservableCollection<Song> ListSong { get => listSong; set => listSong = value; }

        public ListViewDemo()
        {
            this.ListSong = new ObservableCollection<Song>();
            this.ListSong.Add(new Song()
            {
                name = "Chưa bao giờ",
                singer = "Hà Anh Tuấn",
                thumbnail = "https://file.tinnhac.com/resize/600x-/music/2017/07/04/19554480101556946929-b89c.jpg",
                link = "https://c1-ex-swe.nixcdn.com/NhacCuaTui963/ChuaBaoGioSEESINGSHARE2-HaAnhTuan-5111026.mp3"
            });
            this.ListSong.Add(new Song()
            {
                name = "Tình thôi xót xa",
                singer = "Hà Anh Tuấn",
                thumbnail = "https://i.ytimg.com/vi/XyjhXzsVdiI/maxresdefault.jpg",
                link = "https://c1-ex-swe.nixcdn.com/NhacCuaTui963/TinhThoiXotXaSEESINGSHARE1-HaAnhTuan-4652191.mp3"
            });
            this.ListSong.Add(new Song() {
                name="Tháng tư là tháng nói dối của em",
                singer="Hà Anh Tuấn",
                thumbnail= "https://sky.vn/wp-content/uploads/2018/05/0-30.jpg",
                link = "https://od.lk/s/NjFfMjM4MzQ1OThf/ThangTuLaLoiNoiDoiCuaEm-HaAnhTuan-4609544.mp3"
            });
            this.ListSong.Add(new Song()
            {
                name = "Nơi ấy bình yên",
                singer = "Hà Anh Tuấn",
                thumbnail = "https://i.ytimg.com/vi/A8u_fOetSQc/hqdefault.jpg",
                link = "https://c1-ex-swe.nixcdn.com/NhacCuaTui946/NoiAyBinhYenSeeSingShare2-HaAnhTuan-5085337.mp3"
            });
            this.ListSong.Add(new Song()
            {
                name = "Giấc mơ chỉ là giấc mơ",
                singer = "Hà Anh Tuấn",
                thumbnail = "https://i.ytimg.com/vi/J_VuNwxSEi0/maxresdefault.jpg",
                link = "https://c1-ex-swe.nixcdn.com/NhacCuaTui945/GiacMoChiLaGiacMoSeeSingShare2-HaAnhTuan-5082049.mp3"
            });
            this.ListSong.Add(new Song()
            {
                name = "Người tình mùa đông",
                singer = "Hà Anh Tuấn",
                thumbnail = "https://i.ytimg.com/vi/EXAmxBxpZEM/maxresdefault.jpg",
                link = "https://c1-ex-swe.nixcdn.com/NhacCuaTui963/NguoiTinhMuaDongSEESINGSHARE2-HaAnhTuan-5104816.mp3"
            });
            this.InitializeComponent();
            this.VolumeSlider.Value = 100;
            _timer.Interval = TimeSpan.FromMilliseconds(1000);
            _timer.Tick += ticktock;
            _timer.Start();
        }

        private void ticktock(object sender, object e)
        {
            MinDuration.Text = MediaPlayer.Position.Minutes + ":"+ MediaPlayer.Position.Seconds;
            Progress.Minimum = 0;
            Progress.Maximum = MediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
            MaxDuration.Text = MediaPlayer.NaturalDuration.TimeSpan.Minutes + ":" + MediaPlayer.NaturalDuration.TimeSpan.Seconds;
            Progress.Value = MediaPlayer.Position.TotalSeconds;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void StackPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            StackPanel panel = sender as StackPanel;
            Song selectedSong =  panel.Tag as Song;
            Debug.WriteLine(ListSong[0].name);
            onPlay = MenuList.SelectedIndex;
            LoadSong(selectedSong);
            PlaySong();
        }
        private void PlaySong()
        {
            MediaPlayer.Play();
            PlayButton.Icon = new SymbolIcon(Symbol.Pause);
            isPlaying = true;
        }
        private void PauseSong()
        {
            MediaPlayer.Pause();
            PlayButton.Icon = new SymbolIcon(Symbol.Play);
            isPlaying = false;
        }        
        private void PlayClick(object sender, RoutedEventArgs e)
        {            
            if (isPlaying)
            {
                PauseSong();
            }
            else
            {
                PlaySong();
            }
        }
        private void PlayBack(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Stop();
            if (onPlay > 0)
            {
                onPlay = onPlay - 1;
            }
            else
            {
                onPlay = ListSong.Count - 1;
            }
            LoadSong(ListSong[onPlay]);
            PlaySong();
            MenuList.SelectedIndex = onPlay;
        }

        private void PlayNext(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Stop();
            if (onPlay < ListSong.Count - 1)
            {
                onPlay = onPlay + 1;
            }
            else
            {
                onPlay = 0;
            }
            LoadSong(ListSong[onPlay]);
            PlaySong();
            MenuList.SelectedIndex = onPlay;
        }
        private void LoadSong(Entity.Song currentSong)
        {
            this.NowPlaying.Text = "Loading";
            MediaPlayer.Source = new Uri(currentSong.link);            
            Debug.WriteLine(MediaPlayer.NaturalDuration.TimeSpan.TotalSeconds);
            this.NowPlaying.Text = currentSong.name + " - " + currentSong.singer;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (MediaPlayer.Source != null && MediaPlayer.NaturalDuration.HasTimeSpan)
            {
                Progress.Minimum = 0;
                Progress.Maximum = MediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                Progress.Value = MediaPlayer.Position.TotalSeconds;

            }
        }      

        private void VolumeSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Slider vol = sender as Slider;
            if (vol != null)
            {               
                MediaPlayer.Volume = vol.Value / 100;
                this.volume.Text = vol.Value.ToString();
            }
        }
        
    }
}
