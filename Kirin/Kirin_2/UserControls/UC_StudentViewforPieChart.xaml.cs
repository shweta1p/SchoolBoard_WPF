using Kirin_2.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kirin_2.UserControls
{
    /// <summary>
    /// Interaction logic for UC_StudentViewforPieChart.xaml
    /// </summary>
    public partial class UC_StudentViewforPieChart : UserControl
    {
        public UC_StudentViewforPieChart()
        {
            InitializeComponent();            
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void StudentView_Loaded(object sender, RoutedEventArgs e)
        {
          
        }
    }
}
