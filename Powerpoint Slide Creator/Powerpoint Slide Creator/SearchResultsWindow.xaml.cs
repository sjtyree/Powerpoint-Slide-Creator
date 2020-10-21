using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Powerpoint_Slide_Creator
{
    /// <summary>
    /// Interaction logic for SearchResultsWindow.xaml
    /// </summary>
    public partial class SearchResultsWindow : Window
    {
        public SearchResultsWindow()
        {
            InitializeComponent();
        }

        public SearchResultsWindow(string TitleText, string SlideText)
        {
            InitializeComponent();
            ArrayList urls = new ArrayList();
            getImageURLs(urls, TitleText, SlideText);
            addImagesToListBox(urls);

        }

        private void getImageURLs(ArrayList urls, string titleText, string slideText)
        {
            //HttpClient testClient = new HttpClient();
            //create web client
            WebClient googleImages = new WebClient();
            Regex googleRegex = new Regex(@"src=""https://[^""]*""", RegexOptions.Compiled | RegexOptions.IgnoreCase);//\\""[^""]*""");
            //get google html for image search
            string html = googleImages.DownloadString("https://www.google.com/search?tbm=isch&q=" + titleText + "+" + slideText);
            //MessageBox.Show(html);
            MatchCollection googleMatches = googleRegex.Matches(html);
            
            //MessageBox.Show("" + googleMatches.Count);
            foreach (Match m in googleMatches)
            {
                //add the match to the arraylist
                urls.Add(m.Value);
            }

        }

        private void addImagesToListBox(ArrayList urls)
        {
            int iterator;
            foreach (string x in urls) //(int x = 0; x < urls.Count; x++)
            {
                iterator = 1;
                Console.WriteLine(x);
                Console.WriteLine(x.Substring(5, x.Length - 6));

                //create the image and add it to the listbox
                Image googleImage = new Image();
                googleImage.Name = "Image" + iterator;
                googleImage.Source = new BitmapImage(new Uri(x.Substring(5, x.Length - 6)));
                this.Image_List_Box.Items.Add(googleImage);

                //create the corresponding checkbox and add it to the listbox
                CheckBox listCheckBox = new CheckBox();
                listCheckBox.Name = "Checkbox" + iterator;
                this.Image_List_Box.Items.Add(new CheckBox());

                //increment iterator
                iterator++;
            }
            //Image mechonis = new Image();
            //mechonis.Source = new BitmapImage(new Uri(@"C:\Users\Spencer\source\repos\SEH-Interview-Test\Powerpoint Slide Creator\Powerpoint Slide Creator\Test Images\mechonis.jpg")); 
            //this.Image_List_Box.Items.Add(mechonis);
        }
    }
}
