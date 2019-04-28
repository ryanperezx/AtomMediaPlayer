using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AtomMediaPlayer
{
    /// <summary>
    /// Interaction logic for Help.xaml
    /// </summary>
    public partial class Help : Window
    {
        public Help()
        {
            InitializeComponent();
        }

        private void btnPlay_MouseEnter(object sender, MouseEventArgs e)
        {
            tbHelp.Text = "Play button is used to start or play the media from the current paused time";
        }

        private void btnPause_MouseEnter(object sender, MouseEventArgs e)
        {
            tbHelp.Text = "Pause button is used to pause the current media ";

        }

        private void btnStop_MouseEnter(object sender, MouseEventArgs e)
        {
            tbHelp.Text = "Stop button is used to stop the current media from playing";
        }

        private void sliProgress_MouseEnter(object sender, MouseEventArgs e)
        {
            tbHelp.Text = "Timeline can be used to adjust the current running time of the playing media";
        }

        private void pbVolume_MouseEnter(object sender, MouseEventArgs e)
        {
            tbHelp.Text = "Volume is used to adjust the audio output of the current playing media";

        }

        private void speedRatioSlider_MouseEnter(object sender, MouseEventArgs e)
        {
            tbHelp.Text = "Playback speed is used to adjust the speed of the current playing media.";

        }

        private void lblProgressStatus_MouseEnter(object sender, MouseEventArgs e)
        {
            tbHelp.Text = "Time Marker displays the current running time";
        }
    }
}
