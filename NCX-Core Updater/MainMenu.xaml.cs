using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NCX_Core_Updater
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public string updateNum;
        public string nupdateNum;
        public string releaseNum;
        public string nightlyNum;
        static readonly string SavePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        static readonly string ProgFilesPath = "C:/Program Files/NCX/NCX-Core/";

        public MainMenu()
        {
            InitializeComponent();
            // Read latest release and set to the latest release label
            string text = File.ReadAllText(System.IO.Path.Combine(SavePath, "updateNotice.txt"));
            releaseNum = Convert.ToString(text);
            label1.Content = "Latest Release: " + releaseNum;
            // Read latest nightly and set to the latest nightly label
            TextReader tr = new StreamReader(System.IO.Path.Combine(SavePath, "nightlyNotice.txt"));
            string nnumString = tr.ReadLine();
            nightlyNum = Convert.ToString(nnumString);
            tr.Close();
            label2.Content = "Latest Nightly: " + nightlyNum;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            About win = new About();
            win.Show();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            label3.Content = "Fetching release data...";
            string text = File.ReadAllText(System.IO.Path.Combine(SavePath, "version.txt"));
            updateNum = Convert.ToString(text);
            if (updateNum == releaseNum)
            {
                string message = "NCX-Core is up to date! (You're running NCX-Core v" + updateNum + ")";
                string caption = "Up To Date";
                MessageBoxButton buttons = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Information;
                if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.OK)
                {
                    
                }
            }
            else
            {
                string message = "There is an update to NCX-Core available: v" + releaseNum + ". You are currently running v" + updateNum + ". Would you like to update?";
                string caption = "Update Available";
                MessageBoxButton buttons = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Information;
                if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.Yes)
                {
                    updateRelease();
                }
                else
                {

                }
            }
        }

        public void updateRelease()
        {
            label3.Visibility = Visibility.Visible;
            progressBar1.Visibility = Visibility.Visible;
            label3.Content = "Downloading latest release...";
            using (WebClient wc = new WebClient())
            {
                wc.DownloadFileCompleted += DownloadCompleted;
                wc.DownloadFileAsync(
                    // Param1 = Link of file
                    new System.Uri("https://github.com/NinjaCheetah/NCX-Core/releases/latest/download/NCXCore-Setup.msi"),
                    // Param2 = Path to save
                    System.IO.Path.Combine(SavePath, "NCXCore-Setup.msi")
                );
            }
        }

        public void DownloadCompleted(object sender, EventArgs e)
        {
            label3.Content = "Download Complete";
            Directory.SetCurrentDirectory(SavePath);
            Process p = new Process();
            p.StartInfo.FileName = "msiexec";
            p.StartInfo.Arguments = "/i NCXCore-Setup.msi";
            p.Start();
            label3.Content = "Installing...";
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            label3.Content = "Fetching nightly data...";
            string text = File.ReadAllText(System.IO.Path.Combine(SavePath, "version.txt"));
            nupdateNum = Convert.ToString(text);
            if (nupdateNum == nightlyNum)
            {
                string message = "You're running the latest NCX-Core Nightly! (Running NCX-Core v" + nupdateNum + ")";
                string caption = "Up To Date";
                MessageBoxButton buttons = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Information;
                if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.OK)
                {

                }
            }
            else
            {
                string message = "There is a new NCX-Core Nightly available: v" + nightlyNum + ". You are currently running v" + nupdateNum + ". Would you like to update?";
                string caption = "Nightly Available";
                MessageBoxButton buttons = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Information;
                if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.Yes)
                {
                    updateNightly();
                }
                else
                {

                }
            }
        }

        public void updateNightly()
        {
            label3.Visibility = Visibility.Visible;
            progressBar1.Visibility = Visibility.Visible;
            label3.Content = "Downloading latest nightly...";
            using (WebClient wc = new WebClient())
            {
                wc.DownloadFileCompleted += DownloadCompleted2;
                wc.DownloadFileAsync(
                    // Param1 = Link of file
                    new System.Uri("https://github.com/NinjaCheetah/NCX-Core/raw/master/Beta/NCXCore.zip"),
                    // Param2 = Path to save
                    System.IO.Path.Combine(ProgFilesPath, "NCXCore.zip")
                );
            }
        }

        public void DownloadCompleted2(object sender, EventArgs e)
        {
            label3.Content = "Download Complete";
            ZipFile.ExtractToDirectory(System.IO.Path.Combine(ProgFilesPath, "NCXCore.zip"), ProgFilesPath, true);
            File.Delete(System.IO.Path.Combine(ProgFilesPath, "NCXCore.zip"));
            label3.Content = "Installation Complete";
        }
    }
}
