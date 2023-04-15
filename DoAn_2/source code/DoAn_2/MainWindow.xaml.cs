using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Runtime.CompilerServices;
using System.Drawing;
using WMPLib;
using Path = System.IO.Path;
using System.Numerics;
using Color = System.Windows.Media.Color;
using AudioSwitcher.AudioApi.CoreAudio;
namespace DoAn_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {
        private string[] paths;
        private bool _playing = false;
        private bool _open = false;
        private bool _isEmpty = false;
        private int _shuffle = 1;
        private int _history = 1;
        ObservableCollection<string> historyMusic = new ObservableCollection<string>();
        DispatcherTimer _timer;
        public static BindingList<object>  listMusic = new BindingList<object>();
        public MainWindow()
        {
            InitializeComponent();
        }
        private bool IsEmptyList()
        {
            var count = ListViewVideo.Items.Count;
            if (count != 0) return false;
            else return true;         
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ControlDisable();
            ButtonSave.IsEnabled = false;
        }       
        public void ControlDisable()
        {
            ButtonPlay.IsEnabled = false;
            ButtonNext.IsEnabled = false;
            ButtonPrev.IsEnabled = false;
            Next_Skip.IsEnabled = false;
            Prev_Skip.IsEnabled = false;
        }
        public void ControlEnable()
        {
            ButtonPlay.IsEnabled = true;
            ButtonNext.IsEnabled = true;
            ButtonPrev.IsEnabled = true;
            Next_Skip.IsEnabled = true;
            Prev_Skip.IsEnabled = true;
        }
        public Double Duration(String file)
        {
            WindowsMediaPlayer wmp = new WindowsMediaPlayer();
            IWMPMedia mediainfo = wmp.newMedia(file);
            return mediainfo.duration;
        }       
        private static string GetThisFilePath([CallerFilePath] string path = null)
        {
            return path;
        }      
        private static BindingList<object> listmusic = new BindingList<object>();
        private void player_MediaOpened(object sender, RoutedEventArgs e)
        {
            int hours1 = player.NaturalDuration.TimeSpan.Hours;
            int minutes2 = player.NaturalDuration.TimeSpan.Minutes;
            int seconds3 = player.NaturalDuration.TimeSpan.Seconds;
            totalPos.Text = $"{hours1}:{minutes2}:{seconds3}";
            progressSlider.Maximum = player.NaturalDuration.TimeSpan.TotalSeconds +0.3;           
        }
        private void _timer_Tick(object? sender, EventArgs e)
        {
            int hours = player.Position.Hours;
            int minutes = player.Position.Minutes;
            int seconds = player.Position.Seconds;
            currentPos.Text = $"{hours}:{minutes}:{seconds}";
            progressSlider.Value = player.Position.TotalSeconds;         
        }
        private void progressSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double value = progressSlider.Value;
            TimeSpan newPos = TimeSpan.FromSeconds(value);
            player.Position = newPos;
        }
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Video files |*.wmv; *.3g2; *.3gp; *.3gp2; *.3gpp; *.amv; *.asf;  *.avi; *.bin; *.cue; *.divx; *.dv; *.flv; *.gxf; *.iso; *.m1v; *.m2v; *.m2t; *.m2ts; *.m4v; " +
                          " *.mkv; *.mov; *.mp2; *.mp2v; *.mp4; *mp3*; *.mp4v; *.mpa; *.mpe; *.mpeg; *.mpeg1; *.mpeg2; *.mpeg4; *.mpg; *.mpv2; *.mts; *.nsv; *.nuv; *.ogg; *.ogm; *.ogv; *.ogx; *.ps; *.rec; *.rm; *.rmvb; *.tod; *.ts; *.tts; *.vob; *.vro; *.webm; *.dat; "; 
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (var filename in openFileDialog.FileNames)
                {
                    // Get preview media
                    MediaPlayer mediaPlayer = new MediaPlayer();
                    mediaPlayer.ScrubbingEnabled = true;
                    mediaPlayer.Open(new Uri(filename));
                    mediaPlayer.Position = TimeSpan.FromSeconds(5);
                    Thread.Sleep(4000);
                    DrawingVisual drawingVisual = new DrawingVisual();
                    DrawingContext drawingContext = drawingVisual.RenderOpen();
                    drawingContext.DrawVideo(mediaPlayer, new Rect(0, 0, 160, 100));
                    drawingContext.Close();
                    double dpiX = 2 / 200;
                    double dpiY = 2 / 200;
                    RenderTargetBitmap bmp = new RenderTargetBitmap(160, 100, dpiX, dpiY, PixelFormats.Pbgra32);
                    bmp.Render(drawingVisual);
                    // Get preview media
                    //Get name
                    var info = new FileInfo(filename);
                    var shortname = info.Name;
                    var name = shortname.Substring(0, shortname.Length - 4);
                    //Get name
                    // Get total time
                    double duration = Duration(filename);
                    TimeSpan time = TimeSpan.FromSeconds(duration);
                    string str = time.ToString(@"hh\:mm\:ss");
                    //Get total time
                    var paths = filename;   //FilePath
                    var data = new
                    {
                        Image_Video = bmp,
                        MusicName = name,
                        Time = str,
                        FilePath = paths,
                    };
                    listmusic.Add(data);
                    ListViewVideo.Items.Add(data);
                    ButtonLoad.IsEnabled = false;
                }
            }
            else
            {
                ButtonLoad.IsEnabled = true;
            }
            ControlEnable();
            ButtonSave.IsEnabled = true;  
        }
        private void btn_remove_Click(object sender, RoutedEventArgs e)
        {
            if(!IsEmptyList())
            {
                var i = ListViewVideo.SelectedIndex;
                if(i != -1)
                {
                    ListViewVideo.Items.RemoveAt(i);
                    ListViewVideo.SelectedIndex = 0;
                }
                else
                {
                    ListViewVideo.SelectedIndex = 0;
                }
            }
            else
            {
                ControlDisable();
                AutoClosingMessageBox.Show("Danh sách phát đang trống", "Media Player", 1000);              
            }
        }
        private void ButtonPlay_Click(object sender, RoutedEventArgs e)
        {
            if (IsEmptyList())
            {
                AutoClosingMessageBox.Show("PlayList trống không thể phát !", "Media Player", 1000);
                ControlDisable();
            }
            else
            {
                if(ListViewVideo.SelectedIndex != -1)
                {
                    ButtonPlay.IsEnabled = true;
                    if (_playing)
                    {
                        player.Pause();
                        _playing = false;
                        _timer.Stop();  
                    }
                    else
                    {
                        _playing = true;
                        player.Play();
                        _timer.Start();
                        var select = ListViewVideo.SelectedItem;
                        var arrSelect = select.ToString();
                        historyMusic.Add(arrSelect);
                    }
                }               
            }
        }
        private void SourceVideo ()
        {
            var select = ListViewVideo.SelectedItem;
            string[] arrSelect = select.ToString().Split("FilePath = ");
            int length = arrSelect[1].Length;
            string src = arrSelect[1].Substring(0,length - 2 );
            player.Source = new Uri(src, UriKind.Absolute);
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            _timer.Start();
            _timer.Tick += _timer_Tick;
        }
        private void ListViewVideo_MouseUp(object sender, MouseButtonEventArgs e)
        {
            int count = ListViewVideo.Items.Count;           
            if ( count != 0)
            {
                player.Pause();
                _playing = false;
                SourceVideo();
            }
        }
        private void Shuffle_Click(object sender, RoutedEventArgs e)
        {
            _shuffle *= -1;
            if(_shuffle == -1)
            {
                Shuffle.Background = Brushes.Aqua;
            }
            else
            {
                Shuffle.Background = Brushes.Transparent;
            }
        }
        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            int count = ListViewVideo.Items.Count;
            if(!IsEmptyList())
            {
                Random rd = new Random();
                int num = rd.Next(0, count);
                if (_shuffle == -1)
                {
                    ListViewVideo.SelectedIndex = num;
                }
                else
                {
                    ListViewVideo.SelectedIndex += 1;
                }
                player.Pause();
                _playing = false;
                SourceVideo();
                ButtonPrev.IsEnabled = true;
            }          
        }
        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            int count = ListViewVideo.Items.Count;
            if (!IsEmptyList())
            {
                Random rd = new Random();
                int num = rd.Next(0, count);
                if (_shuffle == -1)
                {
                    ListViewVideo.SelectedIndex = num;
                }
                else
                {
                    if (ListViewVideo.SelectedIndex >= 1)
                    {
                        ListViewVideo.SelectedIndex -= 1;
                    }
                    else
                    {
                        ListViewVideo.SelectedIndex = 0;
                    }
                }
                player.Pause();
                _playing = false;
                SourceVideo();
                ButtonNext.IsEnabled = true;
            }
        }
        private void Next_Skip_Click(object sender, RoutedEventArgs e)
        {
            if (!IsEmptyList())
            {
                progressSlider.Value += 10;
            }
            
        }
        private void Prev_Skip_Click(object sender, RoutedEventArgs e)
        {
            if(progressSlider.Value >= 10 && !IsEmptyList())
            {
                progressSlider.Value -= 10;
            }
        }
        private void Volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = (int)Volume.Value;
            CoreAudioDevice defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;
            defaultPlaybackDevice.Volume = value;           
            VolumeLabel.Text = value.ToString() + " %";
        }
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            ButtonLoad.IsEnabled = true;
            if (_open == false)
            {
                if (!IsEmptyList())
                {
                    var path = GetThisFilePath();
                    var directory = Path.GetDirectoryName(path);
                    var newpath = directory + @"\default.txt";
                    StreamWriter save = new StreamWriter(newpath);
                    foreach (var item in ListViewVideo.Items)
                    {
                        save.WriteLine(item.ToString());
                    }
                    save.Close();
                    ListViewVideo.Items.Clear();
                    listMusic.Clear();
                    AutoClosingMessageBox.Show("Saved", "Media Player", 1000);
                    ControlDisable();
                }
                else
                {
                    AutoClosingMessageBox.Show("Danh sách không thể trống ! \nVui lòng thêm 1 bài hát để lưu", "Media Player", 1000);
                    ButtonSave.IsEnabled = false;
                }
            }
            else
            {
                if (!IsEmptyList())
                {
                    StreamWriter save = new StreamWriter(playlistPath.Text);
                    foreach (var item in ListViewVideo.Items)
                    {
                        save.WriteLine(item.ToString());
                    }
                    save.Close();
                    ListViewVideo.Items.Clear();
                    listMusic.Clear();
                    playlistName.Text = "default.txt";
                    AutoClosingMessageBox.Show("Saved", "Media Player", 1000);
                    ControlDisable();
                }
                else
                {
                    AutoClosingMessageBox.Show("Danh sách không thể trống ! \nVui lòng thêm 1 bài hát để lưu", "Media Player", 2000);
                    ButtonSave.IsEnabled = false;
                }
            }
        }
        private void ButtonLoad_Click(object sender, RoutedEventArgs e)
        {
            ButtonSave.IsEnabled = true;
            var screen = new OpenFileDialog();
            var path = GetThisFilePath();
            var directory = Path.GetDirectoryName(path);
            screen.InitialDirectory = directory;
            screen.Filter = "Text files (*.txt)|*.txt";
            if (screen.ShowDialog() == true)
            {
                ListViewVideo.Items.Clear();
                playlistName.Text = screen.SafeFileName;
                playlistPath.Text = screen.FileName;
                _open = true;
                using StreamReader reader = new StreamReader(screen.FileName);
                var file = File.ReadLines(screen.FileName).ToArray();
                for (int i = 0; i < file.Length; i++)
                {
                    string[] _name = file[i].ToString().Split("MusicName = ");
                    string[] _name2 = _name[1].ToString().Split(", Time");
                    string name = _name2[0];// MusicName

                    string[] _time = file[i].ToString().Split("Time = ");
                    string time = _time[1].Substring(0, 8);// Time

                    string[] _src = file[i].ToString().Split("FilePath = ");
                    int length = _src[1].Length;
                    string src = _src[1].Substring(0, length-2);
                    MediaPlayer mediaPlayer = new MediaPlayer();
                    mediaPlayer.ScrubbingEnabled = true;
                    mediaPlayer.Open(new Uri(src));
                    mediaPlayer.Position = TimeSpan.FromSeconds(5);
                    Thread.Sleep(4000);
                    DrawingVisual drawingVisual = new DrawingVisual();
                    DrawingContext drawingContext = drawingVisual.RenderOpen();
                    drawingContext.DrawVideo(mediaPlayer, new Rect(0, 0, 160, 100));
                    drawingContext.Close();
                    double dpiX = 2 / 200;
                    double dpiY = 2 / 200;
                    RenderTargetBitmap bmp = new RenderTargetBitmap(160, 100, dpiX, dpiY, PixelFormats.Pbgra32);
                    bmp.Render(drawingVisual);
                    var data = new
                    {
                        Image_Video = bmp,
                        MusicName = name,
                        Time = time,
                        FilePath = src,
                    };
                    listmusic.Add(data);
                    ListViewVideo.Items.Add(data);
                }
                if(IsEmptyList())
                {
                    ControlDisable();
                }
                else
                {
                    ControlEnable();
                    ButtonLoad.IsEnabled = false;
                }                
            }
            else
            {
                ButtonLoad.IsEnabled = true;
                ButtonSave.IsEnabled = false;
                ControlDisable();
            }
            
        }
        private void CreateListBtn_Click(object sender, RoutedEventArgs e)
        {
            var screen = new SaveWindow();
            screen.Show();
            ButtonLoad.IsEnabled = true;
        }
        private void player_MediaEnded(object sender, RoutedEventArgs e)
        {
            int count = ListViewVideo.Items.Count;
            if (count > 1)
            {
                AutoClosingMessageBox.Show("Đang chuyển sang bài tiếp theo", "Media Player", 1000);
                if (progressSlider.Value == player.NaturalDuration.TimeSpan.TotalSeconds)
                {
                    if (count != 0)
                    {
                        Random rd = new Random();
                        int num = rd.Next(0, count);
                        if (_shuffle == -1)
                        {
                            ListViewVideo.SelectedIndex = num;
                        }
                        else
                        {
                            ListViewVideo.SelectedIndex += 1;
                        }
                        _playing = false;
                        player.Pause();
                        SourceVideo();
                    }
                }
            }
        }
        private void HisroryBtn_Click(object sender, RoutedEventArgs e)
        {
            _history *= -1;                    
            if (_history == -1)
            {
                ListViewVideo.Visibility = Visibility.Hidden;
                HistoryList.Visibility = Visibility.Visible;
                HistoryList.Items.Clear();
                var uniqueArr = historyMusic.Distinct().ToArray();
                for (int i = 0; i < uniqueArr.Length; i++)
                {
                    string[] _name = uniqueArr[i].ToString().Split("MusicName = ");
                    string[] _name2 = _name[1].ToString().Split(", Time");
                    string name = _name2[0];// MusicName

                    string[] _time = uniqueArr[i].ToString().Split("Time = ");
                    string time = _time[1].Substring(0, 8);// Time

                    string[] _src = uniqueArr[i].ToString().Split("FilePath = ");
                    int length = _src[1].Length;
                    string src = _src[1].Substring(0, length - 2);

                    MediaPlayer mediaPlayer = new MediaPlayer();
                    mediaPlayer.ScrubbingEnabled = true;
                    mediaPlayer.Open(new Uri(src));
                    mediaPlayer.Position = TimeSpan.FromSeconds(5);
                    Thread.Sleep(4000);
                    DrawingVisual drawingVisual = new DrawingVisual();
                    DrawingContext drawingContext = drawingVisual.RenderOpen();
                    drawingContext.DrawVideo(mediaPlayer, new Rect(0, 0, 160, 100));
                    drawingContext.Close();
                    double dpiX = 2 / 200;
                    double dpiY = 2 / 200;
                    RenderTargetBitmap bmp = new RenderTargetBitmap(160, 100, dpiX, dpiY, PixelFormats.Pbgra32);
                    bmp.Render(drawingVisual);
                    var data = new
                    {
                        Image_Video = bmp,
                        MusicName = name,
                        Time = time,
                        FilePath = src,
                    };
                    listmusic.Add(data);
                    HistoryList.Items.Add(data);
                }
                ButtonSave.IsEnabled = false;
                ButtonLoad.IsEnabled = false;
                btn_remove.IsEnabled = false;
                btn_add.IsEnabled = false;
                ControlDisable();
            }                                               
            else
            {
                ListViewVideo.Visibility = Visibility.Visible;
                HistoryList.Visibility = Visibility.Hidden;
                ButtonSave.IsEnabled = true;
                ButtonLoad.IsEnabled = true;
                btn_remove.IsEnabled = true;
                btn_add.IsEnabled = true;
                ControlEnable();
            }
        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.A:
                    ButtonPlay_Click(sender, e);   //Play hoặc Pause video = phím A
                    break;
                case Key.S:
                    ButtonSave_Click(sender, e);   //Lưu và load Playlist = phím S
                    break;
                case Key.H:
                    HisroryBtn_Click(sender, e);   //Lịch sử tệp đã xem gần đây = phím H
                    break;
                case Key.N:
                    ButtonNext_Click(sender, e);   //Phát bài tiếp theo trong danh sách = phím N
                    break;
                case Key.P:
                    ButtonPrev_Click(sender, e);   //Phát bài trước đó trong danh sách = phím P
                    break;
                case Key.Right:
                    Next_Skip_Click(sender, e);    //Phát tiếp theo 10s = mũi tên phải
                    break;
                case Key.Left:
                    Prev_Skip_Click(sender, e);   //Phát lại 10s = mũi tên trái
                    break;
            }
        }
    }
}
