using Kirin_2.ViewModel;
using Syncfusion.UI.Xaml.CellGrid.Helpers;
using Syncfusion.UI.Xaml.Spreadsheet.Helpers;
using Syncfusion.Windows.PdfViewer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Kirin_2.UserControls
{
    /// <summary>
    /// Interaction logic for UC_PdfView.xaml
    /// </summary>

    public partial class UC_PdfView : UserControl
    {
        public UC_PdfView(string filePath)
        {
            InitializeComponent();
            PdfViewer viewer = new PdfViewer(filePath);
            this.DataContext = viewer;

            // Hides the bookmark icon. 
            pdfViewer.IsBookmarkEnabled = false;

            // Hides the layer icon. 
            pdfViewer.EnableLayers = false;

            // Hides the organize page icon. 
            pdfViewer.PageOrganizerSettings.IsIconVisible = false;

            // Hides the redaction icon. 
            pdfViewer.EnableRedactionTool = false;

            // Hides the form icon. 
            pdfViewer.FormSettings.IsIconVisible = false;
        }

        private void HideThumbnailTool()
        {
            //Get the instance of the left pane using its template name 
            OutlinePane outlinePane = pdfViewer.Template.FindName("PART_OutlinePane", pdfViewer) as OutlinePane;

            //Get the instance of the thumbnail button using its template name 
            ToggleButton thumbnailButton = (ToggleButton)outlinePane.Template.FindName("PART_ThumbnailButton", outlinePane);
            thumbnailButton.Visibility = Visibility.Collapsed;

            //Get the instance of the toolbar using its template name.
            DocumentToolbar toolbar = pdfViewer.Template.FindName("PART_Toolbar", pdfViewer) as DocumentToolbar;

            //Get the instance of the sticky notes Thumbnails
            ToggleButton notesButton = (ToggleButton)toolbar.Template.FindName("PART_StickyNote", toolbar);
            notesButton.Visibility = Visibility.Collapsed;

            //Get the instance of the Ink Tool
            ToggleButton inkButton = (ToggleButton)toolbar.Template.FindName("PART_Ink", toolbar);
            inkButton.Visibility = Visibility.Collapsed;

            //Get the instance of the Ink Eraser Tool
            ToggleButton inkEraserButton = (ToggleButton)toolbar.Template.FindName("PART_InkEraser", toolbar);
            inkEraserButton.Visibility = Visibility.Collapsed;

            //Get the instance of the highlightButton Tool
            ToggleButton highlightButton = (ToggleButton)toolbar.Template.FindName("PART_Highlight", toolbar);
            highlightButton.Visibility = Visibility.Collapsed;

            //Get the instance of the underline Tool
            ToggleButton underlineButton = (ToggleButton)toolbar.Template.FindName("PART_Underline", toolbar);
            underlineButton.Visibility = Visibility.Collapsed;

            //Get the instance of the Strikethrough Tool
            ToggleButton strikeButton = (ToggleButton)toolbar.Template.FindName("PART_Strikethrough", toolbar);
            strikeButton.Visibility = Visibility.Collapsed;

            //Get the instance of the Shape Tool
            ToggleButton shapeButton = (ToggleButton)toolbar.Template.FindName("PART_Shapes", toolbar);
            shapeButton.Visibility = Visibility.Collapsed;

            //Get the instance of the Stamp Tool
            ToggleButton stampButton = (ToggleButton)toolbar.Template.FindName("PART_Stamp", toolbar);
            stampButton.Visibility = Visibility.Collapsed;

            //Get the instance of the Signature Tool
            Button signButton = (Button)toolbar.Template.FindName("PART_ButtonSignature", toolbar);
            signButton.Visibility = Visibility.Collapsed;

            //Get the instance of the Select Tool
            ToggleButton selectButton = (ToggleButton)toolbar.Template.FindName("PART_SelectTool", toolbar);
            selectButton.Visibility = Visibility.Collapsed;

            //Get the instance of the Hand Tool
            ToggleButton handButton = (ToggleButton)toolbar.Template.FindName("PART_HandTool", toolbar);
            handButton.Visibility = Visibility.Collapsed;

            //Get the instance of the Hand Tool
            ToggleButton marqueeButton = (ToggleButton)toolbar.Template.FindName("PART_MarqueeZoom", toolbar);
            marqueeButton.Visibility = Visibility.Collapsed;
        }

        #region Handlers
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HideThumbnailTool();
        }
        #endregion
    }
}

