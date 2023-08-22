﻿using Kirin_2.UserControls;
using Kirin_2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Kirin_2.Pages
{
    /// <summary>
    /// Interaction logic for DashboardInsights.xaml
    /// </summary>
    public partial class DashboardInsights : Page
    {
        public App app;
        public DashboardInsights()
        {
            app = (App)Application.Current;
            InitializeComponent();
            FillList();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var ah = ActualHeight;
            var aw = ActualWidth;
            var h = Height;
            var w = Width;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mw != null)
            {
               // mw.MainWindowFrame.Content = new Dashboard();
                mw.MainWindowFrame.NavigationService.GoBack();
            }
        }

        private void btnThumbsUp_Click(object sender, RoutedEventArgs e)
        {
            app.insightFeedback.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ThumbsUpPopup.Visibility = Visibility.Collapsed;
        }

        private void btnThumbsDown_Click(object sender, RoutedEventArgs e)
        {
            app.insightFeedback.Visibility = Visibility.Visible;
        }

        private void FillList()
        {
            List<RadioButtonList> aList = new List<RadioButtonList>();
            aList.Add(new RadioButtonList() { Value = "5 minutes or less", IsSelected = false });
            aList.Add(new RadioButtonList() { Value = "5-30 minutes", IsSelected = false });
            aList.Add(new RadioButtonList() { Value = "30-60 minutes", IsSelected = false });
            aList.Add(new RadioButtonList() { Value = "1-3 hours", IsSelected = false });
            aList.Add(new RadioButtonList() { Value = "3 hourse or more", IsSelected = false });

            this.listBoxZone.ItemsSource = aList;
            this.listBoxDown.ItemsSource = aList;
        }
    }
}
