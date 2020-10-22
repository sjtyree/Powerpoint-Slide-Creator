using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using System;
using System.Collections;
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

namespace Powerpoint_Slide_Creator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// 
    /// Powerpoint code taken from the following websites:
    /// https://www.free-power-point-templates.com/articles/how-to-create-a-powerpoint-presentation-using-c-and-embed-a-picture-to-the-slide/
    /// https://www.free-power-point-templates.com/articles/create-powerpoint-ppt-programmatically-using-c/
    /// </summary>
    public partial class MainWindow : Window
    {
        private Microsoft.Office.Interop.PowerPoint.Application pptApplication;
        private Presentation pptPresentation;
        private Microsoft.Office.Interop.PowerPoint.Slides slides;
        private Microsoft.Office.Interop.PowerPoint._Slide slide;
        private Microsoft.Office.Interop.PowerPoint.TextRange objText;
        Microsoft.Office.Interop.PowerPoint.CustomLayout customLayout;
        int slideNumber;

        public MainWindow()
        {
            InitializeComponent();

            //initialize powerpoint
            pptApplication = new Microsoft.Office.Interop.PowerPoint.Application();

            // Create the Presentation File
            pptPresentation = pptApplication.Presentations.Add(MsoTriState.msoTrue);

            customLayout = pptPresentation.SlideMaster.CustomLayouts[Microsoft.Office.Interop.PowerPoint.PpSlideLayout.ppLayoutText];

            slideNumber = 1;
        }

        private void MainWindowButtonClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Documents.TextRange textRange = new System.Windows.Documents.TextRange(this.MainWindowSlideText.Document.ContentStart, this.MainWindowSlideText.Document.ContentEnd);
            //MessageBox.Show(textRange.Text);
            SearchResultsWindow newWindow = new SearchResultsWindow(this.MainWindowTitleText.Text, textRange.Text);
            newWindow.ShowDialog();
            ArrayList selectedUrls = newWindow.getSelectedUrls();
            createSlide(selectedUrls);
        }

        private void createSlide(ArrayList selectedUrls)
        {
            // Create new Slide
            slides = pptPresentation.Slides;
            slide = slides.AddSlide(slideNumber, customLayout);
            //increment slide number
            slideNumber++;
            // Add title
            objText = slide.Shapes[1].TextFrame.TextRange;
            objText.Text = this.MainWindowTitleText.Text;
            objText.Font.Name = "Arial";
            objText.Font.Size = 32;

            System.Windows.Documents.TextRange textRange = new System.Windows.Documents.TextRange(this.MainWindowSlideText.Document.ContentStart, this.MainWindowSlideText.Document.ContentEnd);

            objText = slide.Shapes[2].TextFrame.TextRange;
            objText.Text = textRange.Text;

            Microsoft.Office.Interop.PowerPoint.Shape shape = slide.Shapes[2];
            foreach (string url in selectedUrls)
            {
                slide.Shapes.AddPicture(url, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, shape.Left, shape.Top, shape.Width, shape.Height);
            }
        }


        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            //do anything else that needs to be done before the program ends  
            this.Close();
        }

    }
}
