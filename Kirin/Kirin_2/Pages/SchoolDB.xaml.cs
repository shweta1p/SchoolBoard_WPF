using Kirin_2.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.ComponentModel;
using System.Threading.Tasks;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.Windows.Controls.Notification;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

namespace Kirin_2.Pages
{
    /// <summary>
    /// Interaction logic for SchoolDB.xaml
    /// </summary>
    public partial class SchoolDB : Page
    {
        UserViewModel uvm;

        public SchoolDB()
        {
            uvm = new UserViewModel();
            Globals.reset = 0;
            InitializeComponent();

            //var schoolData = App.Current.Properties["SchoolBoard"];
            //var myCollection = new ObservableCollection<SCHOOLDB_DESC>(schoolData);
            //uvm.SCHOOLDB_ALL = myCollection;

            //busyIndicator.AnimationType = AnimationTypes.Gear;
            //busyIndicator.Foreground = Brushes.Blue;
            //busyIndicator.Background = Brushes.Transparent;
            //busyIndicator.ViewboxHeight = 100;
            //busyIndicator.ViewboxWidth = 100;
            //busyIndicator.IsBusy = true;
            //busyIndicator.Visibility = Visibility.Visible;

            //BackgroundWorker worker = new BackgroundWorker();

            ////this is where the long running process should go
            //worker.DoWork += (o, ea) =>
            //{
            //    //uvm.LoadSchoolDB("All");
            //};

            //worker.RunWorkerCompleted += async (o, ea) =>
            //{
            //    await Task.Delay(1000);

            //    //work has completed. you can now interact with the UI
            //    busyIndicator.IsBusy = false;
            //    busyIndicator.Visibility = Visibility.Collapsed;
            //};
            //worker.RunWorkerAsync();

            //uvm.LoadSchoolDB("All");
            uvm.PhoneNumberLabel = "Phone #";
            //uvm.Role = "All";
            //uvm.LoadData();
            this.DataContext = uvm;
            dataPager.OnDemandLoading += dataPager_OnDemandLoading;
        }

        private async void dataPager_OnDemandLoading(object sender, Syncfusion.UI.Xaml.Controls.DataPager.OnDemandLoadingEventArgs args)
        {
            var source = await GetSchoolDBData();

            //Data's loaded to SfDataPager dynamically     
            dataPager.LoadDynamicItems(args.StartIndex, source.Take(args.PageSize));

            if (DGStaff.ItemsSource != null)
            {
                DGStaff.View.Filter = FilterRecords;
                DGStaff.View.RefreshFilter();
            }
        }

        public async Task<ObservableCollection<SCHOOLDB_DESC>> GetSchoolDBData()
        {
            uvm.Role = "All";
            uvm.LoadData();
            uvm.LoadSchoolDB(); 
            return uvm.SCHOOLDB_ALL;
            //return uvm.IncrementalItemsSource;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var ah = ActualHeight;
            var aw = ActualWidth;
            var h = Height;
            var w = Width;

            DGStaff.Height = (ah - 140);
        }

        public void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DGStaff.ItemsSource != null)
            {
                DGStaff.View.Filter = FilterRecords;
                DGStaff.View.RefreshFilter();
            }
        }

        public bool FilterRecords(object obj)
        {
            if (obj is SCHOOLDB_DESC SB)
            {
                return SB.NAME.ToUpper().StartsWith(SearchBox.Text.ToUpper())
                || SB.EMAIL.ToUpper().StartsWith(SearchBox.Text.ToUpper());
            }
            return false;
        }

        private void rdAll_Checked(object sender, RoutedEventArgs e)
        {   
            if (dataPager != null)
            {
                uvm.Role = "All";
                uvm.LoadData();
                dataPager.Source = uvm.IncrementalItemsSource;
            }
        }

        private void rdStudents_Checked(object sender, RoutedEventArgs e)
        {
            uvm.Role = "Student";
            uvm.LoadData();
            // uvm.LoadSchoolDB();
            //ObservableCollection<SCHOOLDB_DESC> schoolDB = new ObservableCollection<SCHOOLDB_DESC>(uvm.SCHOOLDB_ALL.Where(x => x.mROLE.ToLower() == "student"));
            //dataPager.Source = schoolDB;
            dataPager.Source = uvm.IncrementalItemsSource;
        }

        private void rdTeachers_Checked(object sender, RoutedEventArgs e)
        {
            uvm.Role = "Teacher";
            uvm.LoadData();
            //uvm.LoadSchoolDB();
            //ObservableCollection<SCHOOLDB_DESC> schoolDB = new ObservableCollection<SCHOOLDB_DESC>(uvm.SCHOOLDB_ALL.Where(x => x.mROLE.ToLower() == "teacher"));
            //uvm.SCHOOLDB_ALL = schoolDB;
            //dataPager.Source = schoolDB;
            dataPager.Source = uvm.IncrementalItemsSource;
        }

        private void rdStaff_Checked(object sender, RoutedEventArgs e)
        {
            uvm.Role = "Support";
            uvm.LoadData();
            //uvm.LoadSchoolDB();
            //ObservableCollection<SCHOOLDB_DESC> schoolDB = new ObservableCollection<SCHOOLDB_DESC>(uvm.SCHOOLDB_ALL.Where(x => x.mROLE.ToLower() == "support"));
            //dataPager.Source = schoolDB;
            //uvm.SCHOOLDB_ALL = schoolDB;
            dataPager.Source = uvm.IncrementalItemsSource;
        }

        private void rdAdmin_Checked(object sender, RoutedEventArgs e)
        {
            uvm.Role = "Admin";
            uvm.LoadData();
            //uvm.LoadSchoolDB();
            //ObservableCollection<SCHOOLDB_DESC> schoolDB = new ObservableCollection<SCHOOLDB_DESC>(uvm.SCHOOLDB_ALL.Where(x => x.mROLE.ToLower() == "admin"));
            //dataPager.Source = schoolDB;
            //uvm.SCHOOLDB_ALL = schoolDB;
            dataPager.Source = uvm.IncrementalItemsSource;
        }

        //private void DGStaff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    SCHOOLDB_DESC x = (SCHOOLDB_DESC)DGStaff.SelectedItem;
        //    if (x != null)
        //    {
        //        if (x.ROLE == "STUDENT")
        //        {
        //            try
        //            {
        //                foreach (Window window in Application.Current.Windows)
        //                {
        //                    if (window.GetType() == typeof(MainWindow))
        //                    {
        //                        //  NavigationService.Navigate(new Uri(string.Format("{0}{1}", "Pages/", "ScoreSheet_StudentView.xaml?ID=" + (sender as Button).Tag), UriKind.RelativeOrAbsolute));
        //                        this.NavigationService.Navigate(new SchoolDB_StudentView((sender as Button).Tag.ToString()));
        //                        // (window as MainWindow).MainWindowFrame.Navigate(new Uri(string.Format("{0}{1}{2}", "Pages/", "ScoreSheet_StudentView?ID="+(sender as Button).Tag, ".xaml"), UriKind.RelativeOrAbsolute));
        //                    }

        //                }


        //                //foreach (Window window in Application.Current.Windows)
        //                //{
        //                //    if (window.GetType() == typeof(MainWindow))
        //                //    {
        //                //        // NavigationService.Navigate(new Uri(string.Format("{0}{1}", "Pages/", "SchoolDB_StudentView.xaml?ID=" + x.ID.ToString()), UriKind.RelativeOrAbsolute));
        //                //        if (this.NavigationService == null)
        //                //        {
        //                //            ((Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService).Navigate(new SchoolDB_StudentView(x.ID.ToString()));
        //                //        }
        //                //        else
        //                //        {
        //                //            this.NavigationService.Navigate(new SchoolDB_StudentView(x.ID.ToString()));
        //                //        }
        //                //        // (window as MainWindow).MainWindowFrame.Navigate(new Uri(string.Format("{0}", "Pages/SchoolDB_StudentView.xaml?ID="+x.ID.ToString()), UriKind.RelativeOrAbsolute));

        //                //    }

        //                //}
        //            }
        //            catch (Exception ee)
        //            {


        //            }

        //        }
        //        if (x.ROLE == "TEACHER")
        //        {
        //            try
        //            {
        //                foreach (Window window in Application.Current.Windows)
        //                {
        //                    if (window.GetType() == typeof(MainWindow))
        //                    {
        //                        //  NavigationService.Navigate(new Uri(string.Format("{0}{1}", "Pages/", "ScoreSheet_StudentView.xaml?ID=" + (sender as Button).Tag), UriKind.RelativeOrAbsolute));
        //                        this.NavigationService.Navigate(new SchoolDB_TeacherView(x.ID.ToString()));
        //                        //((Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService).Navigate(new SchoolDB_TeacherView(x.ID.ToString()));

        //                        // (window as MainWindow).MainWindowFrame.Navigate(new Uri(string.Format("{0}{1}{2}", "Pages/", "ScoreSheet_StudentView?ID="+(sender as Button).Tag, ".xaml"), UriKind.RelativeOrAbsolute));

        //                    }

        //                }
        //            }
        //            catch (Exception ee)
        //            {


        //            }
        //        }

        //    }
        //}

        private void ViewProfile(object sender, RoutedEventArgs e)
        {
            var x = (Button)sender;
            if (((SCHOOLDB_DESC)x.DataContext).ID != null)
            {
                //if (((SCHOOLDB_DESC)x.DataContext).ROLE == "STUDENT")
                //{
                //    try
                //    {
                //        foreach (Window window in Application.Current.Windows)
                //        {
                //            if (window.GetType() == typeof(MainWindow))
                //            {
                //                // NavigationService.Navigate(new Uri(string.Format("{0}{1}", "Pages/", "SchoolDB_StudentView.xaml?ID=" + x.ID.ToString()), UriKind.RelativeOrAbsolute));
                //                if (this.NavigationService == null)
                //                {
                //                    ((Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService).Navigate(new SchoolDB_StudentView(((SCHOOLDB_DESC)x.DataContext).ID.ToString()));
                //                }
                //                else
                //                {
                //                    this.NavigationService.Navigate(new SchoolDB_StudentView(((SCHOOLDB_DESC)x.DataContext).ID.ToString()));
                //                }
                //                // (window as MainWindow).MainWindowFrame.Navigate(new Uri(string.Format("{0}", "Pages/SchoolDB_StudentView.xaml?ID="+x.ID.ToString()), UriKind.RelativeOrAbsolute));

                //            }

                //        }
                //    }
                //    catch (Exception ee)
                //    {


                //    }

                //}

                if (((SCHOOLDB_DESC)x.DataContext).ROLE == "TEACHER")
                {
                    try
                    {
                        foreach (Window window in Application.Current.Windows)
                        {
                            if (window.GetType() == typeof(MainWindow))
                            {
                                if (this.NavigationService == null)
                                {
                                    ((Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService).Navigate(new UC_StaffProfileModal(((SCHOOLDB_DESC)x.DataContext).ID.ToString(), ((SCHOOLDB_DESC)x.DataContext).ROLE.ToString()));
                                }
                                else
                                {
                                    this.NavigationService.Navigate(new SchoolDB_StudentView(((SCHOOLDB_DESC)x.DataContext).ID.ToString()));
                                }

                            }

                        }
                    }
                    catch (Exception ee)
                    {


                    }
                }

            }
        }
    }

    //public  class Navigator
    //{
    //   private static NavigationService NavigationService { get; } = (Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService;
    //  //      private  NavigationService NavigationService { get; } = this.MainWindowFrame.NavigationService;

    //    public  void Navigate(string path, object param = null)
    //    {
    //        NavigationService.Navigate(new Uri(path, UriKind.Relative), param);
    //    }

    //    public  void GoBack()
    //    {
    //        if (NavigationService.CanGoBack)
    //        {
    //            NavigationService.GoBack();
    //        }

    //    }

    //    public  void GoForward()
    //    {
    //        NavigationService.GoForward();
    //    }
    //}


}
