using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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
using System.Windows.Xps;

namespace Kirin_2.Pages
{
    /// <summary>
    /// Interaction logic for PrintWindow.xaml
    /// </summary>
    public partial class PrintWindow : Window
    {
        private FixedDocumentSequence _document;
        public PrintWindow(FixedDocumentSequence document)
        {

            InitializeComponent();
            _document = new FixedDocumentSequence();
            _document = document;
            PreviewD.Document = document;


        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();


            printDialog.PrintQueue = LocalPrintServer.GetDefaultPrintQueue();

            printDialog.PrintTicket = printDialog.PrintQueue.DefaultPrintTicket;
            printDialog.PrintTicket.PageOrientation = PageOrientation.Landscape;

            printDialog.PrintTicket.PageScalingFactor = 90;

            printDialog.PrintTicket.PageMediaSize = new PageMediaSize(PageMediaSizeName.ISOA4);

            //printDialog.PrintableAreaHeight ; //*get

            //printDialog.PrintableAreaWidth;   //get

            //printDialog.PrintDocument.

            printDialog.PrintTicket.PageBorderless = PageBorderless.None;


            if (printDialog.ShowDialog() == true)
            {
                //print directly from the Xps file 

                //printDialog.PrintDocument(_document.DocumentPaginator, "Page ");


                // set the print ticket for the document sequence and write it to the printer.

                _document.PrintTicket = printDialog.PrintTicket;



                //-< send_document_to_printer >-

                XpsDocumentWriter writer = PrintQueue.CreateXpsDocumentWriter(printDialog.PrintQueue);

                writer.WriteAsync(_document, printDialog.PrintTicket);


            }
        }
    }
}
