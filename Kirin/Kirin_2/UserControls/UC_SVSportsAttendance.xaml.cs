using Kirin_2.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Kirin_2.UserControls
{
    /// <summary>
    /// Interaction logic for UC_SVSportsAttendance.xaml
    /// </summary>
    public partial class UC_SVSportsAttendance : UserControl
    {
        public UC_SVSportsAttendance()
        {
            InitializeComponent();           
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void StudentView_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

    }
}
