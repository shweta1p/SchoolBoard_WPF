using Kirin_2.Pages;
using Kirin_2.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kirin_2.UserControls
{
    /// <summary>
    /// Interaction logic for UC_SVTransferStuentBar.xaml
    /// </summary>
    public partial class UC_SVTransferStudentBar : UserControl
    {        
        public UC_SVTransferStudentBar()
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

        private void ViewProfile(object sender, RoutedEventArgs e)
        {
            var x = (Button)sender;
            if (((StudentData)x.DataContext) != null)
            {
                try
                {
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(MainWindow))
                        {
                            ((Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService).Navigate(new SchoolDB_StudentView(((StudentData)x.DataContext).ID.ToString()));
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

        private void StudentView_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}

