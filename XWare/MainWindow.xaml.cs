using System;
using System.Collections.Generic;
using System.IO;
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
        static readonly string DocPath = SavePath + "/NCX-Core/NCXNewsPlus/data/";

        public class News
        {
            public string Edition { get; set; }
            public string Date { get; set; }
            public string Content { get; set; }
            public string Line1 { get; set; }
            public string Line2 { get; set; }
            public string Line3 { get; set; }
            public string Line4 { get; set; }
            public string Line5 { get; set; }
            public string Line6 { get; set; }
            public string Line7 { get; set; }
            public string Line8 { get; set; }
            public string Line9 { get; set; }
            public string Line10 { get; set; }
            public string Line11 { get; set; }
            public string Line12 { get; set; }
            public string Line13 { get; set; }
            public string Line14 { get; set; }
            public string Line15 { get; set; }
            public string Line16 { get; set; }
        }

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
                    new System.Uri("https://github.com/NinjaCheetah/NCX-Installer-News/releases/latest/download/newsFull.json"),
                    // Param2 = Path to save
                    System.IO.Path.Combine(DocPath, "newsFull.json")
                );
            }
        }

        public void DownloadCompleted(object sender, EventArgs e)
        {
            string json = System.IO.File.ReadAllText(System.IO.Path.Combine(DocPath, "newsFull.json"));
            News news = JsonConvert.DeserializeObject<News>(json);

            label2.Content = (news.Edition);
            label3.Content = (news.Date);
            // Start with news content below
            line1.Content = (news.Line1);
            line2.Content = (news.Line2);
            line3.Content = (news.Line3);
            line4.Content = (news.Line4);
            line5.Content = (news.Line5);
            line6.Content = (news.Line6);
            line7.Content = (news.Line7);
            line8.Content = (news.Line8);
            line9.Content = (news.Line9);
            line10.Content = (news.Line10);
            line11.Content = (news.Line11);
            line12.Content = (news.Line12);
            line13.Content = (news.Line13);
            line14.Content = (news.Line14);
            line15.Content = (news.Line15);
            line16.Content = (news.Line16);

        }
    }
}
