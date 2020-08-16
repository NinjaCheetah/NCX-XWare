using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using Newtonsoft.Json;

namespace XWare
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static readonly string SavePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public class News
        {
            public string Edition { get; set; }
            public string Date { get; set; }
            public string Content { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();
            using (WebClient wc = new WebClient())
            {
                wc.DownloadFileCompleted += DownloadCompleted;
                wc.DownloadFileAsync(
                    // Param1 = Link of file
                    new System.Uri("https://github.com/NinjaCheetah/NCX-Installer-News/releases/latest/download/newsFull.json"),
                    // Param2 = Path to save
                    System.IO.Path.Combine(SavePath, "newsFull.json")
                );
            }
        }

        public void DownloadCompleted(object sender, EventArgs e)
        {
            string json = System.IO.File.ReadAllText(System.IO.Path.Combine(SavePath, "newsFull.json"));
            News news = JsonConvert.DeserializeObject<News>(json);

            label2.Content = (news.Edition);
            label3.Content = (news.Date);
            textblock.Text = (news.Content);
        }
    }
}
