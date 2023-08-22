using Kirin_2.ViewModel;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Kirin_2.Pages
{
    /// <summary>
    /// Interaction logic for ClassMultiDayAttendance.xaml
    /// </summary>
    public partial class ClassMultiDayAttendance : Page
    {
        //string classID = "1";
        public ClassMultiDayAttendance(string classID) 
        {
            InitializeComponent();
            ClassAttendanceViewModel classVM = new ClassAttendanceViewModel();
            DataContext = classVM;
            classVM.getMultiDayClassAttendance();
            classVM.CURRENTCLASSID = Int32.Parse(classID);
            classVM.DateRangeFilter_From = DateTime.Now;
            classVM.DateRangeFilter_To = DateTime.Now.AddDays(7);
            ClassTitle.Content = "Record Meeting Attendance: " + classVM.getClassName(Int32.Parse(classID));
        }

        private void SwitchToSingleDayView(object sender, System.Windows.RoutedEventArgs e)
        {
            var item = sender as Button;

            MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mw != null)
            {
                mw.MainWindowFrame.Content = new ClassAttendance(item.Tag.ToString(), DateTime.Now);
            }
        }

        private void SubmitAttendance(object sender, RoutedEventArgs e)
        {
            var item = sender as Button;

            MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mw != null)
            {
                mw.MainWindowFrame.Content = new AttendancePage();
            }
        }

        private void AddAlertComment(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Add alert comment");
        }

        public System.ComponentModel.ICollectionView SDCollectionView;
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var Searchbox = sender as TextBox;
            SDCollectionView = CollectionViewSource.GetDefaultView(AttendanceList.ItemsSource);
            SDCollectionView.Filter = FilterStaffDirectory;
            refreshGrid();
        }

        public void refreshGrid()
        {
            SDCollectionView.Refresh();
        }

        private bool FilterStaffDirectory(object obj)
        {
            if (obj is StudentDefMultiDay sd)
            {
                return sd.STUDENT_FULLNAME.ToUpper().StartsWith(SearchBox.Text.ToUpper());

            }
            return false;
        }
    }
}
