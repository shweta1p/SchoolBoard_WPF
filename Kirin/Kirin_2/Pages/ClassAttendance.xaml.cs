using Kirin_2.UserControls;
using Kirin_2.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Kirin_2.Pages
{
    /// <summary>
    /// Interaction logic for ClassAttendance.xaml
    /// </summary>
    public partial class ClassAttendance : Page
    {
        public ObservableCollection<StudentDef> classlist;
        public App app;
        public string ClassID;
        ClassAttendanceViewModel classVM;
        public ClassAttendance(string classID, DateTime date)
        {
            InitializeComponent();
            classVM = new ClassAttendanceViewModel();
            classVM.AttendanceDate = date;
            app = (App)Application.Current;

            CombinedViewModel cvm = new CombinedViewModel();
            lblSchool.Content = cvm.SubjectVM._schooldata.FirstOrDefault().SchoolName;
            lblSemester.Content = cvm.SubjectVM._schooldata.FirstOrDefault().Semester;
            classVM.IsCodeVisible = false;
            classVM.IsCmbCodeVisible = true;
                        
            ClassID = classID;
            classVM.getClassList(Int32.Parse(classID), Convert.ToDateTime(date));

            ClassTitle.Content = "Record Meeting Attendance: " + classVM.getClassName(Int32.Parse(classID));
            classVM.AttendanceHeaderCurrent = "Attendance: " + Convert.ToDateTime(date).ToString("dddd, dd MMMM yyyy");
            dateSelected.Content = date.ToString("dd/MM/yyyy");

            var currentDate = DateTime.Now;
            TimeSpan ts = currentDate - classVM.AttendanceDate;

            if (ts.TotalHours > 24)
            {
                classVM.IsCodeVisible = true;
                classVM.IsCmbCodeVisible = false;
                submit.IsEnabled = false;
                //classVM.SubmitBackground = (Color)ColorConverter.ConvertFromString("#708090");
            }
            else
            {
                submit.IsEnabled = true;
                //classVM.SubmitBackground = (Color)ColorConverter.ConvertFromString("#3d78a6");
            }

            DataContext = classVM;
        }

        private void dpicker_Click(object sender, RoutedEventArgs e)
        {
            app.singleDay.Visibility = Visibility.Visible;
        }
        
        private void SubmitAttendance(object sender, System.Windows.RoutedEventArgs e)
        {
            //var item = sender as Button;

            //MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            //if (mw != null)
            //{
            //    // mw.MainWindowFrame.Content = new ClassMultiDayAttendance(item.Tag.ToString());
            //    //mw.MainWindowFrame.Content = new ClassMultiDayAttendance(ClassID);
            //}
        }

        private void SwitchToMultiDayView(object sender, System.Windows.RoutedEventArgs e)
        {
            var item = sender as Button;

            MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mw != null)
            {
                mw.MainWindowFrame.Content = new ClassMultiDayAttendance(item.Tag.ToString());
                //mw.MainWindowFrame.Content = new ClassMultiDayAttendance(ClassID);
            }
        }

        public static T FindAncestorOrSelf<T>(DependencyObject obj)
        where T : DependencyObject
        {
            while (obj != null)
            {
                T objTest = obj as T;

                if (objTest != null)
                    return objTest;

                obj = VisualTreeHelper.GetParent(obj);
            }
            return null;
        }

        //private void dpicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    DateTime datepicker = (DateTime)dpicker.SelectedDate;
        //    classVM.AttendanceDate = datepicker;
        //    classVM.getClassList(Int32.Parse(ClassID), datepicker);

        //    var currentDate = DateTime.Now;
        //    TimeSpan ts = currentDate - datepicker;

        //    if (ts.TotalHours > 24)
        //    {
        //        classVM.IsCodeVisible = true;
        //        classVM.IsCmbCodeVisible = false;
        //        submit.IsEnabled = false;
        //        classVM.SubmitBackground = (Color)ColorConverter.ConvertFromString("#708090");
        //    }
        //    else
        //    {
        //        submit.IsEnabled = true;
        //        classVM.SubmitBackground = (Color)ColorConverter.ConvertFromString("#3d78a6");
        //    }

        //    DataContext = classVM;
        //}


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
            if (obj is StudentDef sd)
            {
                return sd.STUDENT_FULLNAME.ToUpper().StartsWith(SearchBox.Text.ToUpper());

            }
            return false;
        }

        private void ViewComment(object sender, RoutedEventArgs e)
        {
            app.attendanceComment.Visibility = Visibility.Visible;
        }

        //private void Alters_Click(object sender, RoutedEventArgs e)
        //{
        //    app.addAlerts.Visibility = Visibility.Visible;
        //    app.addAlerts.SubjectId.Content = ClassID;
        //}
    }

    public class StudentAttendanceDay
    {
        public int id { get; set; }
        public string attendance { get; set; }
        public string comment { get; set; }
    }
}
