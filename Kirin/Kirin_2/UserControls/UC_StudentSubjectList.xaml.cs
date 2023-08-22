using Kirin_2.Pages;
using Kirin_2.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kirin_2.UserControls
{
    /// <summary>
    /// Interaction logic for UC_StudentSubjectList.xaml
    /// </summary>
    public partial class UC_StudentSubjectList : UserControl
    {        
        public UC_StudentSubjectList()
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

