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
using System.Windows.Shapes;
using System.Globalization;
namespace AtomMediaPlayer
{
    /// <summary>
    /// Interaction logic for Loop.xaml
    /// </summary>
    public partial class Loop : Window
    {
        public TimeSpan from, to;
        public Loop()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txtFrom.Text) || string.IsNullOrEmpty(txtFrom.Text))
            {
                MessageBox.Show("One or more field are empty!");
            }
            else
            {
                if (!TimeSpan.TryParse(txtFrom.Text, out from))
                {
                    MessageBox.Show("Please input a valid format!");
                    txtFrom.Focus();
                    return;
                }
                if (!TimeSpan.TryParse(txtTo.Text, out to))
                {
                    MessageBox.Show("Please input a valid format!");
                    txtTo.Focus();
                    return;
                }
                this.DialogResult = true;
            }
        }
    }
}
