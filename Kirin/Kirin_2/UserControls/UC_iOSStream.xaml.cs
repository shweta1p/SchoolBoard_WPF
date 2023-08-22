using Kirin_2.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kirin_2.UserControls
{
    /// <summary>
    /// Interaction logic for UC_iOSStream.xaml
    /// </summary>
    public partial class UC_iOSStream : UserControl
    {
       
        public UC_iOSStream()
        {
            InitializeComponent();
        }

       
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void iOSStream_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
