using Kirin_2.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Kirin_2.Pages
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        SettingsViewModel svm;
        public App app;
        public Settings()
        {
            app = (App)Application.Current;
            svm = new SettingsViewModel();
            InitializeComponent();

            this.DataContext = svm;

            RcAdmin1.Width = 440;
            Account.Width = 900;
            RcAdmin2.Width = 900;
            Filters.Width = 920;
            DGLeftMenu.Width = 340;
            Globals.reset = 0;
        }

        private void btnUser_Click(object sender, RoutedEventArgs e)
        {
            Puser.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#247BC0");
            Padmin.Foreground = Brushes.Transparent;

            RcUser.Visibility = Visibility.Visible;
            RcUser2.Visibility = Visibility.Visible;
            SpUser.Visibility = Visibility.Visible;
            RcAdmin1.Visibility = Visibility.Collapsed;
            RcAdmin2.Visibility = Visibility.Collapsed;
            bAdmin.Visibility = Visibility.Collapsed;
            Filters.Visibility = Visibility.Collapsed;
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            Padmin.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#247BC0");
            Puser.Foreground = Brushes.Transparent;

            bAdmin.Visibility = Visibility.Visible;
            RcAdmin1.Visibility = Visibility.Visible;
            RcAdmin2.Visibility = Visibility.Visible;
            RcUser.Visibility = Visibility.Collapsed;
            RcUser2.Visibility = Visibility.Collapsed;
            SpUser.Visibility = Visibility.Collapsed;
            Filters.Visibility = Visibility.Collapsed;
        }

        private void Hyperlink_Dashboard(object sender, RoutedEventArgs e)
        {

        }

        private void DGLeftMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LeftMenuAdmin data = ((DataGrid)sender).SelectedItem as LeftMenuAdmin;

            if (data != null)
            {
                string type = data.Icon;

                RcAdmin1.Width = 350;
                DGLeftMenu.Width = 300;
                SepAdmin.Visibility = Visibility.Visible;
                IconAdmin.Visibility = Visibility.Visible;
                IconAdmin.Margin = new Thickness(-40, 120, -40, 0);
                switch (type)
                {
                    case "Building":
                        Account.Width = 950;
                        Account.Visibility = Visibility.Visible;
                        RcAdmin2.Visibility = Visibility.Collapsed;
                        IconAdmin2.Visibility = Visibility.Collapsed;
                        break;
                    case "PeopleGroup":
                        RcAdmin2.Width = 950;
                        app.accountAccess.Visibility = Visibility.Visible;
                        RcAdmin2.Visibility = Visibility.Visible;
                        IconAdmin2.Visibility = Visibility.Visible;
                        IconAdmin2.Margin = new Thickness(-15,230, 20, 0);
                        scRMenu.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                        break;
                    case "Filter":
                        Filters.Width = 950;
                        Filters.Visibility = Visibility.Visible;
                        Account.Visibility = Visibility.Collapsed;
                        RcAdmin2.Visibility = Visibility.Collapsed;
                        IconAdmin2.Visibility = Visibility.Visible;
                        IconAdmin2.Margin = new Thickness(-15, 290, 20, 0);
                        scRMenu.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                        break;
                    case "Trash":
                        IconAdmin2.Visibility = Visibility.Visible;
                        IconAdmin2.Margin = new Thickness(-15, 350, 20, 0);
                        scRMenu.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                        break;
                }
            }
        }

        private void DGRightMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RightMenuAdmin data = ((DataGrid)sender).SelectedItem as RightMenuAdmin;

            if (data != null)
            {
                string type = data.Icon;

                SepAdmin.Visibility = Visibility.Visible;
                IconAdmin.Visibility = Visibility.Visible;
                IconAdmin2.Visibility = Visibility.Collapsed;
                RcAdmin2.Width = 370;
                SepAdmin2.Visibility = Visibility.Collapsed;
                //SepAdmin3.Visibility = Visibility.Visible;
                RcAdmin1.Visibility = Visibility.Collapsed;
                switch (type)
                {
                    case "ClipboardCheckOutline":
                        IconAdmin.Margin = new Thickness(-40,160,-40,0);
                        gdSetUp.Width = 940;
                        gdSetUp.Visibility = Visibility.Visible;
                        Filters.Visibility = Visibility.Collapsed;
                        Account.Visibility = Visibility.Collapsed;
                        RcAdmin1.Visibility = Visibility.Collapsed;
                        gdProperty.Visibility = Visibility.Collapsed;
                        gdDataStream.Visibility = Visibility.Collapsed;
                        gdDataCollection.Visibility = Visibility.Collapsed;
                        gdDataRetention.Visibility = Visibility.Collapsed;
                        gdDataFilters.Visibility = Visibility.Collapsed;
                        gdDataImport.Visibility = Visibility.Collapsed;
                        gdReportingIdentity.Visibility = Visibility.Collapsed;
                        break;
                    case "Settings":
                        IconAdmin.Margin = new Thickness(-40, 225, -40, 0);
                        gdProperty.Width = 940;
                        gdProperty.Visibility = Visibility.Visible;
                        gdSetUp.Visibility = Visibility.Collapsed;
                        Filters.Visibility = Visibility.Collapsed;
                        Account.Visibility = Visibility.Collapsed;
                        RcAdmin1.Visibility = Visibility.Collapsed;
                        gdDataStream.Visibility = Visibility.Collapsed;
                        gdDataCollection.Visibility = Visibility.Collapsed;
                        gdDataRetention.Visibility = Visibility.Collapsed;
                        gdDataFilters.Visibility = Visibility.Collapsed;
                        gdDataImport.Visibility = Visibility.Collapsed;
                        gdReportingIdentity.Visibility = Visibility.Collapsed;
                        break;
                    case "Gamepad":
                        IconAdmin.Margin = new Thickness(-40, 285, -40, 0);
                        gdDataStream.Width = 920;
                        gdDataStream.Visibility = Visibility.Visible;
                        gdProperty.Visibility = Visibility.Collapsed;
                        gdSetUp.Visibility = Visibility.Collapsed;
                        Filters.Visibility = Visibility.Collapsed;
                        Account.Visibility = Visibility.Collapsed;
                        RcAdmin1.Visibility = Visibility.Collapsed;
                        gdDataCollection.Visibility = Visibility.Collapsed;
                        gdDataRetention.Visibility = Visibility.Collapsed;
                        gdDataFilters.Visibility = Visibility.Collapsed;
                        gdDataImport.Visibility = Visibility.Collapsed;
                        gdReportingIdentity.Visibility = Visibility.Collapsed;
                        break;
                    case "Database":
                        IconAdmin.Margin = new Thickness(-40, 345, -40, 0);
                        //gdDataCollection.Width = 940;
                        //gdDataCollection.Visibility = Visibility.Visible;
                        //gdDataImport.Visibility = Visibility.Visible;
                        //gdDataStream.Visibility = Visibility.Collapsed;
                        //gdProperty.Visibility = Visibility.Collapsed;
                        //gdSetUp.Visibility = Visibility.Collapsed;
                        //Filters.Visibility = Visibility.Collapsed;
                        //Account.Visibility = Visibility.Collapsed;
                        //RcAdmin1.Visibility = Visibility.Collapsed;
                        //gdDataRetention.Visibility = Visibility.Collapsed;
                        //gdDataFilters.Visibility = Visibility.Collapsed;
                        //gdReportingIdentity.Visibility = Visibility.Collapsed;
                        break;
                    case "ArrowExpandUp":
                        IconAdmin.Margin = new Thickness(-40, 405, -40, 0);
                        gdDataImport.Width = 940;
                        gdDataImport.Visibility = Visibility.Visible;
                        gdDataStream.Visibility = Visibility.Collapsed;
                        gdProperty.Visibility = Visibility.Collapsed;
                        gdSetUp.Visibility = Visibility.Collapsed;
                        Filters.Visibility = Visibility.Collapsed;
                        Account.Visibility = Visibility.Collapsed;
                        RcAdmin1.Visibility = Visibility.Collapsed;
                        gdDataCollection.Visibility = Visibility.Collapsed;
                        gdDataRetention.Visibility = Visibility.Collapsed;
                        gdDataFilters.Visibility = Visibility.Collapsed;
                        gdReportingIdentity.Visibility = Visibility.Collapsed;
                        break;
                    case "AccountCash":
                        IconAdmin.Margin = new Thickness(-40, 465, -40, 0);
                        gdReportingIdentity.Width = 940;
                        gdReportingIdentity.Visibility = Visibility.Visible;
                        gdDataImport.Visibility = Visibility.Collapsed;
                        gdDataStream.Visibility = Visibility.Collapsed;
                        gdProperty.Visibility = Visibility.Collapsed;
                        gdSetUp.Visibility = Visibility.Collapsed;
                        Filters.Visibility = Visibility.Collapsed;
                        Account.Visibility = Visibility.Collapsed;
                        RcAdmin1.Visibility = Visibility.Collapsed;
                        gdDataCollection.Visibility = Visibility.Collapsed;
                        gdDataRetention.Visibility = Visibility.Collapsed;
                        gdDataFilters.Visibility = Visibility.Collapsed;
                        break;
                }
            }
        }

        private void btnAll_Click(object sender, RoutedEventArgs e)
        {
            Pall.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#247BC0");
            Pios.Foreground = Brushes.Transparent;
            Pandroid.Foreground = Brushes.Transparent;
            Pweb.Foreground = Brushes.Transparent;

            svm.GetDataStreamData("all");
            DGDataStreams.ItemsSource = svm.DataStreamList;
        }

        private void btnIos_Click(object sender, RoutedEventArgs e)
        {
            Pall.Foreground = Brushes.Transparent;
            Pios.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#247BC0");
            Pandroid.Foreground = Brushes.Transparent;
            Pweb.Foreground = Brushes.Transparent;

            svm.GetDataStreamData("ios");
            DGDataStreams.ItemsSource = svm.DataStreamList;
        }

        private void btnAndroid_Click(object sender, RoutedEventArgs e)
        {
            Pall.Foreground = Brushes.Transparent;
            Pios.Foreground = Brushes.Transparent;
            Pandroid.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#247BC0");
            Pweb.Foreground = Brushes.Transparent;

            svm.GetDataStreamData("android");
            DGDataStreams.ItemsSource = svm.DataStreamList;
        }

        private void btnWeb_Click(object sender, RoutedEventArgs e)
        {
            Pall.Foreground = Brushes.Transparent;
            Pios.Foreground = Brushes.Transparent;
            Pandroid.Foreground = Brushes.Transparent;
            Pweb.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#247BC0");

            svm.GetDataStreamData("web");
            DGDataStreams.ItemsSource = svm.DataStreamList;
        }

        private void DGDataStreams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataStream data = ((DataGrid)sender).SelectedItem as DataStream;

            if (data != null)
            {
                string type = data.Icon;

                switch (type)
                {
                    case "AppleIos":
                        app.iosStream.Visibility = Visibility.Visible;
                        break;
                }
            }
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            DataGridRow row = FindVisualParent<DataGridRow>(sender as Expander);
            row.DetailsVisibility = Visibility.Visible;
        }

        private void Expander_Collapsed(object sender, RoutedEventArgs e)
        {
            DataGridRow row = FindVisualParent<DataGridRow>(sender as Expander);
            row.DetailsVisibility = Visibility.Collapsed;
        }

        public T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null) return null;
            T parent = parentObject as T;
            if (parent != null)
                return parent;
            else
                return FindVisualParent<T>(parentObject);
        }

        private void DGRightMenu_RowDetailsVisibilityChanged(object sender, DataGridRowDetailsEventArgs e)
        {
            DataGrid MainDataGrid = sender as DataGrid;
            
            RightMenuAdmin rightMenu = (MainDataGrid.SelectedItem as RightMenuAdmin);
            if (rightMenu == null)
            {
                return;
            }
            else if (rightMenu.Icon == "Database")
            {
                List<RightSubMenuAdmin> subList = new List<RightSubMenuAdmin>();
                subList.Add(new RightSubMenuAdmin { ID = "1", Label = "Data Collection" });
                subList.Add(new RightSubMenuAdmin { ID = "2", Label = "Data Retention" });
                subList.Add(new RightSubMenuAdmin { ID = "3", Label = "Data Filters" });

                DataGrid DetailsDataGrid = e.DetailsElement as DataGrid;

                DetailsDataGrid.ItemsSource = subList;
            }
            else {
                List<RightSubMenuAdmin> subList = new List<RightSubMenuAdmin>();
                DataGrid DetailsDataGrid = e.DetailsElement as DataGrid;

                DetailsDataGrid.ItemsSource = subList;
            }
        }

        private void DGRightSubMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RightSubMenuAdmin data = ((DataGrid)sender).SelectedItem as RightSubMenuAdmin;

            if (data != null)
            {
                string label = data.Label;

                SepAdmin.Visibility = Visibility.Visible;
                IconAdmin.Visibility = Visibility.Visible;
                IconAdmin2.Visibility = Visibility.Collapsed;
                RcAdmin2.Width = 370;
                SepAdmin2.Visibility = Visibility.Collapsed;
                //SepAdmin3.Visibility = Visibility.Visible;
                RcAdmin1.Visibility = Visibility.Collapsed;
                switch (label)
                {
                    case "Data Collection":
                        gdDataCollection.Width = 940;
                        gdDataCollection.Visibility = Visibility.Visible;
                        gdSetUp.Visibility = Visibility.Collapsed;
                        Filters.Visibility = Visibility.Collapsed;
                        Account.Visibility = Visibility.Collapsed;
                        RcAdmin1.Visibility = Visibility.Collapsed;
                        gdProperty.Visibility = Visibility.Collapsed;
                        gdDataStream.Visibility = Visibility.Collapsed;
                        gdDataRetention.Visibility = Visibility.Collapsed;
                        gdDataFilters.Visibility = Visibility.Collapsed;
                        gdDataImport.Visibility = Visibility.Collapsed;
                        gdReportingIdentity.Visibility = Visibility.Collapsed;
                        break;
                    case "Data Retention":
                        gdDataRetention.Width = 940;
                        gdDataRetention.Visibility = Visibility.Visible;
                        gdDataCollection.Visibility = Visibility.Collapsed;
                        gdSetUp.Visibility = Visibility.Collapsed;
                        Filters.Visibility = Visibility.Collapsed;
                        Account.Visibility = Visibility.Collapsed;
                        RcAdmin1.Visibility = Visibility.Collapsed;
                        gdProperty.Visibility = Visibility.Collapsed;
                        gdDataStream.Visibility = Visibility.Collapsed;
                        gdDataFilters.Visibility = Visibility.Collapsed;
                        gdDataImport.Visibility = Visibility.Collapsed;
                        gdReportingIdentity.Visibility = Visibility.Collapsed;
                        break;
                    case "Data Filters":
                        gdDataFilters.Width = 940;
                        gdDataFilters.Visibility = Visibility.Visible;
                        gdDataRetention.Visibility = Visibility.Collapsed;
                        gdDataCollection.Visibility = Visibility.Collapsed;
                        gdSetUp.Visibility = Visibility.Collapsed;
                        Filters.Visibility = Visibility.Collapsed;
                        Account.Visibility = Visibility.Collapsed;
                        RcAdmin1.Visibility = Visibility.Collapsed;
                        gdProperty.Visibility = Visibility.Collapsed;
                        gdDataStream.Visibility = Visibility.Collapsed;
                        gdDataImport.Visibility = Visibility.Collapsed;
                        gdReportingIdentity.Visibility = Visibility.Collapsed;
                        break;
                }
            }
        }

        private void FilterView_Click(object sender, RoutedEventArgs e)
        {
            app.dataFilter.Visibility = Visibility.Visible; 
        }

        private void btnLeftArrow_Click(object sender, RoutedEventArgs e)
        {
            RcAdmin1.Width = 440;
            RcAdmin1.Visibility = Visibility.Visible;
            IconAdmin2.Visibility = Visibility.Visible;
            SepAdmin2.Visibility = Visibility.Visible;
            RcAdmin2.Visibility = Visibility.Visible;
            Account.Width = 900;
            RcAdmin2.Width = 900; 
            Filters.Width = 920;
            DGLeftMenu.Width = 340;
            SepAdmin.Visibility = Visibility.Collapsed;
            IconAdmin.Visibility = Visibility.Collapsed;
            gdDataCollection.Visibility = Visibility.Collapsed;
            gdSetUp.Visibility = Visibility.Collapsed;
            Filters.Visibility = Visibility.Collapsed;
            Account.Visibility = Visibility.Collapsed;
            gdProperty.Visibility = Visibility.Collapsed;
            gdDataStream.Visibility = Visibility.Collapsed;
            gdDataRetention.Visibility = Visibility.Collapsed;
            gdDataFilters.Visibility = Visibility.Collapsed;
            gdDataImport.Visibility = Visibility.Collapsed;
            gdReportingIdentity.Visibility = Visibility.Collapsed;
        }        
    }

}
