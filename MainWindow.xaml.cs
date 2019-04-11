using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace AtomMediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool mediaPlayerIsPlaying = false;
        private bool userIsDraggingSlider = false;

        private bool fullscreen = false;
        private DispatcherTimer DoubleClickTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

            DoubleClickTimer.Interval = TimeSpan.FromMilliseconds(GetDoubleClickTime());
            DoubleClickTimer.Tick += (s, e) => DoubleClickTimer.Stop();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if((AtomPlayer.Source != null) && (AtomPlayer.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider)){
                sliProgress.Minimum = 0;
                sliProgress.Maximum = AtomPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                sliProgress.Value = AtomPlayer.Position.TotalSeconds;
            }
        }

        private void BrowseButtonClick(object sender, RoutedEventArgs e)

        {

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Media Files (*.wmv;*.mp4;*.mp3;*.wav;*.avi;*.mkv)|*.wmv;*.mp4;*.mp3;*.wav;*.avi;*.mkv|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);


            if (dlg.ShowDialog() == true)
            {

                string selectedFileName = dlg.FileName;
                AtomPlayer.Source = new Uri(selectedFileName);
                AtomPlayer.Play();
                this.Title = Path.GetFileNameWithoutExtension(selectedFileName);
            }

        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            AtomPlayer.Play();
            mediaPlayerIsPlaying = true;
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            AtomPlayer.Pause();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            AtomPlayer.Stop();
            mediaPlayerIsPlaying = false;
        }

        private void sliProgress_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
        }

        private void sliProgress_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            AtomPlayer.Position = TimeSpan.FromSeconds(sliProgress.Value);
        }

        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            AtomPlayer.Volume += (e.Delta > 0) ? 0.1 : -0.1;
        }

        private void AtomPlayer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!DoubleClickTimer.IsEnabled)
            {
                DoubleClickTimer.Start();
            }
            else
            {
                if (!fullscreen)
                {
                    this.WindowStyle = WindowStyle.None;
                    this.WindowState = WindowState.Maximized;
                    Grid.SetRowSpan(AtomPlayer, 3);
                    AtomPlayer.Margin = new Thickness(0, 0, 0, 0);
                    AtomPlayer.Stretch = Stretch.Fill;


                    Panel.SetZIndex(statusBar, 3);
                    Panel.SetZIndex(btnPlay, 3);
                    Panel.SetZIndex(btnStop, 3);
                    Panel.SetZIndex(btnPause, 3);
                    Panel.SetZIndex(gridButton, 3);
                    Panel.SetZIndex(gridStatusBar, 3);

                    statusBar.Opacity = 0;

                    btnPlay.Opacity = 0;
                    btnPause.Opacity = 0;
                    btnStop.Opacity = 0;

                }
                else
                {
                    this.WindowStyle = WindowStyle.SingleBorderWindow;
                    this.WindowState = WindowState.Normal;
                    Grid.SetRowSpan(AtomPlayer, 2);
                    AtomPlayer.Margin = new Thickness(0, 20, 0, 63);
                    AtomPlayer.Stretch = Stretch.UniformToFill;

                    Panel.SetZIndex(statusBar, 1);
                    Panel.SetZIndex(btnPlay, 1);
                    Panel.SetZIndex(btnStop, 1);
                    Panel.SetZIndex(btnPause, 1);
                    Panel.SetZIndex(gridButton, 1);
                    Panel.SetZIndex(gridStatusBar, 1);


                    statusBar.Opacity = 1;

                    btnPlay.Opacity = 1;
                    btnPause.Opacity = 1;
                    btnStop.Opacity = 1;

                }

                fullscreen = !fullscreen;
            }
        }

        private void ChangeMediaSpeedRatio(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            AtomPlayer.SpeedRatio = (double)speedRatioSlider.Value;
        }

        private void mediaElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)

        {
            MessageBox.Show(e.ErrorException.Message);

        }

        [DllImport("user32.dll")]
        private static extern uint GetDoubleClickTime();

        private void StatusBar_MouseEnter(object sender, MouseEventArgs e)
        {
            if (fullscreen)
            {
                statusBar.Opacity = 1;
                statusBar.IsEnabled = true;
            }

        }

        private void statusBar_MouseLeave(object sender, MouseEventArgs e)
        {
            if (fullscreen)
            {
                statusBar.Opacity = 0;
                statusBar.IsEnabled = false;
            }
        }

        private void btn_MouseEnter(object sender, MouseEventArgs e)
        {
            if (fullscreen)
            {
                btnPlay.Opacity = 1;
                btnPause.Opacity = 1;
                btnStop.Opacity = 1;

                btnPause.IsEnabled = true;
                btnPlay.IsEnabled = true;
                btnStop.IsEnabled = true;
            }

        }

        private void btn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (fullscreen)
            {
                btnPlay.Opacity = 0;
                btnPause.Opacity = 0;
                btnStop.Opacity = 0;

                btnPause.IsEnabled = false;
                btnPlay.IsEnabled = false;
                btnStop.IsEnabled = false;
            }
        }
    }
}
