using Kirin_2.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kirin_2.UserControls
{
    /// <summary>
    /// Interaction logic for UC_EditDataFilter.xaml
    /// </summary>
    public partial class UC_EditDataFilter : UserControl
    {
       
        public UC_EditDataFilter()
        {
            InitializeComponent();
        }

       
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void EditDataFilter_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
