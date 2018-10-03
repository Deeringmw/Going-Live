using System;
using System.Collections.Generic;
using System.IO;
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
using MahApps.Metro.Controls;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace Going_Live_Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        Settings settings = new Settings();

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(@"settings.json"));
                txtChatPath.Text = settings.ChatPath;
                txtObsPath.Text = settings.ObsPath;
                txtTwitch.Text = settings.TwitchName;
                txtCustomSettings.Text = settings.CustomPath;

            }
            catch { }
        }
        
        


        private void btnPathObs_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".exe";

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                txtObsPath.Text = filename;
            }
        }

        private void btnPathChat_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".exe";

            bool? result = dlg.ShowDialog();


            if (result == true)
            {
                string filename = dlg.FileName;
                txtChatPath.Text = filename;
            }
        }

        private void btnLaunch_Click(object sender, RoutedEventArgs e)
        {
            if (chkObs.IsChecked == true)
            {
                try
                {
                    System.Diagnostics.Process.Start(txtObsPath.Text);
                }
                catch { }
            }
            if (chkChat.IsChecked == true)
            {
                try
                {
                    System.Diagnostics.Process.Start(txtChatPath.Text);
                }
                catch { }
            }
            if (chkOpenTwitch.IsChecked == true)
            {
                System.Diagnostics.Process.Start($"www.twitch.tv/{txtTwitch.Text}");
            }
            if (chkCustom.IsChecked == true)
            {
                System.Diagnostics.Process.Start(txtCustomSettings.Text);
            }
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            settings.TwitchName = txtTwitch.Text;
            settings.ObsPath = txtObsPath.Text;
            settings.ChatPath = txtChatPath.Text;
            settings.CustomPath = txtCustomSettings.Text;

            File.WriteAllText("settings.json", JsonConvert.SerializeObject(settings));
        }

        private void btnCustomSettings_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".exe";

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                txtCustomSettings.Text = filename;
            }
        }

        private void chkCustomSettings_Checked(object sender, RoutedEventArgs e)
        {
            btnCustomSettings.Visibility = Visibility.Visible;
            txtCustomSettings.Visibility = Visibility.Visible;
            chkCustom.Visibility = Visibility.Visible;
        }

        private void chkCustomSettings_Unchecked(object sender, RoutedEventArgs e)
        {
            btnCustomSettings.Visibility = Visibility.Hidden;
            txtCustomSettings.Visibility = Visibility.Hidden;
            chkCustom.Visibility = Visibility.Hidden;
        }


    }
}
