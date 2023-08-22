﻿using Kirin_2.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Documents.Serialization;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace Kirin_2.ViewModel
{
    public class PrintPreview
    {
        public static void Print_WPF_Preview(FrameworkElement wpf_Element)

        {

            //------------< WPF_Print_current_Window >------------
            string sPrintFileName = "print_preview.xps";
            if (File.Exists(sPrintFileName) == true) File.Delete(sPrintFileName);

            //--< create xps document >--

            XpsDocument doc = new XpsDocument("print_preview.xps", FileAccess.ReadWrite);

            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);

            SerializerWriterCollator preview_Document = writer.CreateVisualsCollator();

            preview_Document.BeginBatchWrite();

            preview_Document.Write(wpf_Element);  //*this or wpf xaml control

            preview_Document.EndBatchWrite();

            //--</ create xps document >--



            //var doc2 = new XpsDocument("Druckausgabe.xps", FileAccess.Read);



            FixedDocumentSequence preview = doc.GetFixedDocumentSequence();



            PrintWindow window = new PrintWindow(preview);

            window.Content = new DocumentViewer { Document = preview };

            window.Show();



            doc.Close();
            writer = null;
            preview_Document = null;
            doc = null;
            //------------</ WPF_Print_current_Window >------------





        }
    }
}

