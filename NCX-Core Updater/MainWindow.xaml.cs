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
using System.Threading;
using System.Net;
using System.IO;

namespace NCX_Core_Updater
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static readonly string SavePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        static readonly string DocPath = SavePath + "/NCX-Core/NCXCoreUpdater/data/";

        public MainWindow()
        {
            InitializeComponent();
            if (!Directory.Exists(DocPath))
            {
                Directory.CreateDirectory(DocPath);
            }
            using (WebClient wc = new WebClient())
            {
                wc.DownloadFileCompleted += DownloadCompleted;
                wc.DownloadFileAsync(
                    // Param1 = Link of file
                    new System.Uri("https://github.com/NinjaCheetah/NCX-Installer-News/releases/latest/download/nightlyNotice.txt"),
                    // Param2 = Path to save
                    System.IO.Path.Combine(DocPath, "nightlyNotice.txt")
                );
            }
        }

        public void DownloadCompleted(object sender, EventArgs e)
        {
            using (WebClient wc = new WebClient())
            {
                wc.DownloadFileCompleted += DownloadCompleted2;
                wc.DownloadFileAsync(
                    // Param1 = Link of file
                    new System.Uri("https://github.com/NinjaCheetah/NCX-Installer-News/releases/latest/download/updateNotice.txt"),
                    // Param2 = Path to save
                    System.IO.Path.Combine(DocPath, "updateNotice.txt")
                );
            }
        }

        public void DownloadCompleted2(object sender, EventArgs e)
        {
            _NavigationFrame.Visibility = Visibility.Visible;
            _NavigationFrame.Navigate(new MainMenu());
        }
    }
}
