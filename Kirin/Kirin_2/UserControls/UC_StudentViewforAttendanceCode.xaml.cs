using Kirin_2.Pages;
using Kirin_2.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kirin_2.UserControls
{
    /// <summary>
    /// Interaction logic for UC_StudentViewforAttendanceCode.xaml
    /// </summary>
    public partial class UC_StudentViewforAttendanceCode : UserControl
    {
        public UC_StudentViewforAttendanceCode()
        {
            InitializeComponent();
        }

        private void ViewAttendance(object sender, RoutedEventArgs e)
        {
            App app = (App)Application.Current;
            var x = (Button)sender;
            if (((StudentAttendanceData)x.DataContext) != null)
            {
                string subjectId = App.Current.Properties["SubjectId"].ToString();
                try
                {
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(MainWindow))
                        {
                            ((Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService).Navigate(new ClassAttendance(subjectId,Convert.ToDateTime(date.Content)));
                            Globals.reset = 1;
                        }
                    }
                }
                catch (Exception ee)
                {
                }
            }
            this.Visibility = Visibility.Collapsed;
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
