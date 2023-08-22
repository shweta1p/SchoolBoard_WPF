using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kirin_2.ViewModel
{
    public class PdfViewer : INotifyPropertyChanged
    {
        private Stream docStream;

        public event PropertyChangedEventHandler PropertyChanged;

        public Stream DocumentStream
        {
            get
            {
                return docStream;
            }
            set
            {
                docStream = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DocumentStream"));
            }
        }

        public PdfViewer(string filePath)
        {
            //Load the stream system

            docStream = new FileStream(filePath, FileMode.OpenOrCreate);
        }

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
    }
}
